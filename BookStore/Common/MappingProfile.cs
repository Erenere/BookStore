using AutoMapper;
using BookStore.BookOperations.CreateBook;
using BookStore.BookOperations.GetBook;
using BookStore.BookOperations.GetBooks;

namespace BookStore.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookCommand.CreateBookModel, Book>();
            CreateMap<Book, GetBookQuery.BookViewModel>().ForMember(dest => dest.Genre,
                opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre,
                opt => opt.MapFrom(src => ((GenreEnum) src.GenreId).ToString()));
        }
    }
}