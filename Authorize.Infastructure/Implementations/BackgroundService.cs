using Authorize.Application.Interfaces;
using Authorize.Domain.Repositories;
using Hangfire;
using System.Linq.Expressions;

namespace Authorize.Infastructure.Implementations
{
    public class BackgroundService : IBackgroundService
    {
        private readonly IRecurringJobManager recurringJobManager;

        public BackgroundService(
            IRecurringJobManager recurringJobManager)
        {
            this.recurringJobManager = recurringJobManager;
        }
        public void CreateScheduleJob<T>(
            Expression<Action<T>> methodCall, string jobName, string cronExpression)
        {
            recurringJobManager.AddOrUpdate(jobName, methodCall, cronExpression);
        }

    }
}
