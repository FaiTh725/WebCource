using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Test.API.Contacts.QuestionVariant;
using Test.Application.Commands.QuestionAnswer;
using Test.Application.Commands.Variant.CreateVariant;
using Test.Application.Commands.Variant.DeleteListVariants;
using Test.Application.Commands.Variant.DeleteQuestionVariant;
using Test.Application.Contracts.File;

namespace Test.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionAnswerController : ControllerBase
    {
        private readonly IMediator mediator;

        public QuestionAnswerController(
            IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("[action]")]
        [Authorize(Roles = "Admin, Teacher")]
        public async Task<IActionResult> AddQuestionAnswer(
            CreateQuestionVariantRequest request)
        {
            var createQuestionVariantCommand = new CreateVariantCommand
            {
                QuestionId = request.TestQuestionId,
                IsCorrect = request.IsCorrect,
                Text = request.Text,
                VariantImages = request.VariantImg?
                .Select(x => new FileEntity 
                { 
                    Stream = x.OpenReadStream(),
                    ContentType = x.ContentType
                }).ToList() ??
                new List<FileEntity>()
            };

            var questionId = await mediator.Send(createQuestionVariantCommand);

            return Ok(questionId);
        }

        [HttpDelete("[action]")]
        [Authorize(Roles = "Admin, Teacher")]
        public async Task<IActionResult> DeleteQuestion(
            DeleteQuestionVariantCommand request)
        {
            await mediator.Send(request);

            return Ok();
        }

        [HttpDelete("[action]")]
        [Authorize(Roles = "Admin, Teacher")]
        public async Task<IActionResult> DeleteListQuestions(
            DeleteListVariantsCommand request)
        {
            await mediator.Send(request);

            return Ok();
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<IActionResult> SendTestAnswer(
            AddQuestionAnswerCommand request)
        {
            await mediator.Send(request);

            return Ok();
        }
    }
}
