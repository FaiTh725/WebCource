using FluentValidation;
using Test.Application.Commands.QuestionAnswer;

namespace Test.API.Validators.QuestionAnswer
{
    public class SendAnswerValidator : 
        AbstractValidator<AddQuestionAnswerCommand>
    {
        public SendAnswerValidator()
        {
            RuleFor(x => x.TestVariantsId)
                .NotEmpty()
                    .WithMessage("List Variant cant be empty");
        }
    }
}
