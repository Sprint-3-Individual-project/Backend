using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace WebshopBackend.DTOs
{
    public class RegisterDTO
    {
        [Newtonsoft.Json.JsonConstructor]
        public RegisterDTO([JsonProperty("username")] string username, [JsonProperty("password")] string password, [JsonProperty("email")] string email)
        {
            _username = username;
            _password = password;
            _email = email;
        }
        [Required(ErrorMessage = "Username is required")]
        private string _username;
        public string Username { get { return _username; } }

        [Required(ErrorMessage = "Password is required")]
        private string _password;
        public string Password { get { return _password; } }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email adress")]
        private string _email;
        public string Email { get { return _email; } }
    }
}

