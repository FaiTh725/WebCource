using FluentValidation;
using Test.API.Contacts.QuestionVariant;

namespace Test.API.Validators.QuestionAnswer
{
    public class CreateQuestionVariantValidator : AbstractValidator<CreateQuestionVariantRequest>
    {
        public CreateQuestionVariantValidator()
        {
            RuleFor(x => x.Text)
                .NotEmpty()
                    .WithMessage("Text is required")
                .MaximumLength(500)
                    .WithMessage("Text must not exceed 500 characters");
        }
    }
}
