using System;
using System.Linq;
using BookStore.DBOperations;

namespace BookStore.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        private readonly IBookStoreDbContext _context;

        public UpdateAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public int AuthorId { get; set; }
        public UpdateAuthorModel Model { get; set; }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if (author is null)
                throw new InvalidOperationException("Author does not exist.");

            if (_context.Authors.Any(x =>
                (x.FirstName + x.LastName).ToLower() == (Model.FirstName + Model.LastName).ToLower() 
                && x.Id != AuthorId))
                throw new InvalidOperationException("Author already exists");

            author.FirstName = string.IsNullOrEmpty(Model.FirstName.Trim()) ? author.FirstName : Model.FirstName;
            author.LastName = string.IsNullOrEmpty(Model.LastName.Trim()) ? author.LastName : Model.LastName;
            author.DateOfBirth = Model.DateOfBirth != default ? author.DateOfBirth : Model.DateOfBirth;
                
            // author.FirstName = Model.FirstName;
            // author.LastName = Model.LastName;
            //author.DateOfBirth = Model.DateOfBirth;
            _context.SaveChanges();
        }
    }

    public class UpdateAuthorModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}