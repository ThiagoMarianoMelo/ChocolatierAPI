using Flunt.Notifications;
using Flunt.Validations;

namespace Chocolatier.Domain.Command.Customer
{
    public class CreateCustomerCommand : BaseComamnd
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public void Validate()
        {
            AddNotifications(
            new Contract<Notification>()
                .Requires()
                .IsNotNullOrWhiteSpace(Name, "Name", "O Nome do cliente é obrigatório.")
                .IsNotNullOrWhiteSpace(Email, "Email", "O Email do cliente é obrigatório.")
                .IsNotNullOrWhiteSpace(Phone, "Phone", "O Telefone do cliente é obrigatório.")
                .IsNotNullOrWhiteSpace(Address, "Address", "O Endereço do cliente é obrigatório."));
        }
    }
}
