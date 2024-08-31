using Flunt.Notifications;
using Flunt.Validations;

namespace Chocolatier.Domain.Command.Product
{
    public class CreateProductCommand : BaseComamnd
    {
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public Guid RecipeId { get; set; }
        public DateTime ExpireAt { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract<Notification>()
                .Requires()
                .IsFalse(string.IsNullOrWhiteSpace(Name), "Name", "Problema interno para identificação do Nome do produto, tente novamente.")
                .IsFalse(Price <= 0, "Price", "Problema interno para identificação do preço do produto, tente novamente.")
                .IsFalse(RecipeId == Guid.Empty, "RecipeId", "Problema interno para identificação dos dados do produto, tente novamente.")
                .IsFalse(ExpireAt == DateTime.MinValue, "ExpireAt", "Problema interno para identificação da validade do produto, tente novamente."));

        }
    }
}
