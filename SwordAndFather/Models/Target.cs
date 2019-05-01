using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwordAndFather.Models
{
    public class Target
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Bounty { get; set; }
        public string Location { get; set; }
        public FitnessLevel FitnessLevel { get; set; }
        public int Age { get; set; }
        public int UserId { get; set; }
        public bool IsDead { get; set; }

    }
}
