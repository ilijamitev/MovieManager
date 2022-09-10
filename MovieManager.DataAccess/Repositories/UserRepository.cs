using Microsoft.EntityFrameworkCore;
using MovieManager.DataAccess.Data;
using MovieManager.DataAccess.Repositories.Interfaces;
using MovieManager.Domain.Models;

namespace MovieManager.DataAccess.Repositories;
public class UserRepository : IRepository<User>
{
    private readonly MovieManagerDbContext _context;

    public UserRepository(MovieManagerDbContext context)
    {
        _context = context;
    }

    public IEnumerable<User> GetAll()
    {
        return _context.Users;
    }

    public User GetById(int id)
    {
        return _context.Users.SingleOrDefault(x => x.Id == id)!;
    }

    public IEnumerable<User> Filter(Func<User, bool> filter)
    {
        return _context.Users.Where(filter);
    }

    public void Create(User entity)
    {
        _context.Users.Add(entity);
        _context.SaveChanges();
    }

    public void Update(User entity)
    {
        _context.Users.Update(entity);
        _context.SaveChanges();
    }

    public void Delete(User entity)
    {
        _context.Users.Remove(entity);
        _context.SaveChanges();
    }

}
