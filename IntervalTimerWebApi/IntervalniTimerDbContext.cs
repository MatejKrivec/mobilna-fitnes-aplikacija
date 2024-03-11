using IntervalTimerWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace IntervalTimerWebApi
{
    public class IntervalniTimerDbContext : DbContext
    {
        public DbSet<Trening> Treningi { get; set; }
        public DbSet<Uporabnik> Uporabniki { get; set; }
        public DbSet<Vadba> Vadbe { get; set; }
        public string DbPath { get; set; }

        public IntervalniTimerDbContext()
        {
            DbPath = Path.Join(Directory.GetCurrentDirectory(), "intervalnitimer.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }
    }
}
