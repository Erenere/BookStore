using System;
using System.Linq;
using AutoMapper;
using BookStore.Application.BookOperations.Commands.UpdateBook;
using BookStore.DBOperations;
using FluentAssertions;
using TestProject1.TestSetup;
using Xunit;

namespace TestProject1.Application.BookOperations.Commands.UpdateCommand
{
    public class UpdateBookCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotExistingBookIdGiven_InvalidOperationException_ShouldBeReturned()
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.bookId = Int32.MaxValue;
            
            //act&assert
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Book does not exist");
        }

        [Fact]
        public void WhenValidBookIdIsGiven_Book_ShouldBeUpdated()
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.bookId = 1;
            UpdateBookCommand.UpdateBookModel model = new UpdateBookCommand.UpdateBookModel()
                {Title = "Test Title", GenreId = 1};
            command.Model = model;
            //act
            FluentActions.Invoking(()=>command.Handle()).Invoke();
            
            //assert
            var book = _context.Books.SingleOrDefault(book => book.Id == command.bookId);
            book.Should().NotBeNull();
            book.GenreId.Should().Be(model.GenreId);
            book.Title.Should().Be(model.Title);
        }
    }
}