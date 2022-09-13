using FluentValidation;
using MovieManager.ServiceModels.MovieServiceModels;

namespace MovieManager.Helpers.Validations;

public class CreateMovieValidator : AbstractValidator<CreateMovieDto>
{
    public CreateMovieValidator()
    {
        RuleFor(c => c.Title).NotNull().Length(1, 100);
        RuleFor(c => c.Description).MaximumLength(500);
        RuleFor(c => c.Year).NotNull().InclusiveBetween(1950, DateTime.Now.Year);
        RuleFor(c => c.Genre).NotNull();
    }
}
