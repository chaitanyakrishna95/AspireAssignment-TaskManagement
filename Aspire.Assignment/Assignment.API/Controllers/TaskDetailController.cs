using Assignment.Contracts.DTO;
using Assignment.Core.Exceptions;
using Assignment.Core.Handlers.Commands;
using Assignment.Core.Handlers.Queries;
using Assignment.Providers.Handlers.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Assignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskDetailController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskDetailController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> Post([FromForm] CreateTaskDTO model)
        {
            try
            {
                var command = new CreateTaskCommand(model);
                var response = await _mediator.Send(command);
                if (response>0)
                {
                    if (model.File.Length > 0)
                    {
                        try
                        {
                            string path = Path.Combine(@"C:\\TaskFiles", model.File.FileName);
                            using (FileStream fileStream = System.IO.File.Create(path))
                            {
                                model.File.CopyTo(fileStream);
                                fileStream.Flush();

                            }
                        }
                        catch (InvalidRequestBodyException ex)
                        {
                            return BadRequest(new BaseResponseDTO
                            {
                                IsSuccess = false,
                                Errors = ex.Errors
                            });
                        }


                    }
                }
                return StatusCode((int)HttpStatusCode.Created, response);
            }
            catch (InvalidRequestBodyException ex)
            {
                return BadRequest(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = ex.Errors
                });
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TaskDetailDTO>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllTaskQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }
        [HttpGet]
        [Route("DownloadFile")]
        public async Task<IActionResult> DownloadFIle(string Filename)
        {
            var filepath= Path.Combine(@"C:\\TaskFiles", Filename);
            var provider = new FileExtensionContentTypeProvider();
            if(!provider.TryGetContentType(filepath, out var contentType))
            {
                contentType="application/octet-stream";
            }
            var bytes = await System.IO.File.ReadAllBytesAsync(filepath);
            return File(bytes,contentType,Path.GetFileName(filepath));
        }
    }
}
