using Application.Shared.Exceptions;
using MediatR;
using Test.Domain.Repositories;

namespace Test.Application.Commands.Student.DeleteStudent
{
    public class DeleteStudentHandler : IRequestHandler<DeleteStudentCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteStudentHandler(
            IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var existStudent = await unitOfWork.StudentRepository
                .GetStudent(request.StudentEmail);

            if (existStudent is null)
            {
                throw new NotFoundApiException("Student doesnt exist");
            }

            await unitOfWork.StudentRepository
                .DeleteStudent(request.StudentEmail);

            await unitOfWork.SaveChangesAsync();
        }
    }
}
