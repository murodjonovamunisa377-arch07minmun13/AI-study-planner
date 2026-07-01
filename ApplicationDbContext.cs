using Microsoft.EntityFrameworkCore;
using AIStudyPlanner.Models;

namespace AIStudyPlanner.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<StudyPlan> StudyPlans { get; set; }
        public DbSet<StudyTask> StudyTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Cascade Deletes: If a StudyPlan is deleted, remove all its Tasks
            modelBuilder.Entity<StudyPlan>()
                .HasMany(p => p.StudyTasks)
                .WithOne(t => t.StudyPlan)
                .HasForeignKey(t => t.StudyPlanId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
