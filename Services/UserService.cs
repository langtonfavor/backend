using backend.Models;
using backend.Services;
using BCrypt.Net;

namespace backend.Services
{
   public class UserService : IUserService
{
    private readonly IUserRepository userRepository;

    public UserService(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public User Register(User model)
    {
        if (string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Password))
        {
            return null; 
        }

        var existingUser = userRepository.GetUserByEmail(model.Email);
        if (existingUser != null)
        {
            return null; 
        }

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
        model.Password = hashedPassword;

        return userRepository.CreateUser(model);
    }

    public User Login(User model)
    {
        if (string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Password))
        {
            return null; 
        }

        var user = userRepository.GetUserByEmail(model.Email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
        {
            return null; 
        }

        return user;
    }
} 
}

