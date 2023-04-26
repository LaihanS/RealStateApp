using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.ViewModels.User;

namespace RealStateApp.Core.Application.IServices
{
    public interface IAccountJWTService
    {
        Task ActivateOrInactivateUser(string id);
        Task<AuthenticationJWTResponse> AuthAsync(AuthenticationRequest request);
        Task<string> ConfirmUserAsync(string userid, string token);
        Task EditUser(UserViewModel edituser);
        Task<ForgotPassworResponse> ForgotPasswordAsync(ForgotPassworRequest request, string origin);
        Task<List<UserViewModel>> GetUsers(List<string> userids);
        Task<RegisterResponse> RegisterBasicUserAsync(RegisterRequest request, string origin, bool IsAdmin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest reset);
        Task SingOutAsync();
    }
}