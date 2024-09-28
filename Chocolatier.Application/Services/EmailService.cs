﻿using Chocolatier.Domain.Interfaces.Services;
using System.Net.Mail;
using System.Net;

namespace Chocolatier.Application.Services
{
    public class EmailService : IEmailService
    {
        public void SendEmail(List<string> receivers, string subject, string body)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress("no.reply.chocolatier@gmail.com"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            foreach (var receiver in receivers)
            {
                mailMessage.To.Add(receiver);
            }

            using var smtpClient = new SmtpClient("smtp.gmail.com", 587) ;
            
            smtpClient.Credentials = new NetworkCredential("no.reply.chocolatier@gmail.com", "dgqz acfy ugun qiyv");
            smtpClient.EnableSsl = true;
            smtpClient.Send(mailMessage);
        }
    }
}
