using System;
using BookStore.Application.AuthorOperations.Commands.UpdateAuthor;
using FluentAssertions;
using TestProject1.TestSetup;
using Xunit;

namespace TestProject1.Application.AuthorOperations.Commands.UpdateCommand
{
    public class UpdateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0,"a", "b")]
        [InlineData(0,"aaa",  "bbb")]
        [InlineData(0,"a",  "bbb")]
        [InlineData(0,"aaa",  "")]
        [InlineData(1,"",  "bbb")]
        [InlineData(1,"aaa",  "")]
        [InlineData(1,"",  "")]
        public void WhenInvalidInputsAreGiven_ValidatorShouldReturnErrors(int authorId, string fname, string lname)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.AuthorId = authorId;
            command.Model = new UpdateAuthorModel()
            {
                FirstName = fname,
                LastName = lname,
                DateOfBirth = DateTime.Now.Date.AddYears(-1),
            };

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_ValidatorShouldReturnError()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.AuthorId = 1;
            command.Model = new UpdateAuthorModel()
            {
                FirstName = "firstname",
                LastName = "lastname",
                DateOfBirth = DateTime.Now.Date
            };

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.AuthorId = 1;
            command.Model = new UpdateAuthorModel()
            {
                FirstName = "firstname",
                DateOfBirth = DateTime.Now.Date.AddYears(-1),
                LastName = "lastname",
            };

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Be(0);
        }
    }
}