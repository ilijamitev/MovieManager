namespace MovieManager.Domain.Enums;

[Flags]
public enum Genre
{
    Action = 1,
    Comedy = 2,
    Drama = 4,
    Animation = 8,
    SciFi = 16,
    Adventure = 32,
}
