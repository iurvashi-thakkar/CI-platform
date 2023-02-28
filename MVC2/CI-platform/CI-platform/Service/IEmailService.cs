using CI_platform.Models;

namespace CI_platform.Service
{
    public interface IEmailService
    {
        Task SendTestEmail(UserEmailOptions userEmailOptions);
    }
}