using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OwinWebApi.Models;

namespace OwinWebApi.Services
{
    public class UserStore
    {
        private UserDbContext _context;

        public UserStore(UserDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsUserExist(User user)
        {
            return await _context.Users.AnyAsync(u => u.Id == user.Id || u.Email == user.Email);
        }

        public bool IsPasswordValid(User user, string password)
        {
            var hasher = new PasswordHasher();
            var hash = hasher.Hash(password);
            return hash.Equals(user.PasswordHash);
        }
        public void AddUser(User user, string password)
        {
            if (_context.Users.Any(u => u.Id == user.Id || u.Email == user.Email))
            {
                throw new Exception("A user with that Email address already exists");
            }

            var hasher = new PasswordHasher();
            user.PasswordHash = hasher.Hash(password).ToString();
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public async Task AddUserAsync(User user, string password)
        {
            if (await IsUserExist(user))
            {
                throw new Exception("A user with that Email address already exists");
            }

            var hasher = new PasswordHasher();
            user.PasswordHash = hasher.Hash(password).ToString();
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _context.Users.Include(u => u.Claims)
                                              .FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<User> FindByIdAsync(string userId)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task AddClaimAsync(string userId, UserClaim claim)
        {
            User user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.Claims.Add(claim);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("User does not exist");
            }
        }
    }
}
