using System;
using System.Linq;
using AutoMapper;
using BookStore.Application.BookOperations.Queries.GetBook;
using BookStore.DBOperations;
using BookStore.Entities;
using FluentAssertions;
using TestProject1.TestSetup;
using Xunit;

namespace TestProject1.Application.BookOperations.Queries.GetBook
{
    public class GetBookQueryTests: IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBookQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenInvalidBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            var query = new GetBookQuery(_context, _mapper);
            query.bookId = 0;
            //act & assert
            FluentActions.Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Book does not exist");
        }

        [Fact]
        public void WhenValidBookIdIsGiven_BookDetail_ShouldBeReturn()
        {
            //arrange
            GetBookQuery query = new GetBookQuery(_context,_mapper);
            var bookId = 1;
            query.bookId = bookId;
            //act
            Book book = _context.Books.FirstOrDefault(x => x.Id == bookId);
            //assert
            book.Should().NotBeNull();
        }
    }
}