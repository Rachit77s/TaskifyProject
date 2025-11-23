using Microsoft.EntityFrameworkCore;
using TaskifyProject.Models.Entities;

namespace TaskifyProject.Data
{
    /// <summary>
    /// Database context for the Taskify application
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the ApplicationDbContext class
        /// </summary>
        /// <param name="options">The options to be used by the DbContext</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the DbSet for Tasks
        /// </summary>
        public DbSet<TaskItem> Tasks { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for Users
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Configures the database model and relationships
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();

                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");

                // One user can have many tasks
                entity.HasMany(e => e.Tasks)
                    .WithOne(e => e.User)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // TaskItem configuration
            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");

                entity.HasIndex(e => e.Status);
                entity.HasIndex(e => e.Priority);
                entity.HasIndex(e => e.DueDate);
                entity.HasIndex(e => e.UserId);
            });
        }
    }
}
