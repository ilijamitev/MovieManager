using MovieManager.ServiceModels.Enums;
using MovieManager.ServiceModels.MovieServiceModels;

namespace MovieManager.Services.Interfaces;

public interface IMovieService
{
    MovieDto GetById(int id);
    IEnumerable<MovieDto> GetAllMovies();
    IEnumerable<MovieDto> OrderMoviesBy(MovieOrderBy orderBy);
    IEnumerable<MovieDto> FilterByYear(int year);
    IEnumerable<MovieDto> FilterByGenre(string genre);
    void CreateNewMovie(CreateMovieDto entity);
    void UpdateMovie(MovieDto entity);
    void DeleteMovie(int id);
}
