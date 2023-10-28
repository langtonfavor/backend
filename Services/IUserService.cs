using backend.Models;

namespace backend.Services
{
    public interface IUserService
    {
        User Register(User model);
        User Login(User model);
    }
}
