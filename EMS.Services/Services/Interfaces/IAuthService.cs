using EMS.Data.Entities;

namespace EMS.Services.Services.Interfaces
{
    public interface IAuthService
    {
        IDictionary<string, object> GenerateTokenString(User user);
        Task<bool> SignIn(User user);
        Task<bool> SignUp(User user);
        Task<bool> Signout();
    }
}
