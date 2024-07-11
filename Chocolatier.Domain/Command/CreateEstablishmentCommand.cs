using Chocolatier.Domain.Enum;
using Flunt.Notifications;
using Flunt.Validations;

namespace Chocolatier.Domain.Command
{
    public class CreateEstablishmentCommand : BaseComamnd
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public EstablishmentType EstablishmentType { get; set; }
        public string Address { get; set; } = string.Empty;

        public void Validate()
        {
            AddNotifications(
                new Contract<Notification>()
                .Requires()
                .IsNotNullOrWhiteSpace(UserName, "Name", "O nome do estabelecimento é obrigatório.")
                .IsNotNullOrWhiteSpace(Email, "Email", "O e-mail do estabelecimento é obrigatório.")
                .IsNotNullOrWhiteSpace(Password, "Password", "A senha de acessso do estabelecimento é obrigatório.")
                .IsNotNullOrWhiteSpace(Address, "Adress", "O endereço do estabelecimento é obrigatório."));
        }
    }
}
