using Microsoft.AspNetCore.Identity;

namespace Elympics_Games.Mobile.Services
{
    public class PasswordService<TUser> where TUser : class
    {
        private readonly PasswordHasher<TUser> _hasher = new PasswordHasher<TUser>();

        public bool VerifyPassword(TUser user, string hashedPassword, string inputPassword)
        {
            var result = _hasher.VerifyHashedPassword(user, hashedPassword, inputPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}
