using Star.WebApp.MVC.Models.User;
using System.Threading.Tasks;

namespace Star.WebApp.MVC.Services
{
    public interface IAuthenticateService
    {
        Task<UserResponseLogin> Login(UserLogin userLogin);
        Task<UserResponseLogin> Register(UserRegister userRegister);
    }
}