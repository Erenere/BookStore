using BookStore.Application.BookOperations.Commands.CreateBook;
using BookStore.Application.BookOperations.Commands.UpdateBook;
using BookStore.BookOperations.UpdateBook;
using FluentAssertions;
using TestProject1.TestSetup;
using Xunit;

namespace TestProject1.Application.BookOperations.Commands.UpdateCommand
{
    public class UpdateBookCommandValidatorTests: IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("aaa",2,-1)]
        [InlineData("aaa",0,1)]
        [InlineData("aaa",0,-1)]
        [InlineData("aa",0,-1)]
        [InlineData("aa",2,-1)]
        [InlineData("",-1,1)]
        [InlineData("",-1,-1)]
        [InlineData("",1,-1)]
        public void WhenInvalidInputsAreGiven_ValidatorShouldReturnErrors(string title, int bookId, int genreId)
        {
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.bookId = bookId;
            command.Model = new UpdateBookCommand.UpdateBookModel()
            {
                Title = title,
                GenreId = genreId
            };

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        
        [Fact]
        public void WhenValidInputsAreGiven_ValidatorShouldNotReturnError()
        {
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.bookId = 1;
            command.Model = new UpdateBookCommand.UpdateBookModel()
            {
                Title = "title",
                GenreId = 1
            };

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Be(0);
        }
    }
}