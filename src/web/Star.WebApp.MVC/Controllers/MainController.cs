using Microsoft.AspNetCore.Mvc;
using Star.WebApp.MVC.Models;
using System.Linq;

namespace Star.WebApp.MVC.Controllers
{
    public class MainController : Controller
    {
        protected bool ExistsErrorsInResponse(ResponseResult response)
        {
            if (response != null && response.Errors.Messages.Any())
            {
                return true;
            }

            return false;
        }
    }
}
