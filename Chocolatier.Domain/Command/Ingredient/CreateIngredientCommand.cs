using Chocolatier.Domain.Enum;
using Flunt.Notifications;
using Flunt.Validations;

namespace Chocolatier.Domain.Command.Ingredient
{
    public class CreateIngredientCommand : BaseComamnd
    {
        public DateTime ExpireAt { get; set; }
        public Guid IngredientTypeId { get; set; }

        public void Validate()
        {
            AddNotifications(
            new Contract<Notification>()
                .Requires()
                .IsFalse(ExpireAt == DateTime.MinValue, "ExpireAt", "A data de validade do ingrediente é obrigatória.")
                .IsFalse(IngredientTypeId == Guid.Empty, "Id", "Problema interno para identificação do Tipo de Ingrediente, tente novamente."));
        }
    }
}
