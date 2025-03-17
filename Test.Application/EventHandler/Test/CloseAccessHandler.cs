using MediatR;
using Test.Domain.Event;
using Test.Domain.Repositories;

namespace Test.Application.EventHandler.Test
{
    public class CloseAccessHandler : INotificationHandler<TestCompletedEvent>
    {
        private readonly IUnitOfWork unitOfWork;

        public CloseAccessHandler(
            IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task Handle(TestCompletedEvent notification, CancellationToken cancellationToken)
        {
            await unitOfWork.TestAccessRepository
                .DeleteTestAccess(
                notification.TestId,
                notification.StudentId);
        }
    }
}
