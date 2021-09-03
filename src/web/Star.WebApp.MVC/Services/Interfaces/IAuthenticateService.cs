using Star.WebApp.MVC.Models.User;
using System.Threading.Tasks;

namespace Star.WebApp.MVC.Services
{
    public interface IAuthenticateService
    {
        Task<string> Login(UserLogin userLogin);
        Task<string> Register(UserRegister userRegister);
    }
}