using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace webecommerce.Services.Impl
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpHost;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;
        private readonly string _fromEmail;
        private readonly string _fromName;

        public EmailService(IConfiguration configuration)
        {
            _smtpHost = configuration["Email:SmtpHost"];
            _smtpPort = int.Parse(configuration["Email:SmtpPort"]);
            _smtpUsername = configuration["Email:Username"];
            _smtpPassword = configuration["Email:Password"];
            _fromEmail = configuration["Email:FromEmail"];
            _fromName = configuration["Email:FromName"];
        }

        public async Task SendEmailAsync(string to, string subject, string body, bool isHtml = false)
        {
            try
            {
                using var client = new SmtpClient(_smtpHost, _smtpPort)
                {
                    Credentials = new NetworkCredential(_smtpUsername, _smtpPassword),
                    EnableSsl = true
                };

                var message = new MailMessage
                {
                    From = new MailAddress(_fromEmail, _fromName),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = isHtml
                };
                message.To.Add(to);

                await client.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                // Log the error and rethrow
                // TODO: Add proper logging
                throw new Exception($"Failed to send email: {ex.Message}", ex);
            }
        }

        public async Task SendPasswordResetEmailAsync(string to, string resetToken)
        {
            var subject = "Password Reset Request";
            var body = $@"
                <h2>Password Reset Request</h2>
                <p>You have requested to reset your password. Please use the following token to reset your password:</p>
                <p><strong>{resetToken}</strong></p>
                <p>If you did not request this, please ignore this email.</p>
                <p>This token will expire in 1 hour.</p>";

            await SendEmailAsync(to, subject, body, true);
        }

        public async Task SendWelcomeEmailAsync(string to, string fullName)
        {
            var subject = "Welcome to Our Store!";
            var body = $@"
                <h2>Welcome {fullName}!</h2>
                <p>Thank you for registering with our store. We're excited to have you as a member!</p>
                <p>You can now:</p>
                <ul>
                    <li>Browse our extensive catalog</li>
                    <li>Save items to your wishlist</li>
                    <li>Track your orders</li>
                    <li>And much more!</li>
                </ul>
                <p>If you have any questions, feel free to contact our support team.</p>";

            await SendEmailAsync(to, subject, body, true);
        }

        public async Task SendOrderConfirmationEmailAsync(string to, string orderNumber, decimal totalAmount)
        {
            var subject = $"Order Confirmation #{orderNumber}";
            var body = $@"
                <h2>Order Confirmation</h2>
                <p>Thank you for your order!</p>
                <p>Order Number: <strong>{orderNumber}</strong></p>
                <p>Total Amount: <strong>${totalAmount:N2}</strong></p>
                <p>We'll send you another email when your order ships.</p>
                <p>You can track your order status in your account dashboard.</p>";

            await SendEmailAsync(to, subject, body, true);
        }
    }
} 