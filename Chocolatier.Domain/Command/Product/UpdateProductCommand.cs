using Flunt.Notifications;
using Flunt.Validations;
using System.Text.Json.Serialization;

namespace Chocolatier.Domain.Command.Product
{
    public class UpdateProductCommand : BaseComamnd
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public double Price { get; set; }
        public Guid RecipeId { get; set; }
        public DateTime ExpireAt { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract<Notification>()
                .Requires()
                .IsFalse(Id == Guid.Empty, "Id", "Problema interno para identificação do Produto, tente novamente."));
        }

    }
}
