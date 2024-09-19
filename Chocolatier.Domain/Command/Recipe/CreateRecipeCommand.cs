using Flunt.Notifications;
using Flunt.Validations;

namespace Chocolatier.Domain.Command.Recipe
{
    public class CreateRecipeCommand : BaseComamnd
    {
        public string? Name { get; set; }
        public List<RecipteItemCommand>? RecipeItens { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract<Notification>()
                .Requires()
                .IsFalse(string.IsNullOrWhiteSpace(Name), "Name", "Problema interno para identificação do Nome da receita, tente novamente.")
                .IsFalse(RecipeItens is null || RecipeItens.Count == 0, "RecipeItems", "Problema interno para identificação dos itens da receita, tente novamente."));
        }
    }
}
