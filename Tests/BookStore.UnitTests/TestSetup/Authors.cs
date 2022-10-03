using System;
using BookStore.DBOperations;
using BookStore.Entities;

namespace TestProject1.TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
            context.Authors.AddRange(new Author
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
        }
    }
}