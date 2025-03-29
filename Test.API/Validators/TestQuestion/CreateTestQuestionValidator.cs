using FluentValidation;
using Test.API.Contacts.TestQuestion;

namespace Test.API.Validators.TestQuestion
{
    public class CreateTestQuestionValidator : AbstractValidator<CreateTestQuestionWithVariantsRequest>
    {
        public CreateTestQuestionValidator()
        {
            RuleFor(x => x.Question)
                .NotEmpty()
                    .WithMessage("Question is required")
                .MaximumLength(500)
                    .WithMessage("Question max length is 500");
        }
    }
}
