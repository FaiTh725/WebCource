using Application.Shared.Exceptions;
using MediatR;
using StudentEntity = Test.Domain.Entities.Student;
using Test.Domain.Repositories;

namespace Test.Application.Commands.Student.CreateStudent
{
    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, long>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateStudentCommandHandler(
            IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<long> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await unitOfWork.StudentRepository
                .GetStudent(request.Email);

            if(student is not null)
            {
                throw new ConflictApiException("Student already exist");
            }

            var studentGroup = await unitOfWork.GroupRepository
                .GetGroup(request.Group);

            if(studentGroup is null)
            {
                throw new BadRequestApiException($"{request.Group} doesnt exist");
            }

            var studentEntity = StudentEntity.Initialize(
                request.Email,
                request.Name,
                studentGroup);

            if(studentEntity.IsFailure)
            {
                throw new BadRequestApiException("Error with request - " + 
                    studentEntity.Error);
            }

            var newStudent = await unitOfWork.StudentRepository
                .AddStudent(studentEntity.Value);

            await unitOfWork.SaveChangesAsync();

            return newStudent.Id;
        }
    }
}
