using MyEshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEshop.Data.Repository
{
    public interface IUserRepository
    {
        bool IExistUserByEmail(string email);
        void addUser(User user);
        User GetUserForLogin(string email, string password);
    }


    public class UserRepository : IUserRepository
    {
        private MyEshopContext _context;

        public UserRepository(MyEshopContext context)
        {
            _context = context;
        }
        public void addUser(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
        }

        public User GetUserForLogin(string email, string password)
        {
            return _context.Users
                .SingleOrDefault(a => a.Email == email && a.Password == password);
        }

        public bool IExistUserByEmail(string email)
        {
            return _context.Users.Any(a => a.Email == email);
        }
    }
}
