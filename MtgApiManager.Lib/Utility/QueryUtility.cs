// <copyright file="QueryUtility.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Utility
{
    using System;
    using System.Linq;
    using Newtonsoft.Json;

    /// <summary>
    /// Utility methods dealing with queries.
    /// </summary>
    public static class QueryUtility
    {
        /// <summary>
        /// Gets the property name from a member expression.
        /// </summary>
        /// <typeparam name="T">The type to look for the property in.</typeparam>
        /// <param name="propertyName">The name of the property to get the query parameter for.</param>
        /// <returns>A <see cref="string"/> representing the query name defined in the <see cref="JsonProperty"/></returns>
        public static string GetQueryPropertyName<T>(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException("propertyName");
            }

            var property = typeof(T).GetProperty(propertyName);

            if (property == null)
            {
                return null;
            }

            var attribute = property.GetCustomAttributes(true).SingleOrDefault(x => x is JsonPropertyAttribute);

            if (attribute == null)
            {
                return null;
            }

            return (attribute as JsonPropertyAttribute).PropertyName;
        }
    }
}