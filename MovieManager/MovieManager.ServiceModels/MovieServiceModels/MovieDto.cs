namespace MovieManager.ServiceModels.MovieServiceModels;
public class MovieDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int? Year { get; set; }
    public string? Genre { get; set; }
}
