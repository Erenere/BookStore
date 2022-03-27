using System;
using System.Linq;
using BookStore.BookOperations.GetBooks;
using BookStore.Common;
using BookStore.DBOperations;

namespace BookStore.BookOperations.GetBook
{
    public class GetBookQuery
    {
        private readonly BookStoreDbContext _dbContext;

        public GetBookQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int bookId { get; set; }

        public BookViewModel Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == bookId);
            if (book is null)
                throw new InvalidOperationException("Book does not exist");
            
            BookViewModel vm = new BookViewModel
            {
                Title = book.Title,
                Genre = ((GenreEnum) book.GenreId).ToString(),
                PageCount = book.PageCount,
                PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy")
            };

            return vm;
        }
        
        public class BookViewModel
        {
            public string Title { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
            public string Genre { get; set; }
        }
    }
}