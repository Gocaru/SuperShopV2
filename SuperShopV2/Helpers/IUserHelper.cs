using Microsoft.AspNetCore.Identity;
using SuperShopV2.Data.Entities;
using System.Threading.Tasks;

namespace SuperShopV2.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);
    }
}
