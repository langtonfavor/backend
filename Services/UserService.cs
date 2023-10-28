using backend.Models;
using backend.Services;
using BCrypt.Net;
using Microsoft.Extensions.Logging;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    private readonly ILogger<UserService> logger;

    public UserService(IUserRepository userRepository, ILogger<UserService> logger)
    {
        this.userRepository = userRepository;
        this.logger = logger;
    }

    public User Register(User model)
    {
        if (string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Password))
        {
            return null;
        }

        // Check if a user with the same email already exists
        var existingUser = userRepository.GetUserByEmail(model.Email);
        if (existingUser != null)
        {
            return null;
        }

        // Hash the user's password before storing it
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
        model.Password = hashedPassword;

        try
        {
            return userRepository.CreateUser(model);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "User registration failed, the email is already in use.");
            return null;
        }
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