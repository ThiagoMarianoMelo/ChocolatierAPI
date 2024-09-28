using Chocolatier.Domain.Interfaces.Services;

namespace Chocolatier.Application.Services
{
    public class EmailService : IEmailService
    {
        public void SendEmail(List<string> recivers, string subject, string body)
        {
            var teste = "";
            throw new NotImplementedException();
        }
    }
}
