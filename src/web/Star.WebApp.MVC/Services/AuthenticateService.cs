using Star.WebApp.MVC.Models.User;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Star.WebApp.MVC.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly HttpClient _httpClient;

        public AuthenticateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> Login(UserLogin userLogin)
        {
            var loginContent = new StringContent(
                JsonSerializer.Serialize(userLogin),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync("https://localhost:44324/api/identity/authenticate", loginContent);

            var teste = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<string>(await response.Content.ReadAsStringAsync());
        }

        public async Task<string> Register(UserRegister userRegister)
        {
            var registerContent = new StringContent(
               JsonSerializer.Serialize(userRegister),
               Encoding.UTF8,
               "application/json"
            );

            var response = await _httpClient.PostAsync("https://localhost:44324/api/identity/new-account", registerContent);
            var teste = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<string>(await response.Content.ReadAsStringAsync());
        }
    }
}