using CarFuel.Models;
using System.Data.Entity;

namespace CarFuel.DataAccess.Context {
    public class CarDb : DbContext {

        public DbSet<Car> Cars { get; set; }
        public DbSet<FillUp> FillUps { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {

            modelBuilder.Entity<FillUp>()
              .HasOptional(f => f.NextFillUp)
              .WithOptionalPrincipal(f => f.PreviousFillUp);

        }

    }
}