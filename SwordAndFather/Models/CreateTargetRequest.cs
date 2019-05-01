using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwordAndFather.Models
{
    public class CreateTargetRequest
    {
        public string Location { get; set; }
        public string Name { get; set; }
        public FitnessLevel FitnessLevel { get; set; }
        public int UserId { get; set; }
    }

    public enum FitnessLevel
    {
        Bad,
        Good,
        Awesome,
        Ovaltine
    }
}
