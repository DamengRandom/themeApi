using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ThemeApi.Domain.Models;
using ThemeApi.Handlers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ThemeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThemesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ThemesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Theme>>> ReadThemesHandler(CancellationToken ct)
        {
            return await _mediator.Send(new ReadThemesHandler.Query(), ct);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Theme>> SingleThemeHandler(Guid id)
        {
            return await _mediator.Send(new SingleThemeHandler.Query { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> CreateThemeHandler(CreateThemeHandler.Command command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<Unit>> UpdateThemeHandler(Guid id, UpdateThemeHandler.Command command)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<Unit>> DeleteThemeHandler(Guid id)
        {
            return await _mediator.Send(new DeleteThemeHandler.Command { Id = id});
        }
    }
}
