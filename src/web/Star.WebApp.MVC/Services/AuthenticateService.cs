using Microsoft.Extensions.Options;
using Star.WebApp.MVC.Extensions;
using Star.WebApp.MVC.Models;
using Star.WebApp.MVC.Models.User;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Star.WebApp.MVC.Services
{
    public class AuthenticateService : Service, IAuthenticateService
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _settings;

        public AuthenticateService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.AuthenticationUrl);

            _httpClient = httpClient;
        }

        public async Task<UserResponseLogin> Login(UserLogin userLogin)
        {
            var loginContent = GetContent(userLogin);

            var response = await _httpClient.PostAsync("/api/identity/authenticate", loginContent);

            if (!HandleErrorsResponse(response))
            {
                return new UserResponseLogin()
                {
                    ResponseResult = await DeserializeObjectResponse<ResponseResult>(response)
                };
            }

            return await DeserializeObjectResponse<UserResponseLogin>(response);
        }

        public async Task<UserResponseLogin> Register(UserRegister userRegister)
        {
            var registerContent = GetContent(userRegister);

            var response = await _httpClient.PostAsync("/api/identity/new-account", registerContent);

            if (!HandleErrorsResponse(response))
            {
                return new UserResponseLogin()
                {
                    ResponseResult = await DeserializeObjectResponse<ResponseResult>(response)
                };
            }

            return await DeserializeObjectResponse<UserResponseLogin>(response);
        }
    }
}