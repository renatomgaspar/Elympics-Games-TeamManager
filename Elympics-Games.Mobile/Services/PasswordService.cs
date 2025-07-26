using Elympics_Games.Mobile.DTOs.User;
using Microsoft.AspNetCore.Identity;

namespace Elympics_Games.Mobile.Services
{
    public class PasswordService<TUser> where TUser : class
    {
        private readonly PasswordHasher<TUser> _hasher = new PasswordHasher<TUser>();

        public string HashPassword(TUser user, string plainPassword)
        {
            return _hasher.HashPassword(user, plainPassword);
        }
    }
}
