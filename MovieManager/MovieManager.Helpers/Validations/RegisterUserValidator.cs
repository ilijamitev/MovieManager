using FluentValidation;
using MovieManager.ServiceModels.UserServiceModels;

namespace MovieManager.Helpers.Validations;
public class RegisterUserValidator : AbstractValidator<RegisterUserDto>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Username).NotNull().Length(7, 20);
        RuleFor(x => x.Password).NotNull().Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,20}$");
        RuleFor(x => x.FirstName).Length(1, 50);
        RuleFor(x => x.LastName).Length(1, 50);
    }
}
