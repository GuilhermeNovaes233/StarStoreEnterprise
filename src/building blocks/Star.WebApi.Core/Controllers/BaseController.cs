using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace Star.WebApi.Core.Controllers
{
    [ApiController]
    public abstract class BaseController : Controller
    {
        protected ICollection<string> Errors = new List<string>();
        protected ActionResult CustomResponse(object result = null)
        {
            if (IsValid())
            {
                return Ok(result);
            }
            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>{
                { "Messages", Errors.ToArray() }}));
        }

        //Coleta erros da nossa ModelState se houver
        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);
            foreach (var error in errors)
            {
                AddErrorProcessing(error.ErrorMessage);
            }

            return CustomResponse();
        }

        protected ActionResult CustomResponse(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                AddErrorProcessing(error.ErrorMessage);
            }

            return CustomResponse();
        }

        protected bool IsValid()
        {
            return !Errors.Any();
        }

        protected void AddErrorProcessing(string error)
        {
            Errors.Add(error);
        }

        protected void ClearErrorProcessing()
        {
            Errors.Clear();
        }
    }
}
