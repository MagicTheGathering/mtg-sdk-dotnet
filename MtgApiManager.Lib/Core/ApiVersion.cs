// <copyright file="ApiVersion.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Core
{
    using System.ComponentModel;

    /// <summary>
    /// Represents the API versions.
    /// </summary>
    public enum ApiVersion
    {
        /// <summary>
        /// No API version.
        /// </summary>
        [Description("none")]
        None = 0,

        /// <summary>
        /// Version 1.0.
        /// </summary>
        [Description("v1")]
        V1_0 = 1
    }
}