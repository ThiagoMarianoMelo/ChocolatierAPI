using Chocolatier.Domain.ConfigObjects.Queues;
using Chocolatier.Domain.Enum;
using Chocolatier.Domain.Events;
using Chocolatier.Domain.Interfaces.Services;
using Chocolatier.Domain.Resource;
using Chocolatier.Util.EnumUtil;
using Microsoft.Extensions.Options;

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
                EmailService.SendEmail(data.Emails, data.EmailTemplate.GetStringValue(), GetEmailBody(data.EmailTemplate));
        }

        private string GetEmailBody(EmailTemplate emailTemplate) => emailTemplate switch
        {
            EmailTemplate.OrderCreated => EmailTemplateResource.OrderCreated,
            _ => EmailTemplateResource.OrderCreated
        };
    }
}
