using Application.Shared.Exceptions;
using MediatR;
using Test.Domain.Repositories;

namespace Test.Application.Commands.Teacher.DeleteTeacher
{
    public class DeleteTeacherHandler : IRequestHandler<DeleteTeacherCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteTeacherHandler(
            IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
        {
            var existTeacher = await unitOfWork.TeacherRepository
                .GetTeacher(request.TeacherEmail);

            if (existTeacher is null)
            {
                throw new NotFoundApiException("Teacher doesn not exist");
            }

            await unitOfWork.TeacherRepository
                .DeleteTeacher(request.TeacherEmail);

            await unitOfWork.SaveChangesAsync();
        }
    }
}
