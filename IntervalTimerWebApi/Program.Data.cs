using IntervalTimerWebApi.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace IntervalTimerWebApi
{
    public partial class Program
    {
        static void Data(WebApplication app)
        {
            // Trening zahteve

            app.MapGet("/trening", () =>
            {
                IntervalniTimerDbContext db = new IntervalniTimerDbContext();
                List<Trening> a = db.Treningi.ToList();

                return a;
            });

            app.MapGet("/trening/{id}", (int id) =>
            {
                IntervalniTimerDbContext db = new IntervalniTimerDbContext();
                List<Trening> a = db.Treningi.Where(x => x.id_trening == id).ToList();

                return a;
            });

            app.MapPost("/trening", (Trening trening) =>
            {
                IntervalniTimerDbContext db = new IntervalniTimerDbContext();
                db.Treningi.Add(trening);
                db.SaveChanges();


                return Results.Created("/trening/" + trening.id_trening, trening);
            });

            // Vadba zahteve

            app.MapGet("/vadba", () =>
            {
                IntervalniTimerDbContext db = new IntervalniTimerDbContext();
                List<Vadba> a = db.Vadbe.ToList();

                return a;
            });

            app.MapGet("/vadba/{id}", (int id) =>
            {
                IntervalniTimerDbContext db = new IntervalniTimerDbContext();
                List<Vadba> a = db.Vadbe.Where(x => x.id_vadba == id).ToList();

                return a;
            });

            app.MapPost("/vadba", (Vadba vadba) =>
            {
                IntervalniTimerDbContext db = new IntervalniTimerDbContext();
                db.Vadbe.Add(vadba);
                db.SaveChanges();


                return Results.Created("/vadba/" + vadba.id_vadba, vadba);
            });

            // Uporabnik zahteve

            app.MapGet("/uporabnik", () =>
            {
                IntervalniTimerDbContext db = new IntervalniTimerDbContext();
                List<Uporabnik> a = db.Uporabniki.ToList();

                return a;
            });

            app.MapGet("/uporabnik/{id}", (int id) =>
            {
                IntervalniTimerDbContext db = new IntervalniTimerDbContext();
                List<Uporabnik> a = db.Uporabniki.Where(x => x.id_uporabnik == id).ToList();

                return a;
            });

            app.MapPost("/uporabnikPost", (Uporabnik uporabnik) =>
            {
                IntervalniTimerDbContext db = new IntervalniTimerDbContext();
                db.Uporabniki.Add(uporabnik);
                db.SaveChanges();


                return Results.Created("/uporabnik/" + uporabnik.id_uporabnik, uporabnik);
            });
        }
    }
}