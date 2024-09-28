using Chocolatier.Domain.Enum;

namespace Chocolatier.Domain.Events
{
    public class SendEmailEvent
    {
        public List<string> Emails { get; set; } = [];
        public EmailTemplate EmailTemplate { get; set; }
        public Dictionary<string, string> Params { get; set; } = [];
    }
}
