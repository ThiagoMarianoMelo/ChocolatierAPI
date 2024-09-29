using Chocolatier.Domain.ConfigObjects.Queues;
using Chocolatier.Domain.Enum;
using Chocolatier.Domain.Events;
using Chocolatier.Domain.Interfaces.Services;
using Chocolatier.Domain.Resource;
using Chocolatier.Util.EnumUtil;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Options;
using System.ComponentModel;

namespace Chocolatier.API.Consumer
{
    public class EmailQueueConsumer : RabbitMqConsumer
    {
        private readonly IEmailService EmailService;
        public EmailQueueConsumer(IOptions<EmailQueueConfiguration> queueConfiguration, IEmailService emailService) : base(queueConfiguration.Value)
        {
            EmailService = emailService;
        }

        public override void CreateConsumer()
        {
            StartConsumer<SendEmailEvent>();
        }

        public override void ActionAfterConsume(object? dataConsumed)
        {

            if (dataConsumed is SendEmailEvent data)
            {
                var emailBody = GetEmailBody(data.EmailTemplate);

                FormatEmailBodyWithParams(ref emailBody, data.Params);

                EmailService.SendEmail(data.Emails, data.EmailTemplate.GetStringValue(), emailBody);
            }
        }

        private static string GetEmailBody(EmailTemplate emailTemplate) => emailTemplate switch
        {
            EmailTemplate.OrderCreated => EmailTemplateResource.OrderCreated,
            EmailTemplate.OrderCanceled => EmailTemplateResource.OrderCanceled,
            EmailTemplate.OrderStatusChanged => EmailTemplateResource.OrderStatusChanged,
            _ => throw new InvalidEnumArgumentException()
        };

        private static void FormatEmailBodyWithParams(ref string body, Dictionary<string, string> emailParams )
        {

            foreach (var emailParam in emailParams)
            {
                body = body.Replace(emailParam.Key, emailParam.Value);
            }
        }
    }
}
