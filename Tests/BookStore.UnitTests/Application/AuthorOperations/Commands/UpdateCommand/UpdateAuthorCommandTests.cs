using System;
using System.Linq;
using BookStore.Application.AuthorOperations.Commands.CreateAuthor;
using BookStore.Application.AuthorOperations.Commands.UpdateAuthor;
using BookStore.DBOperations;
using BookStore.Entities;
using FluentAssertions;
using TestProject1.TestSetup;
using Xunit;

namespace TestProject1.Application.AuthorOperations.Commands.UpdateCommand
{
    public class UpdateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenNonExistingAuthorIdIsGiven_InvalidOperationException_ShouldBeReturned()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = -3;

            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Author does not exist.");
        }

        [Fact]
        public void WhenAlreadyExistingAuthorNameIsGiven_InvalidOperationException_ShouldBeReturned()
        {
            var author = new Author()
            {
                FirstName = "Fname",
                LastName = "Lname"
            };
            _context.Authors.Add(author);
            _context.SaveChanges();

            CreateAuthorCommand command = new CreateAuthorCommand(_context);
            command.Model = new CreateAuthorModel() {FirstName = "Fname", LastName = "Lname"};

            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Author already exists.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_AuthorShouldBeUpdated()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = 1;
            UpdateAuthorModel model = new UpdateAuthorModel() {FirstName = "Examplef", LastName = "Examplel"};
            command.Model = model;
            

            FluentActions.Invoking(()=>command.Handle()).Invoke();

            var author = _context.Authors.SingleOrDefault(a => a.Id == command.AuthorId);
            author.Should().NotBeNull();
            author.FirstName.Should().Be(model.FirstName);
            author.LastName.Should().Be(model.LastName);
            author.DateOfBirth.Should().Be(model.DateOfBirth);
        }
    }
}