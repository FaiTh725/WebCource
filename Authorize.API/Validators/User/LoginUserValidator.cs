using Authorize.Application.Commands.User.Login;
using FluentValidation;
using UserEntity = Authorize.Domain.Entities.User;

namespace Authorize.API.Validators.User
{
    public class LoginUserValidator : AbstractValidator<LoginUserRequest>
    {
        public LoginUserValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(UserEntity.MIN_PASSWORD_LENGTH)
                    .WithMessage("Password has to greater or equals " +
                        UserEntity.MIN_PASSWORD_LENGTH.ToString())
                .MaximumLength(UserEntity.MAX_PASSWORD_LENGTH)
                    .WithMessage("Password has to less or equals " +
                        UserEntity.MAX_PASSWORD_LENGTH.ToString())
                .Must(x => UserEntity.IsValidPassword(x))
                    .WithMessage("Password has to contains one letter and one number at the same time");
        }
    }
}
