using Flunt.Notifications;
using Flunt.Validations;

namespace Chocolatier.Domain.Command.Recipe
{
    public class CreateRecipeCommand : BaseComamnd
    {
        public string? Name { get; set; }
        public List<CreateRecipeItemObject>? RecipeItems { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract<Notification>()
                .Requires()
                .IsFalse(string.IsNullOrWhiteSpace(Name), "Name", "Problema interno para identificação do Nome da receita, tente novamente.")
                .IsFalse(RecipeItems is null, "RecipeItems", "Problema interno para identificação dos itens da receita, tente novamente."));
        }
    }

    public class CreateRecipeItemObject()
    {
        public Guid IngredientTypeId { get; set; }
        public int Quantity { get; set; }
    }
}
