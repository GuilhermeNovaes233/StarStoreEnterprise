namespace Star.WebApp.MVC.Models.User
{
    public class UserResponseLogin
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserToken UserToken { get; set; }

        // public ResponseResult ResponseResult { get; set; }
    }
}