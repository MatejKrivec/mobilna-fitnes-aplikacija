using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMR_projekt.Models
{
    public class Uporabnik
    {
        public int id_uporabnik { get; set; }
        public string ime { get; set; }
        public string priimek { get; set; }
        public string uporabnisko_ime { get; set; }
        public string eposta { get; set; }
        public string geslo { get; set; }
        public DateTime datum_registracije { get; set; } = DateTime.Now;


        public Uporabnik(int id_uporabnik, string ime, string priimek, string uporabnisko_ime, string eposta, string geslo, DateTime datum_registracije)
        {
            this.id_uporabnik = id_uporabnik;
            this.ime = ime;
            this.priimek = priimek;
            this.uporabnisko_ime = uporabnisko_ime;
            this.eposta = eposta;
            this.geslo = geslo;
            this.datum_registracije = datum_registracije;
        }

        public Uporabnik()
        {
        }
    }
}
