using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class AccountEntity
    {
        public AccountEntity(int customerid, string username, string password, string email, int role)
        {
            this.CustomerId = customerid;
            this.Username = username;
            this.Password = password;
            this.Email = email;
            this.Role = role;
        }
        public AccountEntity(string username, string password, string email)
        {
            this.Username = username;
            this.Password = password;
            this.Email = email;
        }
        [Key]
        public int CustomerId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int Role { get; set; }


    }
}
