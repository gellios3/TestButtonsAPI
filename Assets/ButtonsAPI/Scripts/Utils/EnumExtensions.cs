using System;
using System.Reflection;

namespace AETOS.Scripts.API
{
    public static class EnumExtensions
    {
        public static string GetString(this Enum value)
        {
            // Get the type
            Type type = value.GetType();

            // Get field info for this type
            FieldInfo fieldInfo = type.GetField(value.ToString());

            // Get the string value attributes

            // Return the first if there was a match.
            return fieldInfo.GetCustomAttributes(
                typeof(StringValueAttribute), false) is StringValueAttribute[] { Length: > 0 } attribs
                ? attribs[0].stringValue
                : value.ToString();
        }
    }
}