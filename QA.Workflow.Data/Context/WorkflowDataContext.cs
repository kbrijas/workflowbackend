using Microsoft.EntityFrameworkCore;
using QA.Framework.DataEntities.Entities;
using QA.Workflow.Data.Context;

namespace QA.Framework.DataEntities
{
    public partial class WorkflowDataContext : MigrationDataContext
    {
        public WorkflowDataContext(DbContextOptions options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging(true);
        }

        public virtual DbSet<MenuMaster> MenuMaster { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<MenuMaster>(entity =>
            {
                entity.HasKey(e => e.SeqNo);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Menu)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.UisRef).HasMaxLength(200);
            });

        }
    }
}
