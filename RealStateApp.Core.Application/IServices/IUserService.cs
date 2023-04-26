using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.ViewModels;
using RealStateApp.Core.Application.ViewModels.Home;
using RealStateApp.Core.Application.ViewModels.User;

namespace RealStateApp.Core.Application.IServices
{
    public interface IUserService
    {
        Task ActivateOrInactivate(string id);
        Task<List<UserViewModel>> ActiveClients();
        Task<string> ConfirmAsync(string userid, string token);
        Task DeleteUserAsync(UserViewModel user);
        Task<SaveUserViewModel> EditUser(SaveUserViewModel uservmsave);
        Task<ForgotPassworResponse> ForgotPasswordAsync(ForgotPasswordViewModel forgotPasswordvm, string origin);
        Task<List<UserViewModel>> GetAllUsersAsync();
        Task<List<UserViewModel>> GetAllUsersAsyncJoined();
        Task<SaveUserViewModel> GetEditAsync(string id);
        Task<AuthenticationResponse> LoginAsync(LoginViewModel loginvm);
        Task<RegisterResponse> RegisterAsync(SaveUserViewModel saveuservm, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel resetPasswordvm, string origin);
        Task SignOutAsync();
        Task<RegisterResponse> RegisterAdminOrDev(SaveUserViewModel saveuservm);
        Task<HomeViewModel> GetHomeViewModel();
        Task<List<UserViewModel>> UnactiveClients();
        Task<List<UserViewModel>> GetAllUsersAsyncJoinedFiltered(FilterViewModel filter);
        Task ActivateOrInactivateManually(string id, bool active);
        Task<UserViewModel> GetUserByIdWithRoles(string Id);
    }
}