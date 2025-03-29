using FluentValidation;
using Test.API.Contacts.Test;

namespace Test.API.Validators.Test
{
    public class CreateTestValidator : AbstractValidator<CreateTestRequest>
    {
        public CreateTestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                    .WithMessage("Name is reqired");
        }
    }
}
