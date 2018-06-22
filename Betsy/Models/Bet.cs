using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Betsy.Models
{
    public class Bet
    {
        public int BetId { get; set; }

        [Required]
        public int Odds { get; set; }

        [Required]
        public string Pick { get; set; }

        [Required]
        [Range(1, Double.MaxValue, ErrorMessage = "Bet amount must be a positive number")]
        public double BetAmount { get; set; }

        public double ReturnAmount { get; set; }

        public DateTime? ClosingDate { get; set; }

        [ScaffoldColumn(false)]
        public bool Open { get; set; }

        [ForeignKey("UserObj")]
        [ScaffoldColumn(false)]
        [Display(Name = "User")]
        public string User { get; set; }

        [ScaffoldColumn(false)]
        public ApplicationUser UserObj { get; set; }

        [ForeignKey("FightObj")]
        [ScaffoldColumn(false)]
        [Display(Name = "Fight")]
        public int Fight { get; set; }

        [ScaffoldColumn(false)]
        public Fight FightObj { get; set; }
    }
}
