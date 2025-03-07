using Application.Shared.Exceptions;
using MediatR;
using Test.Application.Contracts.Teacher;
using Test.Domain.Repositories;

namespace Test.Application.Queries.Teacher.GetTeacherById
{
    public class GetTeacherByIdHandler : IRequestHandler<GetTeacherByIdQuery, TeacherResponse>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetTeacherByIdHandler(
            IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<TeacherResponse> Handle(GetTeacherByIdQuery request, CancellationToken cancellationToken)
        {
            var teacher = await unitOfWork.TeacherRepository
                .GetTeacher(request.Id);

            if(teacher is null)
            {
                throw new NotFoundApiException("Teacher Does Not Exist");
            }

            return new TeacherResponse
            {
                Id = teacher.Id,
                Email = teacher.Email,
                Name = teacher.Name
            };
        }
    }
}
