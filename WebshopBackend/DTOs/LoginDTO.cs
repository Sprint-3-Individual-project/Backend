using System.ComponentModel.DataAnnotations;

namespace WebshopBackend.DTOs
{
    public class LoginDTO
    {
        public LoginDTO(string email, string password) 
        {
            _email = email;
            _password = password;
        }

        [Required(ErrorMessage = "Password is required to login")]
        private string _password;
        public string Password { get { return _password;} }

        [Required(ErrorMessage = "Email is required to login")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        private string _email;
        public string Email { get { return _email;} }

    }
}
