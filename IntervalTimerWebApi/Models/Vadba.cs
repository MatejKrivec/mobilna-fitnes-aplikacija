using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IntervalTimerWebApi.Models
{
    public class Vadba
    {
        [Key]
        public int id_vadba { get; set; }
        public string naziv { get; set; }
        public int kalorije { get; set; }
        public TimeSpan work { get; set; }
        public TimeSpan rest { get; set; }
        public int sets { get; set; }

        [ForeignKey("Trening")]
        public int tk_trening { get; set; }

        [JsonIgnore]
        public Trening? Trening { get; set; }

        public Vadba(int id_vadba, int kalorije, TimeSpan work, TimeSpan rest, int sets, int fk_trening, Trening trening)
        {
            this.id_vadba = id_vadba;
            this.kalorije = kalorije;
            this.work = work;
            this.rest = rest;
            this.sets = sets;
            this.tk_trening = fk_trening;
            Trening = trening;
        }

        public Vadba(int id_vadba, int kalorije, TimeSpan work, TimeSpan rest, int sets, Trening trening)
        {
            this.id_vadba = id_vadba;
            this.kalorije = kalorije;
            this.work = work;
            this.rest = rest;
            this.sets = sets;
            Trening = trening;
        }

        public Vadba(int id_vadba_trening, int kalorije, TimeSpan work, TimeSpan rest, int sets, int fk_trening)
        {
            this.id_vadba = id_vadba;
            this.kalorije = kalorije;
            this.work = work;
            this.rest = rest;
            this.sets = sets;
            this.tk_trening = fk_trening;
        }

        public Vadba()
        {
        }
    }
}
