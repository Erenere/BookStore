using System;
using System.Linq;
using BookStore.Application.AuthorOperations.Commands.CreateAuthor;
using BookStore.DBOperations;
using BookStore.Entities;
using FluentAssertions;
using TestProject1.TestSetup;
using Xunit;

namespace TestProject1.Application.AuthorOperations.Commands.CreateCommand
{
    public class CreateAuthorCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public CreateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenAlreadyExistingAuthorNameIsGiven_InvalidOperationException_ShouldBeReturned()
        {
            //arrange
            var author = new Author()
            {
                FirstName = "FirstN",
                LastName = "LastN",
            };
            _context.Authors.Add(author);
            _context.SaveChanges();

            CreateAuthorCommand command = new CreateAuthorCommand(_context);
            command.Model = new CreateAuthorModel() {FirstName = "FirstN", LastName = "LastN",
                DateofBirth =new DateTime(1990, 01, 10)};
            //act&assert
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Author already exists.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_context);
            CreateAuthorModel model = new CreateAuthorModel()
            {
                FirstName = "NewAuthorFirst", LastName = "NewAuthorLast",
                DateofBirth = new DateTime(1980, 4, 11)
            };
            command.Model = model;
            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();
            //assert
            var author =
                _context.Authors.SingleOrDefault(x => x.FirstName + x.LastName == model.FirstName + model.LastName);
            author.Should().NotBeNull();
            author.DateOfBirth.Should().Be(model.DateofBirth);
        }
    }
}