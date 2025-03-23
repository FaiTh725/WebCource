using Hangfire;
using System.Linq.Expressions;
using Test.Application.Interfaces;

namespace Test.Infastructure.Implementations
{
    public class BackgroundJobService : IBackgroundJobService
    {
        private readonly IBackgroundJobClient backgroundJobClient;

        public BackgroundJobService(
            IBackgroundJobClient backgroundJobClient)
        {
            this.backgroundJobClient = backgroundJobClient;
        }

        public void CancelJob(string jobId)
        {
            backgroundJobClient.Delete(jobId);
        }

        public string CreateDelaydedJob<T>(Expression<Action<T>> methodcall, TimeSpan timeSpan)
        {
            return backgroundJobClient.Schedule(methodcall, timeSpan);
        }

        public string CreateFireAndForgetJob<T>(Expression<Action<T>> methodCall)
        {
            return backgroundJobClient.Enqueue(methodCall);
        }
    }
}
