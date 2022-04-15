using System;
using System.Linq;
using AutoMapper;
using BookStore.BookOperations.GetBooks;
using BookStore.Common;
using BookStore.DBOperations;
using Microsoft.EntityFrameworkCore;

namespace BookStore.BookOperations.GetBook
{
    public class GetBookQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetBookQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public int bookId { get; set; }

        public BookViewModel Handle()
        {
            var book = _dbContext.Books.Include(x=>x.Genre).SingleOrDefault(x => x.Id == bookId);
            if (book is null)
                throw new InvalidOperationException("Book does not exist");

            BookViewModel vm = _mapper.Map<BookViewModel>(book);

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