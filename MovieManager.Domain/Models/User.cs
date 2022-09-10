﻿namespace MovieManager.Domain.Models;

public class User : BaseEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public List<UserMovie> UserMovies { get; set; }
}
