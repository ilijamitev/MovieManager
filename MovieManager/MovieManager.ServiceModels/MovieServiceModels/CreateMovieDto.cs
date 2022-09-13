namespace MovieManager.ServiceModels.MovieServiceModels;

public class CreateMovieDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int? Year { get; set; }
    public string? Genre { get; set; }
}
