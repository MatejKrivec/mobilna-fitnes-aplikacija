using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IntervalTimerWebApi.Models
{
    public class Trening
    {
        [Key]
        public int id_trening { get; set; }
        public DateTime datum { get; set; }
        public TimeSpan trajanje { get; set; }
        public int porabljene_kalorije { get; set; }

        [ForeignKey("Uporabnik")]
        public int tk_uporabnik { get; set; }

        [JsonIgnore]
        public Uporabnik? Uporabnik { get; set; }

        public Trening(int id_trening, DateTime datum, TimeSpan trajanje, int porabljene_kalorije, int tk_uporabnik, Uporabnik? uporabnik)
        {
            this.id_trening = id_trening;
            this.datum = datum;
            this.trajanje = trajanje;
            this.porabljene_kalorije = porabljene_kalorije;
            this.tk_uporabnik = tk_uporabnik;
            Uporabnik = uporabnik;
        }

        public Trening(int id_trening, DateTime datum, TimeSpan trajanje, int porabljene_kalorije, Uporabnik? uporabnik)
        {
            this.id_trening = id_trening;
            this.datum = datum;
            this.trajanje = trajanje;
            this.porabljene_kalorije = porabljene_kalorije;
            Uporabnik = uporabnik;
        }

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
