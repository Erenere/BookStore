using AutoMapper;
using BookStore.Application.AuthorOperations.Commands.CreateAuthor;
using BookStore.Application.AuthorOperations.Queries.GetAuthorDetail;
using BookStore.Application.AuthorOperations.Queries.GetAuthors;
using BookStore.Application.BookOperations.Commands.CreateBook;
using BookStore.Application.BookOperations.Queries.GetBook;
using BookStore.Application.BookOperations.Queries.GetBooks;
using BookStore.Application.GenreOperations.Queries.GetGenreDetail;
using BookStore.Application.GenreOperations.Queries.GetGenres;
using BookStore.BookOperations.GetBook;
using BookStore.Entities;

namespace BookStore.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Book
            CreateMap<CreateBookCommand.CreateBookModel, Book>();
            CreateMap<Book, GetBookQuery.BookViewModel>().ForMember(dest => dest.Genre,
                opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre,
                opt => opt.MapFrom(src => src.Genre.Name));
            //Genre
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
            //Author
            CreateMap<CreateAuthorModel, Author>();
            CreateMap<Author, AuthorsViewModel>().ForMember(dest => dest.Name,
                opt =>
                    opt.MapFrom(src => src.FirstName + " " + src.LastName));
            CreateMap<Author, AuthorDetailViewModel>().ForMember(dest => dest.Name,
                opt =>
                    opt.MapFrom(src => src.FirstName + " " + src.LastName));
        }
    }
}