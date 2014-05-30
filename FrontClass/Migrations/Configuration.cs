using FrontClass.DAL;
using FrontClass.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FrontClass.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FrontClass.DAL.FrontClassContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FrontClass.DAL.FrontClassContext context)
        {
            var courseTitle = "Learning Math";

            if (!context.Courses.Any(c=>c.Title == courseTitle))
            {
                var mathCourse = new Course { Title = courseTitle, EntryCode = "welcome"};

                context.Courses.AddOrUpdate(course => course.Title,
                    mathCourse);

                var firstModule = new Module { Title = "Math Basics", Course = mathCourse };

                context.Modules.AddOrUpdate(module => module.Title,
                    firstModule,
                    new Module { Title = "Fun with Hexidecimal", Course = mathCourse },
                    new Module { Title = "Statistics and You", Course = mathCourse },
                    new Module { Title = "Calculus for Fun and Profit", Course = mathCourse }
                    );

                context.Steps.AddOrUpdate(step => step.Title,
                    new Step { Title = "Addition", Module = firstModule, Content = @"<h1>Welcome to the course!</h1><p>So, you've decided to learn math. Good on ya! You'll find that it's as easy as 1-2-3.</p><p>This course assumes that you have number recognition down pat, and understand the mechanics of counting.</p><p>Let's start with one of the most basic parts of math, addition.</p><h2>Addition</h2><p>Also known as 'adding', addition refers to computing the sum of two or more numbers.</p>" },
                    new Step { Title = "Subtraction", Module = firstModule, Content = @"<h1>This is step 2 content.</h1><p>Replace this content with your own.</p>" },
                    new Step { Title = "Multiplication", Module = firstModule, Content = @"<h1>This is step 3 content.</h1><p>Replace this content with your own.</p>" },
                    new Step { Title = "Division", Module = firstModule, Content = @"<h1>This is step 4 content.</h1><p>Replace this content with your own.</p>" },
                    new Step { Title = "Order of Operations", Module = firstModule, Content = @"<h1>This is step 5 content.</h1><p>Replace this content with your own.</p>" }
                    );

                context.SaveChanges();
            }

            
        }
    }
}
