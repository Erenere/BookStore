using System;
using System.Linq;
using AutoMapper;
using BookStore.Application.BookOperations.Commands.CreateBook;
using BookStore.DBOperations;
using BookStore.Entities;
using FluentAssertions;
using TestProject1.TestSetup;
using Xunit;

namespace TestProject1.Application.BookOperations.Commands.CreateCommand
{
    public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        
        [Fact]
        public void WhenAlreadyExistingBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            var book = new Book
            {
                Title = "WhenAlreadyExistingBookTitleIsGiven_InvalidOperationException_ShouldBeReturn", PageCount = 100,
                PublishDate = new DateTime(1990, 01, 10), GenreId = 1
            };
            _context.Books.Add(book);
            _context.SaveChanges();

            CreateBookCommand command = new(_context, _mapper);
            command.Model = new CreateBookCommand.CreateBookModel {Title = book.Title};

            //act & assert
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Book already exists");
        }
        
        [Fact]
        public void WhenValidInputsAreGiven_BookShouldBeCreated()
        {
            CreateBookCommand command = new(_context, _mapper);
            CreateBookCommand.CreateBookModel model = new CreateBookCommand.CreateBookModel()
                {Title = "Hobbit", PageCount = 1000, PublishDate = DateTime.Now.Date.AddYears(-10), GenreId = 1};

            command.Model = model;
            
            //act
            FluentActions.Invoking(()=>command.Handle()).Invoke();
            
            //assert
            var book = _context.Books.SingleOrDefault(book => book.Title == model.Title);
            
            book.Should().NotBeNull();
            book.PageCount.Should().Be(model.PageCount);
            book.PublishDate.Should().Be(model.PublishDate);
            book.GenreId.Should().Be(model.GenreId);
        }
    }
}