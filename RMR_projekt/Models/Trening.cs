using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMR_projekt.Models
{
    public class Trening
    {
        [Key]
        public int id_trening { get; set; }
        public DateTime datum { get; set; }
        public TimeSpan trajanje { get; set; }
        public int porabljene_kalorije { get; set; }
        public int tk_uporabnik { get; set; }

        public Trening(int id_trening, DateTime datum, TimeSpan trajanje, int porabljene_kalorije, int tk_uporabnik)
        {
            this.id_trening = id_trening;
            this.datum = datum;
            this.trajanje = trajanje;
            this.porabljene_kalorije = porabljene_kalorije;
            this.tk_uporabnik = tk_uporabnik;
        }

        public Trening()
        {
        }
    }
}
