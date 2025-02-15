using System.Net;
using System.Net.Mail;
using ECommerceBackend.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;

namespace ECommerceBackend.Infrastructure.Services;

public class MailService(IConfiguration configuration) : IMailService
{
    readonly IConfiguration _configuration = configuration;

    public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
    {
        await SendMailAsync([to], subject, body, isBodyHtml);

    }

    public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
    {
        MailMessage mail = new();
        mail.IsBodyHtml = isBodyHtml;
        foreach (var to in tos)
        {
            mail.To.Add(to);
        }
        mail.Subject = subject;
        mail.Body = body;
        mail.From = new(_configuration["Mail:Username"]!, "BootCamp E-Commerce", System.Text.Encoding.UTF8);

        SmtpClient smtp = new()
        {
            Credentials = new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]),
            Port = 587,
            EnableSsl = true,
            Host = _configuration["Mail:Host"]!
        };
        await smtp.SendMailAsync(mail);
    }

    public async Task SendPasswordResetMailAsync(string to, string userId, string resetToken)
    {
        var angularClientUrl = _configuration["AngularClientUrl"];
        var mailContent = $"""
    Merhaba<br>
    Eğer yeni şifre talebinde bulunduysanız aşağıdaki linkten şifrenizi yenileyebilirsiniz.<br>
    <strong>
        <a target="_blank" href="{angularClientUrl}/account/update-password/{userId}/{resetToken}">
            Yeni şifre talebi için tıklayınız...
        </a>
    </strong><br><br>
    <span style="font-size:12px;">
        NOT : Eğer ki bu talep tarafınızca gerçekleştirilmemişse lütfen bu maili ciddiye almayınız.
    </span><br>
    Saygılarımızla...<br><br><br>
    BootCamp - E-Commerce
    """;

        await SendMailAsync(to, "Şifre Yenileme Talebi", mailContent);

    }
}