﻿using System.Threading.Tasks;
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

        Task<IdentityResult> UpdateUserAsync(User user);

        Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(User user, string roleName);

        Task<bool> IsUserInRoleAsync(User user, string roleName);

        Task<SignInResult> ValidatePasswordAsync(User user, string password);   //não faz o login, apenas valida a password

        Task<string> GenerateEmailConfirmationTokenAsync(User user);    //Trata do email de confirmação e mete lá o token

        Task<IdentityResult> ConfirmEmailAsync(User user, string token);    //Depois do User confirmar o email, este método vai recebe-lo e vê se está tudo ok

        Task<User> GetUserByIdAsync(string userId);     //Método auxiliar para dar o user através do Id
    }
}
