using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Betsy.Models
{
    public class Fight
    {
        public int FightId { get; set; }

        public string Fighter1 { get; set; }

        public string Fighter2 { get; set; }

        public int? Winner { get; set; }

        public string Event { get; set; }

        public int Odds1 { get; set; }

        public int Odds2 { get; set; }

        public DateTime? ClosingDate { get; set; }
    }
}
