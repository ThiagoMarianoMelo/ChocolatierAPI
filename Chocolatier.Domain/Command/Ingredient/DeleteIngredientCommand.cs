using Flunt.Notifications;
using Flunt.Validations;

namespace Chocolatier.Domain.Command.Ingredient
{
    public class DeleteIngredientCommand : BaseComamnd
    {
        public Guid Id { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract<Notification>()
                .Requires()
                .IsFalse(Id == Guid.Empty, "Id", "Problema interno para identificação do Ingrediente, tente novamente."));
        }
    }
}
