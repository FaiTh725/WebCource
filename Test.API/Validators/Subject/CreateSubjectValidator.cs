using FluentValidation;
using Test.Application.Commands.Subject.CreateSubject;

namespace Test.API.Validators.Subject
{
    public class CreateSubjectValidator : AbstractValidator<CreateSubjectCommand>
    {
        public CreateSubjectValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                    .WithMessage("Name is required");
        }
    }
}
