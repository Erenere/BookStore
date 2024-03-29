﻿using System;
using System.Linq;
using BookStore.DBOperations;
using BookStore.Entities;

namespace BookStore.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand
    {
        private readonly IBookStoreDbContext _context;

        public CreateAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public CreateAuthorModel Model { get; set; }

        public void Handle()
        {
            var author =
                _context.Authors.SingleOrDefault(x => x.FirstName + x.LastName == Model.FirstName + Model.LastName);
            if (author is not null)
                throw new InvalidOperationException("Author already exists.");

            author = new Author();
            author.FirstName = Model.FirstName;
            author.LastName = Model.LastName;
            author.DateOfBirth = Model.DateofBirth;
            _context.Authors.Add(author);
            _context.SaveChanges();
        }
    }

    public class CreateAuthorModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateofBirth { get; set; }
    }
}