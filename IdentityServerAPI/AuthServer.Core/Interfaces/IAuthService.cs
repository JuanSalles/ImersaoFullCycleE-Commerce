using Microsoft.AspNetCore.Identity;

namespace AuthServer.Core.Interfaces;

public interface IAuthService
{
    Task<IdentityResult> Register(string username, string password, string email);
    Task Login(string usarname, string password);
    void Logout();
    void ResetPassword();
    void ChangePassword();
    void ConfirmEmail();
    void RefreshToken();

}
