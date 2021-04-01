using System;
using System.ComponentModel;
using System.Reflection;

namespace Filuet.Hrbl.Ordering.Common
{
    public static class EnumHelper
    {
        public static T GetValueFromDescription<T>(string description) where T : Enum
        {
            foreach (var field in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (string.Equals(attribute.Description, description, StringComparison.InvariantCultureIgnoreCase))
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (string.Equals(field.Name, description, StringComparison.InvariantCultureIgnoreCase))
                        return (T)field.GetValue(null);
                }
            }

            throw new ArgumentException("Not found.", nameof(description));
            // Or return default(T);
        }

        public static string GetDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}
