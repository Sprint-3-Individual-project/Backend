using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using User.Exceptions;
using User.Interfaces;

namespace User
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userrepository;
        public UserManager(IUserRepository userrepository) 
        {
            _userrepository = userrepository;
        }

        public async Task RegisterUser(Account user)
        {
            await _userrepository.RegisterUser(user);
        }

        public async Task UserLogin(Account user)
        {
            throw new NotImplementedException();
        }

        public Account GetAccountByEmail(string email)
        {
            if(email != null)
            {
                Account account = _userrepository.GetAccountByEmail(email);
                return account;
            }
            else
            {
                throw new EmailWasNotGivenException();
            }
        }

        public async Task<bool> ElavateUserRole(Account user)
        {
            bool result = await _userrepository.ElavateUserRole(user);

            return result;
        }
    }
}
