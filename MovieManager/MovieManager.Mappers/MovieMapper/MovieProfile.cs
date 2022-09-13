using AutoMapper;
using MovieManager.Domain.Models;
using MovieManager.ServiceModels.MovieServiceModels;

namespace MovieManager.Mappers.MovieMapper;
public class MovieProfile : Profile
{
    public MovieProfile()
    {
        CreateMap<Movie, MovieDto>()
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.ToString())).ReverseMap();

        CreateMap<CreateMovieDto, Movie>()
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre)).ReverseMap();
    }
}
