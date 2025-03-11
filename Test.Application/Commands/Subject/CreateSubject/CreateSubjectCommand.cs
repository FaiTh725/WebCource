using MediatR;

namespace Test.Application.Commands.Subject.CreateSubject
{
    public class CreateSubjectCommand : IRequest<long>
    {
        public string Name { get; set; } = string.Empty;
    }
}
