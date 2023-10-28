using System.Linq;
using backend.Models;
using backend.Services;

namespace backend.Services
{
    public interface IUserRepository
    {
        User GetUserByEmail(string email);
        User CreateUser(User user);
    }

    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext context;

        public UserRepository(AppDbContext context)
        {
            this.context = context;
        }

        public User GetUserByEmail(string email)
        {
            return context.Users.SingleOrDefault(u => u.Email == email);
        }

        public User CreateUser(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
            return user;
        }
    }
}
