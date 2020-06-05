using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FullSearchApp.Models
{
    public class MyDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder
        .UseNpgsql("User ID=postgres;Password=delafqm;Host=localhost;Port=5432;Database=FullSearch;Pooling=true;")
        //.UseLoggerFactory(PgFtSearch.Program.MyLoggerFactory)
        .UseSnakeCaseNamingConvention();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Article>().HasIndex(p => p.TitleVector).HasMethod("GIN");
            modelBuilder.Entity<Article>().HasIndex(p => p.AbstVector).HasMethod("GIN");
        }

        public DbSet<Article> Articles { get; set; }
    }
}
