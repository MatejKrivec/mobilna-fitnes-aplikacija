using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMR_projekt.Models
{
    public class Vadba
    {
        public int id_vadba { get; set; }
        public string naziv { get; set; }
        public int kalorije { get; set; }
        public TimeSpan work { get; set; }
        public TimeSpan rest { get; set; }
        public int sets { get; set; }
        public int tk_trening { get; set; }
    }
}
