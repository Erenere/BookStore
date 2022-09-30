using System;
using System.Linq;
using BookStore.Application.GenreOperations.Commands.CreateGenre;
using BookStore.DBOperations;
using BookStore.Entities;
using FluentAssertions;
using TestProject1.TestSetup;
using Xunit;

namespace TestProject1.Application.GenreOperations.Commands.CreateCommand
{
    public class CreateGenreCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public CreateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenAlreadyExistingGenreNameIsGiven_InvalidOperationException_ShouldBeReturned()
        {
            //act
            var genre = new Genre()
            {
                Name = "NewName",
            };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.Model = new CreateGenreModel() {Name = "NewName"};
            
            //act & assert
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Book genre already exists.");
        }

        [Fact]
        public void WhenValidInputIsGiven_GenreShouldBeCreated()
        {
            CreateGenreCommand command = new CreateGenreCommand(_context);
            CreateGenreModel model = new CreateGenreModel() {Name = "Test Name"};

            command.Model = model;
            
            //act
            FluentActions.Invoking(()=>command.Handle()).Invoke();
            
            //assert
            var genre = _context.Genres.FirstOrDefault(x => x.Name == model.Name);
            genre.Should().NotBeNull();
        }
    }
}