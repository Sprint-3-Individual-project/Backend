using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BCrypt.Net;

namespace User
{
    public class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool Verification(string givenpassword, string datapassword)
        {
            return BCrypt.Net.BCrypt.Verify(givenpassword, datapassword);
        }
    }
}
