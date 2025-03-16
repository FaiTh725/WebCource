using MediatR;
using Microsoft.AspNetCore.Mvc;
using Test.Application.Commands.Question.DeleteQuestion;
using Test.Application.Common.Mediator;
using Test.Application.Interfaces;

namespace Test.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileUploadController : ControllerBase
    {
        private readonly IBlobService blobService;
        private readonly IMediator mediator;

        public FileUploadController(
            IBlobService blobService,
            IMediator mediator)
        {
            this.blobService = blobService;
            this.mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var upload = await blobService.UploadBlob("first", file.OpenReadStream(), file.ContentType);

            return Ok(upload);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetFile([FromQuery]string path)
        {
            var fileUrl = await blobService.GetBlobUrl(path);

            return Ok(fileUrl);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteFile()
        {
            await blobService.DeleteBlob("first");

            return Ok("deleted");
        }
    }
}
