using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Interfaces
{
    public interface IUserRepository
    {
        Task RegisterUser(Account user);
        Task UserLogin(Account user);
        Account GetAccountByEmail(string email);
        Task<bool> ElavateUserRole(Account user);
    }
}
