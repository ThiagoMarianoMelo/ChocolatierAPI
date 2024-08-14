using Chocolatier.Domain.Enum;

namespace Chocolatier.Domain.Entities
{
    public class IngredientType : BaseEntity
    {
        public IngredientType()
        {
        }

        public IngredientType(Guid id, string name, MeasurementeUnit measurementeUnit)
        {
            Id = id;
            Name = name;
            MeasurementeUnit = measurementeUnit;
        }

        public string Name { get; set; } = string.Empty;
        public MeasurementeUnit MeasurementeUnit { get; set; }
        public bool IsActive {  get; set; }
    }
}
