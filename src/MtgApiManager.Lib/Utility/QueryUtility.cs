using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace MtgApiManager.Lib.Utility
{
    internal static class QueryUtility
    {
        public static string GetQueryPropertyName<T>(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            var property = typeof(T).GetProperty(propertyName);

            if (property == null)
            {
                return null;
            }

            var attribute = property.GetCustomAttributes(true).SingleOrDefault(x => x is JsonPropertyNameAttribute);

            if (attribute == null)
            {
                return null;
            }

            return (attribute as JsonPropertyNameAttribute).Name;
        }
    }
}