// <copyright file="MtgException.cs" company="Team7 Productions">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Core.Exceptions
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;

    /// <summary>
    /// Object representing an mtg exception.
    /// </summary>
    [DataContract]
    public class MtgException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MtgException"/> class.
        /// </summary>
        /// <param name="message">The message of the exception.</param>
        public MtgException(string message)
            : base(string.Format(CultureInfo.InvariantCulture, "{0}, {1}", Properties.Resources.MtgError, message))
        {
        }
    }
}