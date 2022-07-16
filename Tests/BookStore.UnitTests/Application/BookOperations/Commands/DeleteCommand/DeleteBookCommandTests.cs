using System;
using System.Linq;
using AutoMapper;
using BookStore.Application.BookOperations.Commands.DeleteBook;
using BookStore.DBOperations;
using FluentAssertions;
using TestProject1.TestSetup;
using Xunit;

namespace TestProject1.Application.BookOperations.Commands.DeleteCommand
{
    public class DeleteBookCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public DeleteBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotExistingBookIdGiven_InvalidOperationException_ShouldBeReturned()
        {
            //arrange
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.bookId = Int32.MaxValue;
            
            //act&assert
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Book not found");
        }

        [Fact]
        public void WhenValidBookIdIsGiven_Book_ShouldBeDeleted()
        {
            //arrange
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.bookId = 1;
            //act
            FluentActions.Invoking(()=>command.Handle()).Invoke();
            
            //assert
            var book = _context.Books.SingleOrDefault(book => book.Id == command.bookId);
            book.Should().BeNull();
        }
    }
}