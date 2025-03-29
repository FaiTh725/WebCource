using FluentValidation;
using Test.API.Contacts.QuestionVariant;
using Test.API.Contacts.Test;
using Test.API.Contacts.TestQuestion;
using Test.API.Validators.Group;
using Test.API.Validators.QuestionAnswer;
using Test.API.Validators.Subject;
using Test.API.Validators.Teacher;
using Test.API.Validators.Test;
using Test.API.Validators.TestQuestion;
using Test.Application.Commands.Group.CreateGroup;
using Test.Application.Commands.QuestionAnswer;
using Test.Application.Commands.Subject.CreateSubject;
using Test.Application.Commands.Teacher.CreateTeacher;
using Test.Application.Commands.TestAccess.OpenTest;

namespace Test.API.Extentions
{
    public static class AppExtentions
    {
        public static IServiceCollection ConfigureValidators(
            this IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateGroupCommand>, CreateGroupValidator>();
            services.AddScoped<IValidator<CreateQuestionVariantRequest>, CreateQuestionVariantValidator>();
            services.AddScoped<IValidator<AddQuestionAnswerCommand>, SendAnswerValidator>();
            services.AddScoped<IValidator<CreateSubjectCommand>, CreateSubjectValidator>();
            services.AddScoped<IValidator<CreateTeacherCommand>, CreateTeacherValidator>();
            services.AddScoped<IValidator<CreateTestRequest>, CreateTestValidator>();
            services.AddScoped<IValidator<OpenTestCommand>, OpenTestValidator>();
            services.AddScoped<IValidator<CreateTestQuestionWithVariantsRequest>, CreateTestQuestionValidator>();
            

            return services;
        }
    }
}
