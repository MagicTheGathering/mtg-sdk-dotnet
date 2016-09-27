// <copyright file="EnumTestObject.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Utility
{
    using System.ComponentModel;
    using Newtonsoft.Json;

    /// <summary>
    /// Enumeration used for testing
    /// </summary>
    public enum EnumTestObject
    {
        /// <summary>
        /// No description.
        /// </summary>
        NoDescription = 0,

        [JsonProperty(PropertyName = "notDescriptionAttribute")]
        DifferentAttribute = 1,

        /// <summary>
        /// Has description.
        /// </summary>
        [Description("greatDescription")]
        HasDescription = 2,
    }
}