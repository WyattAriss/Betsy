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
    public class FightsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _services;

        public FightsController(ApplicationDbContext context, IServiceProvider services)
        {
            _context = context;
            _services = services;
        }

        // GET: Fights
        public async Task<IActionResult> Index()
        {
            return View(await _context.Fight.ToListAsync());
        }

        // GET: Fights/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fight = await _context.Fight
                .SingleOrDefaultAsync(m => m.FightId == id);
            if (fight == null)
            {
                return NotFound();
            }

            return View(fight);
        }

        // GET: Fights/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fights/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FightId,Fighter1,Fighter2,Event,Odds,ClosingDate")] Fight fight)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fight);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fight);
        }

        // GET: Fights/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fight = await _context.Fight.SingleOrDefaultAsync(m => m.FightId == id);
            if (fight == null)
            {
                return NotFound();
            }
            return View(fight);
        }

        // POST: Fights/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FightId,Fighter1,Fighter2,Event,Odds,ClosingDate")] Fight fight)
        {
            if (id != fight.FightId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fight);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FightExists(fight.FightId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(fight);
        }

        // GET: Fights/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fight = await _context.Fight
                .SingleOrDefaultAsync(m => m.FightId == id);
            if (fight == null)
            {
                return NotFound();
            }

            return View(fight);
        }

        // POST: Fights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fight = await _context.Fight.SingleOrDefaultAsync(m => m.FightId == id);
            _context.Fight.Remove(fight);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FightExists(int id)
        {
            return _context.Fight.Any(e => e.FightId == id);
        }

        // GET: Fights/PlaceBet/5
        public async Task<IActionResult> PlaceBet(int? id) {
            if (id == null) {
                return NotFound();
            }

            var fight = await _context.Fight.SingleOrDefaultAsync(m => m.FightId == id);
            if (fight == null) {
                return NotFound();
            }
            // TODO: Should only be able to place a new bet on fights not already bet on, otherwise, overwrite (or disallow)
            // and only when you have enough money

            var userManager = _services.GetRequiredService<UserManager<ApplicationUser>>();
            var bet = new Bet() {
                Odds = fight.Odds1,
                Pick = "",
                BetAmount = 0,
                ReturnAmount = 0,
                ClosingDate = fight.ClosingDate,
                User = userManager.GetUserAsync(HttpContext.User).Result.Id,
                UserObj = userManager.GetUserAsync(HttpContext.User).Result,
                Fight = fight.FightId,
                FightObj = fight
            };
            if (ModelState.IsValid) {

                ViewData["User"] = new SelectList(_context.ApplicationUser, "Id", "Id", bet.User);
                return View("~/Views/Bets/PlaceBet.cshtml", bet);
            }
            // Shouldnt reach here.
            return RedirectToAction(nameof(Index));
        }

        // GET: Fights/DeclareWinner/5
        public async Task<IActionResult> DeclareWinner(int? id) {
            if (id == null) {
                return NotFound();
            }

            var fight = await _context.Fight.SingleOrDefaultAsync(m => m.FightId == id);
            if (fight == null) {
                return NotFound();
            }
            return View(fight);
        }

        // POST: Fights/DeclareWinner/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeclareWinner(int id, string Winner, Fight fight) {
            if (id != fight.FightId) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    fight.Winner = Int32.Parse(Winner);
                    _context.Update(fight);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!FightExists(fight.FightId)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(fight);
        }
    }
}
