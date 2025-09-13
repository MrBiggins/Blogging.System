using Blogging.System.Domain.Entites;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Blogging.System.Infrastructure.Extentions {
    public static class BlogDataSeederExtensions {
        public static void SeedBlogData(this IServiceProvider serviceProvider) {
            using var scope = serviceProvider.CreateScope();
            var ctx = scope.ServiceProvider.GetRequiredService<BlogSystemDbContext>();

            ctx.Database.EnsureCreated();

            if (!ctx.Authors.Any()) {
                ctx.Authors.AddRange(
                    new AuthorEntity("John", "Doe"),
                    new AuthorEntity("Jane", "Smith")
                );
                ctx.SaveChanges();
            }

            if (!ctx.Posts.Any()) {
                var firstAuthorId = ctx.Authors.First().Id;
                ctx.Posts.Add(new PostEntity(
                    firstAuthorId,
                    "Runtime Seeded Post",
                    "Seeded at startup",
                    "This post was added by SeedBlogData."
                ));
                ctx.SaveChanges();
            }
        }
    }
}
