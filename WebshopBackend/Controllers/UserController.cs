using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using User;
using User.Exceptions;
using User.Interfaces;
using WebshopBackend.DTOs;

namespace WebshopBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("_webshop_frontend")]
    public class UserController : ControllerBase
    {
        IUserManager _usermanager;
        IConfiguration _config;


        public UserController(IUserManager UserManager, IConfiguration config)
        {
            _usermanager = UserManager;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid input. RegisterDTO is required.");
            }
            try
            {
                Account account = _usermanager.GetAccountByEmail(dto.Email);
                if (account == null)
                {
                    var hashedpassword = PasswordHasher.HashPassword(dto.Password);
                    var user = new Account(dto.Username, hashedpassword, dto.Email, Role.User);
                    await _usermanager.RegisterUser(user);
                    return Ok("Je hebt met succes een Account aangemaakt.");
                }
                else
                {
                    return Ok("Het huidige Email adress is al in gebruik");
                }
                // eerst wil ik controleren of de User bestaat, zo niet wil ik controleren of alle velden geldig heeft ingevuld.
                // is dat het geval mag er een nieuwe user worden aangemaakt.
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new
                {
                    Message = "Internal Server Error: " + ex.Message
                });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> UserLogin([FromBody] LoginDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid input. UserDTO is required.");
            }
            try
            {
                //zoekt of het account bestaat met ingevoerde email
                Account user = _usermanager.GetAccountByEmail(dto.Email);
                if (user == null)
                {
                    return Unauthorized("Invalid Email");
                }
                //een check of het wachtwoord overeenkomt met het ingevoerde wachtwoord
                bool passwordvalid = PasswordHasher.Verification(dto.Password, user.Password);
                if (!passwordvalid)
                {
                    return Unauthorized("Invalid Password");
                }

                TokenGenerator tokenGenerator = new TokenGenerator(_config);

                string token = tokenGenerator.GenerateToken(user);

                // User role aanpassen in database naar admin
                //tot slot als alles is gecontroleerd wil ik dat hij een token meekrijgt.
                //await _usermanager.
                return Ok(token);
                //GenerateToken(user, _config)
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new
                {
                    Message = "Internal Server Error: " + ex.Message
                });
            }
        }


        // misschien nog een await toepassen
        [HttpPost("elavate")]
        [Authorize]
        public async Task<IActionResult> ElevateUser(string email)
        {
            var roleClaim = User.FindFirstValue("role");
            if (roleClaim != Role.Admin.ToString())
            {
                return Unauthorized("You are not authorized to do this");
            }

            Account account = _usermanager.GetAccountByEmail(email);
            if (account == null)
            {
                return Unauthorized("Invalid Email");
            }

            bool result = await _usermanager.ElavateUserRole(account);
            if (result)
            {
                return Ok("Gebruikersrol succesvol bijgewerkt naar Admin");
            }
            else
            {
                return BadRequest("Kon gebruikersrol niet bijwerken");
            }
        }


        /// <summary>
        /// Generate JWT token
        /// </summary>
        /// <param name="account"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        //private static string GenerateToken(Account account, IConfiguration config)
        //{
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    // Set the claims
        //    var claims = new List<Claim>()
        //    {
        //        new Claim(JwtRegisteredClaimNames.Jti, account.CustomerId.ToString()),
        //        new Claim(JwtRegisteredClaimNames.Email, account.Email),
        //        new Claim(JwtRegisteredClaimNames.Name, account.Username),
        //        new Claim("role", account.Role.ToString()),
        //    };

        //    var token = new JwtSecurityToken(config["Jwt:Issuer"],
        //      config["Jwt:Issuer"],
        //      claims,
        //      expires: DateTime.Now.AddMinutes(120),
        //      signingCredentials: credentials);

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}
    }
}
