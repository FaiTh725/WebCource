using Hangfire;
using MediatR;

namespace Test.Application.Common.Mediator
{
    public static class MediatorExtentions
    {
        public static void Shedule(
            this IMediator mediator, 
            IRequest request, 
            TimeSpan timeSpan)
        {
            var backgroundJobClient = new BackgroundJobClient();
            backgroundJobClient.Schedule<MediatorWrapper>(x => x.SendCommand(request), timeSpan);
        }
    }
}
