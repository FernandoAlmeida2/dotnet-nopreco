using dotnet_nopreco.Dtos.User;
using FluentValidation;

namespace dotnet_nopreco.Middlewares.User
{
    public class UserLoginValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginValidator()
       {
            RuleFor(u => u.Email).EmailAddress().WithMessage("Provide a valid email!");
       }
    }
}