using System;
using Microsoft.EntityFrameworkCore;
using WebAPIData.Model;

namespace WebAPIData
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
        }

        public DbSet<Student> Students { get; set; }
    }
}