using System;
using System.Linq;
using BookStore.Application.GenreOperations.Commands.CreateGenre;
using BookStore.Application.GenreOperations.Commands.UpdateGenre;
using BookStore.DBOperations;
using BookStore.Entities;
using FluentAssertions;
using TestProject1.TestSetup;
using Xunit;

namespace TestProject1.Application.GenreOperations.Commands.UpdateCommand
{
    public class UpdateGenreCommandTests :IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenNonExistingGenreIdIsGiven_InvalidOperationException_ShouldBeReturned()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = -3;
            
            //act&assert
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Genre not found");
        }

        [Fact]
        public void WhenAlreadyExistingGenreNameIsGiven_InvalidOperationException_ShouldBeReturned()
        {
            //arrange
            var genre = new Genre()
            {
                Name = "Name"
            };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.Model = new CreateGenreModel() {Name = "Name"};
            
            //act & assert
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Book genre already exists.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeUpdated()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = 3;
            UpdateGenreModel model = new UpdateGenreModel() {Name = "New Name1"};
            command.Model = model;
            
            FluentActions.Invoking(()=>command.Handle()).Invoke();

            var genre = _context.Genres.SingleOrDefault(genre => genre.Id == command.GenreId);
            genre.Should().NotBeNull();
            genre.Name.Should().Be(model.Name);
        }
    }
}