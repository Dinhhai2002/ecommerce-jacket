using System.Threading.Tasks;

namespace webecommerce.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body, bool isHtml = false);
        Task SendPasswordResetEmailAsync(string to, string resetToken);
        Task SendWelcomeEmailAsync(string to, string fullName);
        Task SendOrderConfirmationEmailAsync(string to, string orderNumber, decimal totalAmount);
    }
} 