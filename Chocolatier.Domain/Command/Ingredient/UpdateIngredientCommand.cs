using Flunt.Notifications;
using Flunt.Validations;
using System.Text.Json.Serialization;

namespace Chocolatier.Domain.Command.Ingredient
{
    public class UpdateIngredientCommand : BaseComamnd
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public Guid IngredientTypeId { get; set; }
        public int Amount { get; set; }
        public DateTime ExpireAt { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract<Notification>()
                .Requires()
                .IsFalse(Id == Guid.Empty, "Id", "Problema interno para identificação do Ingrediente, tente novamente."));
        }
    }
}
