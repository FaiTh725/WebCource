using Authorize.Domain.Entities;
using Authorize.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Data;

namespace Authorize.Infastructure.BackgroundServices
{
    public class InitializeRoles : BackgroundService
    {
        private readonly IServiceScopeFactory scopeFactory;

        public InitializeRoles(
            IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await WaitDatabase(stoppingToken);

            using var scope = scopeFactory.CreateAsyncScope();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<InitializeRoles>>();

            var rolesDb = unitOfWork.RoleRepository
                .GetRoles()
                .ToList();

            var initializeRole = new List<string> { 
                "User", 
                "Admin",
                "Teacher"
            };

            await unitOfWork.BeginTransactionAsync();

            var addRoleTasks = initializeRole.Select(async role =>
            {
                if(rolesDb.FirstOrDefault(x => x.RoleName == role) is null)
                {
                    using var innerScope = scopeFactory.CreateAsyncScope();
                    var innerUnitOfWork = innerScope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                    var newRole = Role.Initialize(role);

                    if (newRole.IsFailure)
                    {
                        logger.LogError("Error initialize roles");
                        return;
                    }

                    await innerUnitOfWork.RoleRepository.AddRole(newRole.Value);
                    await innerUnitOfWork.SaveChangesAsync();
                }
            }).ToList();

            await Task.WhenAll(addRoleTasks);

            await unitOfWork.CommitTransactionAsync();
        }

        private async Task WaitDatabase(CancellationToken cancellationToken)
        {
            using var scope = scopeFactory.CreateAsyncScope();
            var dbContextFactory = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            while (!cancellationToken.IsCancellationRequested)
            {
                if (await dbContextFactory.CanConnectAsync())
                {
                    return;
                }

                await Task.Delay(5000, cancellationToken);
            }
        }
    }
}
