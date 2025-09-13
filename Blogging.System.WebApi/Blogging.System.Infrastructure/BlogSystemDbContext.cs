using Blogging.System.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace Blogging.System.Infrastructure {
    public class BlogSystemDbContext : DbContext {

        public BlogSystemDbContext(DbContextOptions<BlogSystemDbContext> options) : base(options) { }

        public DbSet<AuthorEntity> Authors => Set<AuthorEntity>();
        public DbSet<PostEntity> Posts => Set<PostEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<AuthorEntity>(b => {
                b.HasKey(a => a.Id);
                b.Property(a => a.Id).ValueGeneratedOnAdd();
                b.Property(a => a.Name).IsRequired().HasMaxLength(100);
                b.Property(a => a.Surname).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<PostEntity>(b => {
                b.HasKey(p => p.Id);
                b.Property(p => p.Id).ValueGeneratedOnAdd();
                b.Property(p => p.Title).IsRequired().HasMaxLength(200);
                b.Property(p => p.Description).HasMaxLength(500);
                b.Property(p => p.Content).IsRequired();
                b.HasIndex(p => p.AuthorId);
            });
        }
    }
}
