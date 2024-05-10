using Microsoft.EntityFrameworkCore;
using to_do_list_project.Models;

namespace to_do_list_project.Data
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<ToDo> ToDos { get; set; } = null!;
        public DbSet<Status> Statuses { get; set; } = null!;
        public DbSet<Catigory> Catigories { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Catigory>().HasData(
            new Catigory { Id = "1", Name = "Select Categories" },
            new Catigory { Id = "work", Name = "Work" },
            new Catigory { Id = "home", Name = "Home" },
            new Catigory { Id = "ex", Name = "Exercise" },
            new Catigory { Id = "shop", Name = "Shopping" },
            new Catigory { Id = "call", Name = "Contact" },
            new Catigory { Id = "others", Name = "Others" }
            );

            modelBuilder.Entity<Status>().HasData(
            new Status { Id = "open", Name = "Open" },
            new Status { Id = "closed", Name = "Completed" }
            );

            modelBuilder.Entity<User>()
                .HasMany(u => u.ToDos)
                .WithOne(td => td.User)
                .HasForeignKey(td => td.UserId);

            modelBuilder.Entity<ToDo>()
                .HasOne(td => td.Catigory)
                .WithMany(c => c.ToDos)
                .HasForeignKey(td => td.CatigoryId);

            modelBuilder.Entity<ToDo>()
                .HasOne(td => td.Status)
                .WithMany(s => s.ToDos)
                .HasForeignKey(td => td.StatusId);

            base.OnModelCreating(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}
