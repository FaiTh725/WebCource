using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Test.API.Contacts.TestQuestion;
using Test.Application.Commands.Question.CreateQuestion;
using Test.Application.Commands.Question.DeleteQuestion;
using Test.Application.Contracts.File;
using Test.Application.Contracts.QuestionVariant;

namespace Test.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestQuestionController : ControllerBase
    {
        private readonly IMediator mediator;

        public TestQuestionController(
            IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("[action]")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> AddQuestion(
            CreateTestQuestionWithVariantsRequest request)
        {
            var questionFiles = request.QuestionImg?.Select(
                x => new FileEntity
                {
                    Stream = x.OpenReadStream(),
                    ContentType = x.ContentType
                }).ToList() ??
                new List<FileEntity>();

            var createQuestionComman = new CreateQuestionCommand
            {
                TestId = request.TestId,
                QuestionImages = questionFiles,
                Type = request.Type,
                Question = request.Question,
                Variants = request.Variants.Select(x => new VariantRequest
                {
                    IsCorrect = x.IsCorrect,
                    Text = x.Text,
                    VariantImages = x.VariantImg?.Select(
                        y => new FileEntity
                        {
                            Stream = y.OpenReadStream(),
                            ContentType = y.ContentType
                        }
                        ).ToList() ??
                        new List<FileEntity>()
                }).ToList()
            };

            var questionId = await mediator.Send(createQuestionComman);

            return Ok(questionId);
        }

        [HttpDelete("[action]")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> DeleteTestQuestion(
            DeleteQuestionCommand request)
        {
            await mediator.Send(request);

            return Ok();
        }
    }
}
