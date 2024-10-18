using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Book { get; set; }
        public DbSet<ChangeHistory> ChangeHistories { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<ChangeHistory>()
        //    .HasKey(ch => ch.Id); 

        //    modelBuilder.Entity<ChangeHistory>()
        //        .Property(ch => ch.Id)
        //        .ValueGeneratedOnAdd();
        //}
    }
}
