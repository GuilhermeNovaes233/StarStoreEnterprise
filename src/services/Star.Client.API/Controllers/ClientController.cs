using Microsoft.AspNetCore.Mvc;
using Star.Client.API.Application.Commands;
using Star.Core.Mediator;
using Star.WebApi.Core.Controllers;
using System;
using System.Threading.Tasks;

namespace Star.Client.API.Controllers
{
    public class ClientController : BaseController
    {
        private readonly IMediatorHandler _mediatorHandler;

        public ClientController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpGet("clients")]
        public async Task<IActionResult> Index()
        {
            var result = await _mediatorHandler.SendCommand(
                new RegisterClientCommand(Guid.NewGuid(), "Guilherme", "guilherme@gmail.com", "20733549047"));

            return CustomResponse(result);
        }
    }
}
