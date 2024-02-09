using AutoMapper;

using BookLibrary.Application.Books.Dtos;
using BookLibrary.Domain;

namespace BookLibrary.Infrastructure.Mappers;

public class ApplicationAutomapperProfile : Profile
{
    public ApplicationAutomapperProfile()
    {
        CreateMap<UpdateBookDto, BookEntity>()
            .ForMember(dest => dest.Authors, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<CreateBookDto, BookEntity>()
            .ForMember(dest => dest.Authors, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors == null ? new List<string>() : src.Authors.Select(a => a.Name)));
        
        CreateMap<BookDto, BookEntity>()
            .ForMember(dest => dest.Authors, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.AuthorIds, opt => opt.MapFrom(src => src.Authors == null ? new List<int>() : src.Authors.Select(a => a.AuthorId)))
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors == null ? new List<string>() : src.Authors.Select(a => a.Name)));
    }
}
