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
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<EFModels.MaternityBenefitsSimulation>()
                .HasOne(e => e.VariantCase)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
        }

        public DbSet<EFModels.MaternityBenefitsPerson> Persons { get; set; }
        public DbSet<EFModels.MaternityBenefitsSimulation> Simulations { get; set; }
        public DbSet<EFModels.MaternityBenefitsSimulationResult> SimulationResults { get; set; }
    }
}