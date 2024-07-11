namespace Chocolatier.Util.EnumUtil
{
    public static class EnumExtension
    {
        public static string GetStringValue(this Enum value)
        {
            Type type = value.GetType();

            var fieldInfo = type.GetField(value.ToString());

            var attribs = fieldInfo?.GetCustomAttributes(
                typeof(StringValueAttribute), false) as StringValueAttribute[];

            if (attribs == null || attribs.Length <= 0)
                return string.Empty;

            return attribs[0].StringValue;
        }
    }
}
