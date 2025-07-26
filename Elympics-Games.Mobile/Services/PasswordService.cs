using Elympics_Games.Mobile.DTOs.User;
using Microsoft.AspNetCore.Identity;

namespace Elympics_Games.Mobile.Services
{
    public class PasswordService
    {
        private readonly PasswordHasher<CreateUserDto> _hasher = new PasswordHasher<CreateUserDto>();

        public string HashPassword(CreateUserDto user, string plainPassword)
        {
            return _hasher.HashPassword(user, plainPassword);
        }
    }
}
