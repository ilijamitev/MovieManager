using Microsoft.EntityFrameworkCore;
using MovieManager.Domain.Models;

namespace MovieManager.DataAccess.Data;
internal static class DataSeed
{
    internal static void InsertDataInDb(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                FirstName = "Ilija",
                LastName = "Mitev",
                Username = "ile123",
                Password = "ile123",
            }
        );

        modelBuilder.Entity<Movie>().HasData(
             new Movie
             {
                 Id = 1,
                 Title = "Meet the Fockers",
                 Description = "Greg Focker and his fiancee Pam decide to make their respective parents meet before their wedding. However, the Fockers' relaxed attitude does not go down well with Pam's family.",
                 Genre = Domain.Enums.Genre.Comedy,
                 Year = 2004,
             },
            new Movie
            {
                Id = 2,
                Title = "Ride Along 2",
                Description = "James takes Ben along to pull the plug on a drug racket involving an influential businessman, Antonio Pope. However, with Ben's wedding day approaching, the two have little time to expose the crime.",
                Genre = Domain.Enums.Genre.Comedy,
                Year = 2016,
            },
            new Movie
            {
                Id = 3,
                Title = "Red Notice",
                Description = "In the world of international crime, an Interpol agent attempts to hunt down and capture the world's most wanted art thief.",
                Genre = Domain.Enums.Genre.Action,
                Year = 2021,
            },
            new Movie
            {
                Id = 4,
                Title = "Uncharted",
                Description = "Victor Sullivan recruits Nathan Drake to help him find the lost fortune of Ferdinand Magellan. However, they face competition from Santiago Moncada, who believes that the treasure belongs to him.",
                Genre = Domain.Enums.Genre.Adventure | Domain.Enums.Genre.Action,
                Year = 2022,
            },
             new Movie
             {
                 Id = 5,
                 Title = "Star Wars: The Rise of Skywalker (Episode IX)",
                 Description = "The revival of Emperor Palpatine resurrects the battle between the Resistance and the First Order while the Jedi's legendary conflict with the Sith Lord comes to a head.",
                 Genre = Domain.Enums.Genre.Adventure | Domain.Enums.Genre.SciFi,
                 Year = 2022,
             });

        modelBuilder.Entity<UserMovie>().HasData(
            new UserMovie
            {
                Id = 1,
                MovieId = 1,
                UserId = 1
            },
            new UserMovie
            {
                Id = 2,
                MovieId = 5,
                UserId = 1
            },
            new UserMovie
            {
                Id = 3,
                MovieId = 3,
                UserId = 1
            });

    }
}
