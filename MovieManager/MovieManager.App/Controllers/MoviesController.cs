using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieManager.ServiceModels.Enums;
using MovieManager.ServiceModels.MovieServiceModels;
using MovieManager.Services.Interfaces;
using Serilog;

namespace MovieManager.App.Controllers;

public class MoviesController : BaseApiController
{
    private readonly IMovieService _movieService;

    public MoviesController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpGet("all")]
    public ActionResult<List<MovieDto>> GetAllMovies()
    {
        try
        {
            var movies = _movieService.GetAllMovies();
            return Ok(movies);
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("movie/{id}")]
    public ActionResult<MovieDto> GetMovieById(int id)
    {
        try
        {
            var movie = _movieService.GetById(id);
            return Ok(movie);
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("orderBy")]
    public ActionResult<List<MovieDto>> OrderMoviesBy([FromQuery] MovieOrderBy orderBy)
    {
        try
        {
            var movies = _movieService.OrderMoviesBy(orderBy);
            return Ok(movies);
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("filterByYear")]
    public ActionResult<List<MovieDto>> FilterByYear([FromQuery] int year)
    {
        try
        {
            var movies = _movieService.FilterByYear(year);
            return StatusCode(StatusCodes.Status200OK, movies);
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("filterByGenre")]
    public ActionResult<List<MovieDto>> FilterByGenre([FromQuery] string genre)
    {
        try
        {
            var movies = _movieService.FilterByGenre(genre);
            return StatusCode(StatusCodes.Status200OK, movies);
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("addMovie")]
    public ActionResult AddMovie(CreateMovieDto movie)
    {
        try
        {
            _movieService.CreateNewMovie(movie);
            return StatusCode(StatusCodes.Status201Created, movie);
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            return StatusCode(StatusCodes.Status422UnprocessableEntity, ex.Message);
        }
    }

    [HttpPut("updateMovie")]
    public ActionResult UpdateMovie(MovieDto movie)
    {
        try
        {
            _movieService.UpdateMovie(movie);
            return StatusCode(StatusCodes.Status200OK, movie);
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            return StatusCode(StatusCodes.Status422UnprocessableEntity, ex.Message);
        }
    }

    [HttpDelete("deleteMovie")]
    public ActionResult DeleteMovie(int id)
    {
        try
        {
            _movieService.DeleteMovie(id);
            return StatusCode(StatusCodes.Status200OK);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        }
    }


}

