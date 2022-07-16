using BookStore.Application.BookOperations.Commands.DeleteBook;
using BookStore.BookOperations.DeleteBook;
using FluentAssertions;
using TestProject1.TestSetup;
using Xunit;

namespace TestProject1.Application.BookOperations.Commands.DeleteCommand
{
    public class DeleteBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidBookIdIsGiven_ValidatorShouldReturnError()
        {
            //arrange
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.bookId = -10;
            //act
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);
            
            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidBookIdIsGiven_ValidatorShouldNotReturnError()
        {
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.bookId = 1;

            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Be(0);
        }
    }
}