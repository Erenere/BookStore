using BookStore.Application.BookOperations.Queries.GetBook;
using BookStore.BookOperations.GetBook;
using FluentAssertions;
using TestProject1.TestSetup;
using Xunit;

namespace TestProject1.Application.BookOperations.Queries.GetBook
{
    public class GetBookQueryValidatorTests: IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidBookIdIsGiven_ValidatorShouldReturnError()
        {
            //arrange
            GetBookQuery query = new GetBookQuery(null,null);
            query.bookId = -1;
            //act
            GetBookQueryValidator validator = new GetBookQueryValidator();
            var result = validator.Validate(query);
            
            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        
        [Fact]
        public void WhenValidBookIdIsGiven_ValidatorShouldNotReturnError()
        {
            GetBookQuery query = new GetBookQuery(null,null);
            query.bookId = 1;

            GetBookQueryValidator validator = new GetBookQueryValidator();
            var result = validator.Validate(query);
            result.Errors.Count.Should().Be(0);
        }
    }
}