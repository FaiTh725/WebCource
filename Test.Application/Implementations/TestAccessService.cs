using Test.Application.Interfaces;
using Test.Domain.Repositories;

namespace Test.Application.Implementations
{
    public class TestAccessService : ITestAccessService
    {
        private readonly IUnitOfWork unitOfWork;

        public TestAccessService(
            IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> HasAccess(long testId, string studentEmail)
        {
            var student = await unitOfWork.StudentRepository
                .GetStudent(studentEmail);

            if(student is null)
            {
                return false;
            }

            var testAccess = await unitOfWork.TestAccessRepository
                .GetTestAccess(testId, student.Id);

            return testAccess is null;
        }
    }
}
