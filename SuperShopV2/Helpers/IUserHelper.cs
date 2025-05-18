using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SuperShopV2.Data.Entities;
using SuperShopV2.Models;


namespace SuperShopV2.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();
    }
}
