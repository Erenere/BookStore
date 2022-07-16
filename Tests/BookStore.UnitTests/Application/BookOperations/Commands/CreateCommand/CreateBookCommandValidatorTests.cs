using System;
using BookStore.Application.BookOperations.Commands.CreateBook;
using BookStore.BookOperations.CreateBook;
using FluentAssertions;
using TestProject1.TestSetup;
using Xunit;

namespace TestProject1.Application.BookOperations.Commands.CreateCommand
{
    public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("Lord Of The Rings",0,-1)]
        [InlineData("Lord Of The Rings",0,1)]
        [InlineData("Lord Of The Rings",100,-1)]
        [InlineData("",0,-1)]
        [InlineData("",100,1)]
        [InlineData("",0,1)]
        [InlineData("L",100,1)]
        [InlineData("L",0,-1)]
        [InlineData("Lord",0,1)]
        [InlineData(" ",100,1)]
        public void WhenInvalidInputsAreGiven_ValidatorShouldBeReturnErrors(string title, int pageCount,int genreId)
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookCommand.CreateBookModel()
            {
                Title = title,
                PageCount = pageCount,
                PublishDate = DateTime.Now.Date.AddYears(-1),
                GenreId = genreId
            };
            //act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);
            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_ValidatorShouldReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookCommand.CreateBookModel()
            {
                Title = "title",
                PageCount = 100,
                PublishDate = DateTime.Now.Date,
                GenreId = 1
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        
        [Fact]
        public void WhenValidInputsAreGiven_ValidatorShouldNotReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookCommand.CreateBookModel()
            {
                Title = "title",
                PageCount = 100,
                PublishDate = DateTime.Now.Date.AddYears(-2),
                GenreId = 1
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Be(0);
        }
    }
}