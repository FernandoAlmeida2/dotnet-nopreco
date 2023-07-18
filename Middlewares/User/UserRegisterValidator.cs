using dotnet_nopreco.Dtos.User;
using FluentValidation;

namespace dotnet_nopreco.Middlewares.User
{
    public class UserRegisterValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterValidator()
        {
            RuleFor(p => p.Name).Equal("Admin").WithMessage("User can only be Admin.");
            RuleFor(p => p.Email).EmailAddress().WithMessage("Provide a valid email!");
            RuleFor(p => p.Password).MinimumLength(5).MaximumLength(60)
                .WithMessage("Password invalid! (must be between 5 to 60 characters)");
            RuleFor(p => p.RepeatPassword).Matches(p => p.Password)
                .WithMessage("Repeat the password correctly!");
        }
    }
}