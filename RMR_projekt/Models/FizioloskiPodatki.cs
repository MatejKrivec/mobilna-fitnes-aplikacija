using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMR_projekt.Models
{
    public class FizioloskiPodatki
    {
        private static FizioloskiPodatki instance;

        public string Spol { get; set; }
        public int visina_cm { get; set; }
        public int teza_kg { get; set; }

        public FizioloskiPodatki(string spol, int visina_cm, int teza_kg)
        {
            this.Spol = spol;
            this.visina_cm = visina_cm;
            this.teza_kg = teza_kg;
        }

        private FizioloskiPodatki() { }

        public static FizioloskiPodatki Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FizioloskiPodatki();
                }
                return instance;
            }
        }
    }
}
