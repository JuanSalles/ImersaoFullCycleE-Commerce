using AuthServer.Core.Entities;
using AuthServer.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AuthServer.Application.Services
{
    public class AuthService : IAuthService
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public void ChangePassword()
        {
            throw new NotImplementedException();
        }

        public void ConfirmEmail()
        {
            throw new NotImplementedException();
        }

        public async Task Login(string username, string password)
        {
            bool isPersistent = false;
            bool lockoutOnFailure = false;
            var result = await _signInManager.PasswordSignInAsync(username, password, isPersistent, lockoutOnFailure);

            if (!result.Succeeded)
            {
                throw new Exception("User credentials were not authenticated");
            }

        }

        public void Logout()
        {
            throw new NotImplementedException();
        }

        public void RefreshToken()
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> Register(string username, string password, string email)
        {
            ApplicationUser user = new()
            {
                UserName = username,
                Email = email
            };

            return _userManager.CreateAsync(user, password);
        }

        public void ResetPassword()
        {
            throw new NotImplementedException();
        }
    }
}
