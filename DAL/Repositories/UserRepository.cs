using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User;
using User.Interfaces;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ProductData _context;
        private Account _account;
        public UserRepository(ProductData context)
        {
            _context = context;
        }
        public async Task RegisterUser(Account user)
        {
            AccountEntity entity = new AccountEntity(user.CustomerId, user.Username, user.Password, user.Email, (int)user.Role);

            _context.Account.Add(entity);

            await _context.SaveChangesAsync();
        }

        public Task UserLogin(Account user)
        {
            throw new NotImplementedException();
        }
        public Account GetAccountByEmail(string email)
        {
            AccountEntity entity = _context.Account.Where(account => account.Email == email).FirstOrDefault();

            if (entity == null)
            {
                return null;
            }
            else
            {
                Account account = new Account(entity.Username, entity.Password, entity.Email, (Role)entity.Role);
                return account;
            }
        }

        public async Task<bool> ElavateUserRole(Account user)
        {
            var existingUser = _context.Account.FirstOrDefault(u => u.CustomerId == user.CustomerId);

            if (existingUser == null)
            {
                return false; // Gebruiker niet gevonden
            }

            // Controleer of de gebruiker al de gewenste rol heeft (bijvoorbeeld "Admin").
            if (existingUser.Role == (int)Role.Admin)
            {
                return true; 
            }

            existingUser.Role = (int)Role.Admin;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
