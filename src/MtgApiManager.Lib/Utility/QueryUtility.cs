using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace MtgApiManager.Lib.Utility
{
    /// <summary>
    /// Utility methods dealing with queries.
    /// </summary>
    internal static class QueryUtility
    {
        /// <summary>
        /// Gets the property name from a member expression.
        /// </summary>
        /// <typeparam name="T">The type to look for the property in.</typeparam>
        /// <param name="propertyName">The name of the property to get the query parameter for.</param>
        /// <returns>A <see cref="string"/> representing the query name defined in the JSON property</returns>
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