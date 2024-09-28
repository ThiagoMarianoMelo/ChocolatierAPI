namespace Chocolatier.Domain.Interfaces.Services
{
    public interface IEmailService
    {
        public void SendEmail(List<string> recivers, string subject, string body);
    }
}
