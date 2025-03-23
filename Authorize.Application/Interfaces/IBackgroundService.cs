using Hangfire;
using System.Linq.Expressions;

namespace Authorize.Application.Interfaces
{
    public interface IBackgroundService
    {
        void CreateScheduleJob<T>(
            Expression<Action<T>> methodCall, string jobName, string cronExpression);
    }
}
