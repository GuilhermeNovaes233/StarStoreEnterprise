using Microsoft.AspNetCore.Mvc;
using Star.Client.API.Application.Commands;
using Star.Core.Mediator;
using System;
using System.Threading.Tasks;

namespace Star.Client.API.Controllers
{
    public class ClientController : Controller
    {
        private readonly IMediatorHandler _mediatorHandler;

        public ClientController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpGet("clients")]
        public async Task<IActionResult> Index()
        {
            var resultado = await _mediatorHandler.SendCommand(
                new RegisterClientCommand(Guid.NewGuid(), "Guilherme", "guilherme@gmail.com", "661.388.600-98"));

            //return CustomResponse(resultado);

            throw new NotImplementedException();
        }
    }
}
