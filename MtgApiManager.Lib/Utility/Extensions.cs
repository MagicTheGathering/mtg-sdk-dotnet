// <copyright file="Extensions.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Utility
{
    using System;
    using System.ComponentModel;
    using System.Linq;

    /// <summary>
    /// Contains extension methods.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Extracts the description attribute from an <see cref="Enum"/> type.
        /// </summary>
        /// <param name="value">A <see cref="Enum"/> value to get the description for.</param>
        /// <returns>A <see cref="string"/> for the description of the value.</returns>
        public static string GetDescription(this Enum value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            var attr = value
                .GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (!attr.Any())
            {
                return value.ToString();
            }

            return (attr.First() as DescriptionAttribute).Description;
        }
    }
}