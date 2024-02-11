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
            CreateMap<BookCategory, CategoryDto>();
            CreateMap<CategoryDto, BookCategory>();
            CreateMap<Member, MemberDto>();
            CreateMap<MemberDto, Member>();
            CreateMap<RentedBook, RentBookDto>();
            CreateMap<RentBookDto, RentedBook>();
        }
    }
}
