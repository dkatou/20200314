using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Api1.Api1Model.Models;

namespace Api1.Api1Model.Data
{
    public class Api1Context : DbContext
    {
        public Api1Context(DbContextOptions<Api1Context> options)
            : base(options)
        {
        }

        public DbSet<Api1.Api1Model.Models.Blog> Blog { get; set; }

        public DbSet<Api1.Api1Model.Models.Post> Post { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>()
                .HasKey(b => new { b.BlogId });

            modelBuilder.Entity<Blog>()
                .HasMany(b => b.Posts)
                .WithOne();

            modelBuilder.Entity<Post>()
                .HasKey(p => new { p.PostId });
        }
    }
}