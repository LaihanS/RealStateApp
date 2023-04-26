using RealStateApp.Core.Application.Dtos.Email;

namespace RealStateApp.Core.Application.IServices
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest email);
    }
}