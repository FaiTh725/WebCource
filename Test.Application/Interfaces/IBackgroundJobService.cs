using System.Linq.Expressions;

namespace Test.Application.Interfaces
{
    public interface IBackgroundJobService
    {
        string CreateFireAndForgetJob<T>(Expression<Action<T>> methodCall);

        string CreateDelaydedJob<T>(Expression<Action<T>> methodcall, TimeSpan timeSpan);

        void CancelJob(string jobId);
    }
}
