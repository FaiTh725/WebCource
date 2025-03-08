using Application.Shared.Exceptions;
using MediatR;
using Test.Application.Contracts.Student;
using Test.Domain.Repositories;

namespace Test.Application.Queries.Student.GetStudentByEmail
{
    public class GetStudentByEmailHandler : IRequestHandler<GetStudentByEmailQuery, StudentResponse>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetStudentByEmailHandler(
            IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<StudentResponse> Handle(GetStudentByEmailQuery request, CancellationToken cancellationToken)
        {
            var student = await unitOfWork.StudentRepository
                .GetStudentWithGroup(request.Email);

            if (student is null)
            {
                throw new NotFoundApiException("Student doesnt exist");
            }

            return new StudentResponse
            {
                Email = student.Email,
                GroupName = student.Group.GroupName,
                Name = student.Name
            };
        }
    }
}
