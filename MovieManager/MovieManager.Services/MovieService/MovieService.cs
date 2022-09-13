using AutoMapper;
using FluentValidation;
using MovieManager.DataAccess.Repositories.Interfaces;
using MovieManager.Domain.Enums;
using MovieManager.Domain.Models;
using MovieManager.ServiceModels.Enums;
using MovieManager.ServiceModels.MovieServiceModels;
using MovieManager.Services.Interfaces;

namespace MovieManager.Services.MovieService;
public class MovieService : IMovieService
{
    private readonly IRepository<Movie> _movieRepository;
    private readonly IValidator<CreateMovieDto> _createMovieValidator;
    private readonly IMapper _mapper;

    public MovieService(IRepository<Movie> movieRepository, IMapper mapper, IValidator<CreateMovieDto> createMovieValidator)
    {
        _movieRepository = movieRepository;
        _mapper = mapper;
        _createMovieValidator = createMovieValidator;
    }


    public IEnumerable<MovieDto> GetAllMovies()
    {
        var moviesFromDb = _movieRepository.GetAll();
        if (moviesFromDb is null)
        {
            throw new Exception("There aren't any available movies.");
        }
        var mappedMovies = moviesFromDb.Select(_mapper.Map<Movie, MovieDto>);
        return mappedMovies;
    }

    public MovieDto GetById(int id)
    {
        var movieFromDb = _movieRepository.GetById(id);
        ArgumentNullException.ThrowIfNull(movieFromDb);
        var mappedMovie = _mapper.Map<MovieDto>(movieFromDb);
        return mappedMovie;
    }

    public IEnumerable<MovieDto> FilterByGenre(string genre)
    {
        var moviesFromDb = _movieRepository.Filter(x => x.Genre.ToString() == genre);
        if (moviesFromDb is null)
        {
            throw new Exception($"There aren't any movies with genre {genre}!");
        }
        var mappedMovies = moviesFromDb.Select(_mapper.Map<Movie, MovieDto>);
        return mappedMovies;
    }

    public IEnumerable<MovieDto> FilterByYear(int year)
    {
        var moviesFromDb = _movieRepository.Filter(x => x.Year == year);
        if (moviesFromDb is null)
        {
            throw new Exception($"There aren't any movies from year {year}!");
        }
        var mappedMovies = moviesFromDb.Select(_mapper.Map<Movie, MovieDto>);
        return mappedMovies;
    }

    public IEnumerable<MovieDto> OrderMoviesBy(MovieOrderBy orderBy)
    {
        var moviesFromDb = _movieRepository.GetAll().Select(_mapper.Map<Movie, MovieDto>);
        return orderBy switch
        {
            MovieOrderBy.title_asc => moviesFromDb.OrderBy(x => x.Title),
            MovieOrderBy.title_desc => moviesFromDb.OrderByDescending(x => x.Title),
            MovieOrderBy.genre_asc => moviesFromDb.OrderBy(x => x.Genre),
            MovieOrderBy.genre_desc => moviesFromDb.OrderByDescending(x => x.Genre),
            MovieOrderBy.year_asc => moviesFromDb.OrderBy(x => x.Year),
            MovieOrderBy.year_desc => moviesFromDb.OrderByDescending(x => x.Year),
            _ => throw new NotImplementedException($@"There is no ordering by ""{orderBy}"" available!")
        };
    }

    public void CreateNewMovie(CreateMovieDto request)
    {
        var moviesFromDb = _movieRepository.Filter(x => x.Title.Equals(request.Title, StringComparison.InvariantCultureIgnoreCase));
        if (moviesFromDb is not null)
        {
            throw new Exception($"There is already a movie with title {request.Title} in the collection!");
        }
        _createMovieValidator.ValidateAndThrow(request);
        var movie = _mapper.Map<Movie>(request);
        _movieRepository.Insert(movie);
    }

    public void UpdateMovie(MovieDto request)
    {
        var movieFromDb = _movieRepository.GetById(request.Id);
        ArgumentNullException.ThrowIfNull(movieFromDb);
        _mapper.Map(request, movieFromDb);
        _movieRepository.Update(movieFromDb);
    }

    public void DeleteMovie(int id)
    {
        var movieFromDb = _movieRepository.GetById(id);
        ArgumentNullException.ThrowIfNull(movieFromDb);
        _movieRepository.Delete(movieFromDb);
    }
}
