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

        public async Task<UserResponseLogin> Login(UserLogin userLogin)
        {
            var loginContent = new StringContent(
                JsonSerializer.Serialize(userLogin),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync("https://localhost:44324/api/identity/authenticate", loginContent);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<UserResponseLogin>(await response.Content.ReadAsStringAsync(), options);
        }

        public async Task<UserResponseLogin> Register(UserRegister userRegister)
        {
            var registerContent = new StringContent(
               JsonSerializer.Serialize(userRegister),
               Encoding.UTF8,
               "application/json"
            );

            var response = await _httpClient.PostAsync("https://localhost:44324/api/identity/new-account", registerContent);

            return JsonSerializer.Deserialize<UserResponseLogin>(await response.Content.ReadAsStringAsync());
        }
    }
}