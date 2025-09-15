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

                var authors = new List<AuthorEntity>
                {
                    new AuthorEntity("Deniss", "Timcenko"),
                    new AuthorEntity("John", "Doe")
                };
                
                ctx.Authors.AddRange(authors);
                ctx.SaveChanges();
            }

            if (!ctx.Posts.Any()) {
                var authors = ctx.Authors.ToList();
                var johnId = authors.First(a => a.Name == "Deniss").Id;
                var janeId = authors.First(a => a.Name == "John").Id;
                
                var posts = new List<PostEntity>
                {
                    new PostEntity(johnId, "Denis's First Post", "Introduction post", "This is my first blog post!"),
                    new PostEntity(johnId, "Denis's Second Post", "Follow-up post", "Thanks for reading my first post!"),
                    new PostEntity(janeId, "John's Thoughts", "Technical insights", "Here are my thoughts on the latest tech trends...")
                };
                
                ctx.Posts.AddRange(posts);
                ctx.SaveChanges();
            }
        }
    }
}
