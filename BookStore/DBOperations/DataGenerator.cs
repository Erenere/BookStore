using System;
using System.Linq;
using BookStore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context =
                new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any()) return;

                context.Genres.AddRange(
                    new Genre
                    {
                        Name = "Personal Growth"
                    },
                    new Genre
                    {
                        Name = "Science Fiction"
                    },
                    new Genre
                    {
                        Name = "Romance"
                    }
                );

                context.Books.AddRange(
                    new Book
                    {
                        //Id = 1,
                        Title = "Lean Startup",
                        GenreId = 0, //Personal Growth,
                        PageCount = 200,
                        PublishDate = new DateTime(2001, 06, 12)
                    },
                    new Book
                    {
                        //Id = 2,
                        Title = "Herland",
                        GenreId = 1, //Science Fiction,
                        PageCount = 250,
                        PublishDate = new DateTime(2010, 05, 23)
                    },
                    new Book
                    {
                        //Id = 3,
                        Title = "Frankestein",
                        GenreId = 1, //Science Fiction
                        PageCount = 540,
                        PublishDate = new DateTime(1981, 12, 21)
                    });

                context.Authors.AddRange(
                    new Author
                    {
                        FirstName = "Albert",
                        LastName = "Camus",
                        DateOfBirth = new DateTime(1912, 10, 15)
                    },
                    new Author
                    {
                        FirstName = "Muazzez",
                        LastName = "Cig",
                        DateOfBirth = new DateTime(1922,11,25)
                    },
                    new Author
                    {
                        FirstName = "Stefan",
                        LastName = "Zweig",
                        DateOfBirth = new DateTime(1901,06,11)
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}