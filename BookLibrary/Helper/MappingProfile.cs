using AutoMapper;
using BookLibrary.Dto;
using BookLibrary.Models;

namespace BookLibrary.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDto>();
            CreateMap<BookAuthor, AuthorDto>();
            CreateMap<AuthorDto, BookAuthor>();
        }
    }
}
