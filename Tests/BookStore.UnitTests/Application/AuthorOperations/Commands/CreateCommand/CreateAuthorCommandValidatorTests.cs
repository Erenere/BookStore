using System;
using BookStore.Application.AuthorOperations.Commands.CreateAuthor;
using FluentAssertions;
using TestProject1.TestSetup;
using Xunit;

namespace TestProject1.Application.AuthorOperations.Commands.CreateCommand
{
    public class CreateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("a","aaa")]
        [InlineData("aa","a")]
        [InlineData("","")]
        [InlineData("a","")]
        [InlineData("","ddd")]
        public void WhenInvalidInputsAreGiven_ValidatorShouldReturnErrors(string firstName, string lastName)
        {
            CreateAuthorCommand command = new CreateAuthorCommand(null);
            command.Model = new CreateAuthorModel()
            {
                FirstName = firstName,
                LastName = lastName,
                DateofBirth = DateTime.Now.AddYears(-1)
            };
            //act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);
            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_ValidatorShouldReturnError()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(null);
            command.Model = new CreateAuthorModel()
            {
                FirstName = "FirstName",
                LastName = "LastName",
                DateofBirth = DateTime.Now.Date
            };

            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_ValidatorShouldNotReturnError()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(null);
            command.Model = new CreateAuthorModel()
            {
                FirstName = "FirstName",
                LastName = "LastName",
                DateofBirth = DateTime.Now.Date.AddYears(-1)
            };
            
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Be(0);
        }
    }
}