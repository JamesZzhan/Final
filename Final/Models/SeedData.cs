using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Final.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new FinalContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<FinalContext>>()))
            {
                // Look for any Books.
                if (context.Book.Any())
                {
                    return;   // DB has been seeded
                }

                context.Book.AddRange(
                    new Book
                    {
                        BookName = "Operating Systems Internals and Design Principles 9th Edition by William Stallings",
                        CourseName = "Operating Systems",
                        CRN = "CSC 350",
                        Major = "CS",
                        ISBN = 123456789,
                        Price = 85.89M
                    },

                   

                    new Book
                    {
                        BookName = "Multivariable Calculus, 7th Edition by James Stewart",
                        CourseName = "Calculus 3",
                        CRN = "Math 203",
                        Major = "Math",
                        ISBN = 123456789,
                        Price = 7.99M
                    }
                );
                context.SaveChanges();
            }
        }
    }
}

