using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Framework
{
    public class DataBase : DbContext
    {
        public DbSet<Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(@"Data Source=tasks.db3;", options =>
            {
                options.MigrationsAssembly(Directory.GetCurrentDirectory());
            });

            base.OnConfiguring(options);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
