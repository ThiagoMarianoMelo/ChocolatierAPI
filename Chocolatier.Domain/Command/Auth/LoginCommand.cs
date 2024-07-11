using Flunt.Notifications;
using Flunt.Validations;

namespace Chocolatier.Domain.Command.Auth
{
    public class LoginCommand : BaseComamnd
    {
        public string? Email { get; set; }
        public string? Password { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract<Notification>()
                .Requires()
                .IsNotNullOrWhiteSpace(Email, "Email", "Email não informado")
                .IsNotNullOrWhiteSpace(Password, "Password", "Senha não informado"));
        }
    }
}
