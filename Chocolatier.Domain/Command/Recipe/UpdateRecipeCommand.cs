using Flunt.Notifications;
using Flunt.Validations;
using System.Text.Json.Serialization;

namespace Chocolatier.Domain.Command.Recipe
{
    public class UpdateRecipeCommand : BaseComamnd
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public List<RecipteItemCommand>? RecipeItems { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract<Notification>()
                .Requires()
                .IsFalse(Id == Guid.Empty, "Id", "Problema interno para identificação da receita, tente novamente.")
                .IsFalse(string.IsNullOrWhiteSpace(Name), "Name", "Problema interno para identificação do Nome da receita, tente novamente.")
                .IsFalse(RecipeItems is null || RecipeItems.Count() == 0 || RecipeItems.Any(rc => rc.IngredientTypeId == Guid.Empty || rc.Quantity <= 0),
                    "RecipeItems", "Problema interno para identificação dos itens da receita, tente novamente."));
        }
    }
}
