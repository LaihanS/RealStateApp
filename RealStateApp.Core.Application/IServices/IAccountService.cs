using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.ViewModels.User;

namespace RealStateApp.Core.Application.IServices
{
    public interface IAccountService
    {
        Task<UserViewModel> FindByIdAsync(string Id);
        Task ActivateOrInactivateUser(string id);
        Task<AuthenticationResponse> AuthAsync(AuthenticationRequest request);
        Task<string> ConfirmUserAsync(string userid, string token);
        Task EditUser(UserViewModel edituser);
        Task<ForgotPassworResponse> ForgotPasswordAsync(ForgotPassworRequest request, string origin);
        Task<List<UserViewModel>> GetUsers(List<string> userids);
        Task<RegisterResponse> RegisterBasicUserAsync(RegisterRequest request, string origin, bool IsAdmin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest reset);
        Task SingOutAsync();
        Task<UserViewModel> FindByEmailAsync(string email);
        Task<UserViewModel> ActivateOrInactivateUserManually(string id, bool active);
        Task<RegisterResponse> RegisterDevOrAdmin(RegisterRequest request, List<string> roles);
    }
}