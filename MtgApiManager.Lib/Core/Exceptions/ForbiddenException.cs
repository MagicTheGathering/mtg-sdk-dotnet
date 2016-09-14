// <copyright file="ForbiddenException.cs" company="Team7 Productions">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Core.Exceptions
{
    using System.Runtime.Serialization;

    /// <summary>
    /// An exception representing a forbidden request.
    /// </summary>
    [DataContract]
    public class ForbiddenException : MtgException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ForbiddenException"/> class.
        /// </summary>
        /// <param name="message">The message of the exception.</param>
        public ForbiddenException(string message)
            : base(message)
        {
        }
    }
}