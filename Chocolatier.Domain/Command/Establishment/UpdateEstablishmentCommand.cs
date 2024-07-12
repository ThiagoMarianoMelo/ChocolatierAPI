using Flunt.Notifications;
using Flunt.Validations;

namespace Chocolatier.Domain.Command.Establishment
{
    public class UpdateEstablishmentCommand : BaseComamnd
    {
        public string Id { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public void Validate()
        {
            AddNotifications(
                new Contract<Notification>()
                .Requires()
                .IsNotNullOrWhiteSpace(Id, "Id", "Problema interno para identificação do estabelecimento, tente novamente."));
        }
    }
}
