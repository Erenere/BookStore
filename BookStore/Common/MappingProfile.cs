using AutoMapper;
using BookStore.Application.GenreOperations.Queries.GetGenreDetail;
using BookStore.Application.GenreOperations.Queries.GetGenres;
using BookStore.BookOperations.CreateBook;
using BookStore.BookOperations.GetBook;
using BookStore.BookOperations.GetBooks;
using BookStore.Entities;

namespace BookStore.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookCommand.CreateBookModel, Book>();
            CreateMap<Book, GetBookQuery.BookViewModel>().ForMember(dest => dest.Genre,
                opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre,
                opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
        }
    }
}