using ForumManage.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumManage.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ForumContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ForumContext>>()))
            {
                // Look for any forums.
                if (context.Forums.Any() || context.Engineers.Any() || context.Cases.Any() || context.Tags.Any())
                {
                    return;   // DB has been seeded
                }

               
                context.Forums.AddRange(
                    new Forum
                    {
                        Name = "Asp.Net",                  
                    },
                    new Forum
                    {
                        Name = "Asp.Net Core",
                    },
                    new Forum
                    {
                        Name = "Azure",
                    }
                );
               
                context.Engineers.AddRange(
                    new Engineer
                    {
                        Name = "Amy",
                    },
                    new Engineer
                    {
                        Name = "Bob",
                    },
                    new Engineer
                    {
                        Name = "Zack",
                    }
                );
             
                context.Cases.AddRange(
                    new Case
                    {
                        Title = "How to Create a asp.net core app",
                        Link = "https://www.baidu.com"
                    },
                    new Case
                    {
                        Title = "How to migrate db",
                        Link = "https://www.google.com"
                    },
                    new Case
                    {
                        Title = "How to scaffold Identity",
                        Link = "https://www.qq.com"
                    }
                );
               
                context.Tags.AddRange(
                    new Tag
                    {
                        Name = "MVC",
                    },
                    new Tag
                    {
                        Name = ".net Core",
                    },
                    new Tag
                    {
                        Name = "EF Core",
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
