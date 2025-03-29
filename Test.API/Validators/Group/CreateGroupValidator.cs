using FluentValidation;
using Test.Application.Commands.Group.CreateGroup;
using Test.Domain.Entities;

namespace Test.API.Validators.Group
{
    public class CreateGroupValidator : AbstractValidator<CreateGroupCommand>
    {
        public CreateGroupValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Must(name =>
                    name.ToString().Length == StudentGroup.GROUP_LENGTH)
                    .WithMessage("Group Name has to 8 characters");
        }
    }
}
