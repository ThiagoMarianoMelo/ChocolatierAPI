using Chocolatier.Domain.Enum;
using Flunt.Notifications;
using Flunt.Validations;

namespace Chocolatier.Domain.Command.IngredientType
{
    public class CreateIngredientTypeCommand : BaseComamnd
    {
        public string Name { get; set; } = string.Empty;
        public MeasurementeUnit MeasurementeUnit { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract<Notification>()
                .Requires()
                .IsNotNullOrWhiteSpace(Name, "Name", "O nome do tipo de ingrediente é obrigatório.")
                .IsNotNull(MeasurementeUnit, "MeasurementeUnit", "O tipo de medida do tipo de ingrediente é obrigatório."));
        }
    }
}
