using BookStore.Application.GenreOperations.Queries.GetGenreDetail;
using FluentAssertions;
using TestProject1.TestSetup;
using Xunit;

namespace TestProject1.Application.GenreOperations.Queries.GetGenre
{
    public class GetGenreDetailQueryValidatorTests: IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidGenreIdIsGiven_ValidatorShouldReturnError()
        {
            //arrange
            GetGenreDetailQuery query = new GetGenreDetailQuery(null, null);
            query.GenreId = -1;
            //act
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            var result = validator.Validate(query);
            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidGenreIdIsGiven_ValidatorShouldNotReturnError()
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(null, null);
            query.GenreId = 2;

            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            var result = validator.Validate(query);
            result.Errors.Count.Should().Be(0);
        }
    }
}