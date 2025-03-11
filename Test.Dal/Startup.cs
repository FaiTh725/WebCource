
using Microsoft.Extensions.DependencyInjection;
using Test.Dal.Repositories;
using Test.Domain.Repositories;

namespace Test.Dal
{
    public static class Startup
    {
        public static void AddDalRepositories(
            this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<ITestRepository, TestRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IQuestionVariantRepository, QuestionVariantRepository>();
        }
    }
}
