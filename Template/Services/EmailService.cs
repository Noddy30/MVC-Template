using System;
using System.Net;
using System.Net.Mail;

namespace Template.Services
{
	public interface IEmailService
	{
		Task SendEmailAsync(string email, string subject, string message);
	}

    public class EmailService : IEmailService
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var ourMail = "augusstaopen@gmail.com";
                var password = "xarsurkwoorlumja";

                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(ourMail, password),
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };

                return client.SendMailAsync(
                    new MailMessage(from: ourMail,
                                    to: email,
                                    subject,
                                    message
                                    ));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}

