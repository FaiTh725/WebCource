using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Notification.Contracts.Email.Requests;
using Test.Domain.Event;
using Test.Domain.Repositories;

namespace Test.Application.EventHandler.Test
{
    public class SendNotificationTestResult : 
        INotificationHandler<TestCompletedEvent>
    {
        private readonly IBus bus;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<SendNotificationTestResult> logger;

        public SendNotificationTestResult(
            IBus bus,
            IUnitOfWork unitOfWork,
            ILogger<SendNotificationTestResult> logger)
        {
            this.unitOfWork = unitOfWork;
            this.bus = bus;
            this.logger = logger;
        }

        public async Task Handle(TestCompletedEvent notification, CancellationToken cancellationToken)
        {
            var test = await unitOfWork.TestRepository
                .GetTest(notification.TestId);
            var student = await unitOfWork.StudentRepository
                .GetStudent(notification.StudentId);

            if(test is null ||
                student is null)
            {
                logger.LogError("Fail sent test resut, test or student doesnt exist");
                return;
            }

            await bus.Publish(new SendEmailRequest 
            { 
                Consumer = student.Email,
                Subject = "Test Passed",
                Message = "you have passed the test and you result is " +
                    notification.Percent.ToString()
            });
        }
    }
}
