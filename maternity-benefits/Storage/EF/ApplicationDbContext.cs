using Microsoft.EntityFrameworkCore;

using EFModels = maternity_benefits.Storage.EF.Models;

namespace maternity_benefits.Storage.EF
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<EFModels.MaternityBenefitsSimulation>()
                .HasOne(e => e.BaseCase)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<EFModels.MaternityBenefitsSimulation>()
                .HasOne(e => e.VariantCase)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<EFModels.MaternityBenefitsSimulationResult>()
                .HasOne(e => e.Simulation)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<EFModels.MaternityBenefitsSimulationResult>()
                .HasMany(e => e.PersonResults)
                .WithOne(f => f.SimulationResult)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<EFModels.MaternityBenefitsPersonResult>()
                .HasOne(e => e.Person)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<EFModels.MaternityBenefitsPerson> Persons { get; set; }
        public DbSet<EFModels.MaternityBenefitsSimulation> Simulations { get; set; }
        public DbSet<EFModels.MaternityBenefitsSimulationResult> SimulationResults { get; set; }
    }
}