// <copyright file="ApiVersions.cs" company="Team7 Productions">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Core
{
    using System.ComponentModel;

    /// <summary>
    /// Represents the api versions.
    /// </summary>
    public enum ApiVersions
    {
        /// <summary>
        /// No api version.
        /// </summary>
        [Description("None")]
        None = 0,

        /// <summary>
        /// Version 1.0.
        /// </summary>
        [Description("v1")]
        V1_0 = 1
    }
}