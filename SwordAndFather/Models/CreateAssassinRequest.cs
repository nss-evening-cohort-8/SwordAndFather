using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwordAndFather.Models
{
    public class CreateAssassinRequest
    {
        public string CodeName { get; set; }
        public string Catchphrase { get; set; }
        public string PreferredWeapon { get; set; }
    }
}
