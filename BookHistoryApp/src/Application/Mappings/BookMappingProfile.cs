using AutoMapper;
using Application.DTOs;
using Domain.Entities;

namespace Application.Mappings
{
    public class BookMappingProfile : Profile
    {
        public BookMappingProfile()
        {
            CreateMap<Book, BookViewDTO>();
            CreateMap<BookDTO, Book>();
        }
    }
}
