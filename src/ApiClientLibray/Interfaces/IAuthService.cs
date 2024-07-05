using Shared;

namespace ApiClientLibray.Interfaces
{
    public interface IAuthService
    {
        Task<UserModel> LoginAsync(string username, string password);
        Task RegisterAsync(UserModel user);
    }
}
