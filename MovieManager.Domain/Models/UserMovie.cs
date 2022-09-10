namespace MovieManager.Domain.Models;

public class UserMovie : BaseEntity
{
    public int MovieId { get; set; }
    public Movie Movie { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}