using FluentValidation;
using Test.Application.Commands.Teacher.CreateTeacher;

namespace Test.API.Validators.Teacher
{
    public class CreateTeacherValidator : AbstractValidator<CreateTeacherCommand>
    {
        public CreateTeacherValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                    .WithMessage("Email is required")
                .EmailAddress()
                    .WithMessage("Incorrect email signature");
        }
    }
}
