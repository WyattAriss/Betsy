using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Betsy.Data;
using Betsy.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Betsy.Controllers
{
    public class BetsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _services;
        private readonly UserManager<ApplicationUser> _userManager;

        public BetsController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IServiceProvider services) {
            _context = context;
            _services = services;
            _userManager = userManager;
        }

        public double OddsCalculation(double betAmount, int odds) {
            if (odds > 0) {
                return betAmount + (betAmount * (odds / 100.0));
            }
            return betAmount + (Math.Abs(betAmount) / (odds / 100.0));
        }

        // GET: Bets
        public async Task<IActionResult> Index() {
            var userId = _userManager.GetUserId(HttpContext.User);
            var applicationDbContext = _context.Bet.Include(b => b.UserObj).Include(f => f.FightObj).Where(b => b.User == userId);
            return View(await applicationDbContext.ToListAsync());
        }

        [ValidateAntiForgeryToken]
        public IActionResult ConfirmBet([Bind("BetId,Fight,FightObj,User,UserObj,Odds,BetAmount,Pick,ClosingDate")] Bet bet, string fighter) {
                if (fighter == "1") {
                    bet.Pick = bet.FightObj.Fighter1;
                    bet.Odds = bet.FightObj.Odds1;
                } else {
                    bet.Pick = bet.FightObj.Fighter2;
                    bet.Odds = bet.FightObj.Odds2;
                }
                bet.Open = true;
                bet.ReturnAmount = OddsCalculation(bet.BetAmount, bet.Odds);
                return View(bet);
        }

        // GET: Bets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bet = await _context.Bet.SingleOrDefaultAsync(m => m.BetId == id);
            if (bet == null)
            {
                return NotFound();
            }
            ViewData["User"] = new SelectList(_context.ApplicationUser, "Id", "Id", bet.User);
            return View(bet);
        }

        // POST: Bets/InsertBet/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InsertBet([Bind("BetId,Fight,FightObj,User,UserObj,Odds,BetAmount,Pick,ClosingDate")] Bet bet)
        {

            if (ModelState.IsValid) {
                bet.FightObj = _context.Fight.Find(bet.Fight);
                int flag = TakeBetMoney(bet.User, bet.BetAmount);
                if (flag == 0) {
                    return NotFound();
                } else if (flag == 1) {
                    // insufficient funds
                }
                bet.Open = true;
                bet.ReturnAmount = OddsCalculation(bet.BetAmount, bet.Odds);
                _context.Add(bet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["User"] = new SelectList(_context.ApplicationUser, "Id", "Id", bet.User);
            return RedirectToAction("Index");
        }

        private int TakeBetMoney(string userId, double betAmount) {

            if (userId == null) {
                return 0;
            }

            var user = _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == userId);
            if (user == null) {
                return 0;
            }

            try {
                if (user.Result.Money < betAmount) {
                    return 1;
                }
                user.Result.Money -= betAmount;
                _context.Update(user.Result);
                _context.SaveChanges();
                return 2;
            } catch (DbUpdateConcurrencyException) {
                return 0;
            }

            return 0;
        }

        // GET: Bets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bet = await _context.Bet
                .Include(b => b.UserObj)
                .SingleOrDefaultAsync(m => m.BetId == id);
            if (bet == null)
            {
                return NotFound();
            }

            return View(bet);
        }

        // POST: Bets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bet = await _context.Bet.SingleOrDefaultAsync(m => m.BetId == id);
            _context.Bet.Remove(bet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BetExists(int id)
        {
            return _context.Bet.Any(e => e.BetId == id);
        }

        // GET: Bets/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var bet = await _context.Bet
                .Include(b => b.UserObj)
                .SingleOrDefaultAsync(m => m.BetId == id);
            if (bet == null) {
                return NotFound();
            }

            return View(bet);
        }

        public IActionResult CloseOut() {
            PayoutAllBets();
            return RedirectToAction("Index");
        }

        public void PayoutAllBets() {
            var betsToCloseOut = _context.Bet.Where(b => b.FightObj.Winner != null && b.Open).ToList();
            string winningFighter = "";

            foreach (var bet in betsToCloseOut) {
                bet.FightObj = _context.Fight.Find(bet.Fight);
                if (bet.FightObj.Winner == 1) {
                    winningFighter = bet.FightObj.Fighter1;
                } else {
                    winningFighter = bet.FightObj.Fighter2;
                }
                bet.Open = false;

                if (bet.Pick == winningFighter) {
                    var user = _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == bet.User);
                    if (bet.ReturnAmount > 0) {
                        user.Result.Money += bet.ReturnAmount;
                        _context.Update(user.Result);
                        _context.SaveChanges();
                    }   
                }
            }
        }
    }
}
