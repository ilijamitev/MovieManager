using MovieManager.DataAccess.Data;
using MovieManager.DataAccess.Repositories.Interfaces;
using MovieManager.Domain.Models;

namespace MovieManager.DataAccess.Repositories;

public class MoviesRepository : IRepository<Movie>
{
    private readonly MovieManagerDbContext _context;
    public MoviesRepository(MovieManagerDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Movie> GetAll()
    {
        return _context.Movies;
    }

    public Movie GetById(int id)
    {
        return _context.Movies.SingleOrDefault(x => x.Id == id)!;
    }

    public IEnumerable<Movie> Filter(Func<Movie, bool> filter)
    {
        return _context.Movies.Where(filter);
    }

    public void Create(Movie entity)
    {
        _context.Movies.Add(entity);
        _context.SaveChanges();
    }

    public void Update(Movie entity)
    {
        //_context.Update(entity); //moze i bez ova
        _context.SaveChanges();
    }

    public void Delete(Movie entity)
    {
        _context.Remove(entity);
        _context.SaveChanges();
    }

}
