using Flunt.Notifications;
using Flunt.Validations;

namespace Chocolatier.Domain.Command.Recipe
{
    public class DeleteRecipeCommand : BaseComamnd
    {
        public Guid Id { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract<Notification>()
                .Requires()
                .IsFalse(Id == Guid.Empty, "Id", "Problema interno para identificação da receita, tente novamente."));
        }
    }
}
