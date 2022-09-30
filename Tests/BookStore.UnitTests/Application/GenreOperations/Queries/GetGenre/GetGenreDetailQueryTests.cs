using System;
using System.Linq;
using AutoMapper;
using BookStore.Application.GenreOperations.Queries.GetGenreDetail;
using BookStore.DBOperations;
using BookStore.Entities;
using FluentAssertions;
using TestProject1.TestSetup;
using Xunit;

namespace TestProject1.Application.GenreOperations.Queries.GetGenre
{
    public class GetGenreQueryTests: IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenreQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNonExistingGenreIdIsGiven_InvalidOperationException_ShouldBeReturned()
        {
            //arrange
            var query = new GetGenreDetailQuery(_context, _mapper);
            query.GenreId = -2;
            //act & assert
            FluentActions.Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Genre not found");
        }

        [Fact]
        public void WhenValidGenreIdIsGiven_Genre_ShouldBeReturned()
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            var genreId = 1;
            query.GenreId = genreId;
            //act
            Genre genre = _context.Genres.SingleOrDefault(x => x.Id == genreId);
            genre.Should().NotBeNull();
        }
    }
}