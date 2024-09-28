namespace Chocolatier.Domain.Interfaces.Services
{
    public interface IEmailService
    {
        void SendEmail(List<string> receivers, string subject, string body);
    }
}
