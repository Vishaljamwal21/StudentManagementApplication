using StudentManagementSystum.Data;
using StudentManagementSystum.Models;
using StudentManagementSystum.Repository.IRepository;
using System;
using System.Linq;

namespace StudentManagementSystum.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public User Register(string email, string password)
        {
            // Check if this is the first user being registered
            bool isFirstUser = !_context.Users.Any();

            string role = isFirstUser ? "Admin" : _context.Users.Count() < 2 ? "Teacher" : "Student";

            User user = new User()
            {
                Email = email,
                Password = password,
                Role = role
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public User Authenticate(string email, string password)
        {
            var userindb = _context.Users.FirstOrDefault(x => x.Email == email && x.Password == password);
            if (userindb == null)
                return null;
            // JWT will be added here

            userindb.Password = ""; // Clearing the password for security reasons before returning the user
            return userindb;
        }

        public bool IsUniqueUser(string email)
        {
            return !_context.Users.Any(x => x.Email == email);
        }
    }
}
