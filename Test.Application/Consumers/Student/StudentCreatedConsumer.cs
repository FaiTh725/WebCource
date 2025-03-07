using Application.Shared.Results;
using MassTransit;
using MediatR;
using Test.Application.Commands.Student.CreateStudent;
using Test.Contracts.Student.Requests;
using Test.Contracts.Student.Responses;

namespace Test.Application.Consumers.Student
{
    public class StudentCreatedConsumer : IConsumer<CreateStudentRequest>
    {
        private readonly IMediator mediator;

        public StudentCreatedConsumer(
            IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task Consume(ConsumeContext<CreateStudentRequest> context)
        {
            try
            {
                var studentId = await mediator.Send(new CreateStudentCommand
                {
                    Group = context.Message.GroupNumber,
                    Email = context.Message.Email,
                    Name = context.Message.Name
                });
                
                await context.RespondAsync(BaseResult.SuccessResult());
            }
            catch(Exception ex)
            {
                await context.RespondAsync(BaseResult
                    .FailureResult(ex.Message));
            }
        }
    }
}
