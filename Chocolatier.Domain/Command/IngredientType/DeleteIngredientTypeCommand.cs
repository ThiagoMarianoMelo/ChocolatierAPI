using Flunt.Notifications;
using Flunt.Validations;

namespace Chocolatier.Domain.Command.IngredientType
{
    public class DeleteIngredientTypeCommand : BaseComamnd
    {
        public Guid Id { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract<Notification>()
                .Requires()
                .IsFalse(Id == Guid.Empty, "Id", "Problema interno para identificação do Tipo de Ingrediente, tente novamente."));
        }
    }
}
