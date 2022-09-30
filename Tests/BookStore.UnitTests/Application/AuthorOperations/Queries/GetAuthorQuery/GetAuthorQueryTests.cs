using System;
using System.Linq;
using AutoMapper;
using BookStore.Application.AuthorOperations.Queries.GetAuthorDetail;
using BookStore.DBOperations;
using BookStore.Entities;
using FluentAssertions;
using TestProject1.TestSetup;
using Xunit;

namespace TestProject1.Application.AuthorOperations.Queries.GetAuthorQuery
{
    public class GetAuthorQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        
        public GetAuthorQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNonExistingAuthorIdIsGiven_InvalidOperationException_ShouldBeReturned()
        {
            var query = new GetAuthorDetailQuery(_context, _mapper);
            query.AuthorId = -2;

            FluentActions.Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Author does not exist");
        }

        [Fact]
        public void WhenValidAuthorIdIsGiven_Author_ShouldBeReturned()
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            var authorId = 1;
            query.AuthorId = authorId;
            
            Author author = _context.Authors.SingleOrDefault(x => x.Id == authorId);
            author.Should().NotBeNull();
        }
    }
}