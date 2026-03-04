using LinkUpApp.Core.Application.Dtos.Email;

namespace LinkUpApp.Core.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequestDto emailRequestDto);
    }
}