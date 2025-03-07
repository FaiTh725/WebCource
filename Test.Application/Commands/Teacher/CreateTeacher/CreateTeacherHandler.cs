using Application.Shared.Exceptions;
using MediatR;
using Test.Domain.Repositories;
using TeacherEntity = Test.Domain.Entities.Teacher;

namespace Test.Application.Commands.Teacher.CreateTeacher
{
    public class CreateTeacherHandler : IRequestHandler<CreateTeacherCommand, long>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateTeacherHandler(
            IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<long> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacher = await unitOfWork.TeacherRepository
                .GetTeacher(request.Email);
        
            if(teacher is not null)
            {
                throw new ConflictApiException("Teacher with email " + request.Email + " already registered");
            }

            var student = await unitOfWork.StudentRepository
                .GetStudent(request.Email);

            if (student is null)
            {
                throw new NotFoundApiException("Teacher bust be build on student");
            }

            var newTeacher = TeacherEntity.Initialize(
                request.Email,
                student.Name);

            if (newTeacher.IsFailure)
            {
                throw new BadRequestApiException(newTeacher.Error);
            }

            var newTeacherDb = await unitOfWork.TeacherRepository
                .AddTeacher(newTeacher.Value);

            await unitOfWork.SaveChangesAsync();

            return newTeacherDb.Id;
        }
    }
}
