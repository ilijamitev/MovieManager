using Microsoft.EntityFrameworkCore;
using MovieManager.Domain.Models;

namespace MovieManager.DataAccess.Data;

public class MovieManagerDbContext : DbContext
{
    public MovieManagerDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<UserMovie> UsersMovies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserMovie>()
            .HasKey(x => new { x.MovieId, x.UserId });

        modelBuilder.Entity<UserMovie>()
             .HasOne(x => x.User)
             .WithMany(x => x.UserMovies)
             .HasForeignKey(x => x.UserId);

        modelBuilder.Entity<UserMovie>()
            .HasOne(x => x.Movie)
            .WithMany(x => x.UserMovies)
            .HasForeignKey(x => x.MovieId);

        modelBuilder.Entity<User>(x =>
        {
            x.Property(p => p.Password).IsRequired().HasMaxLength(50);
            x.Property(p => p.Username).IsRequired().HasMaxLength(20);
            x.Property(p => p.FirstName).IsRequired(false).HasMaxLength(100);
            x.Property(p => p.LastName).IsRequired(false).HasMaxLength(100);
        });

        modelBuilder.Entity<Movie>(x =>
        {
            x.Property(p => p.Title).IsRequired().HasMaxLength(100);
            x.Property(p => p.Year).IsRequired();
            x.Property(p => p.Description).IsRequired(false).HasMaxLength(500);
            x.Property(p => p.Genre).IsRequired();
        });

        DataSeed.InsertDataInDb(modelBuilder);
    }

}
