// <copyright file="BadRequestException.cs" company="Team7 Productions">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Core.Exceptions
{
    using System.Runtime.Serialization;

    /// <summary>
    /// An exception representing a bad request.
    /// </summary>
    [DataContract]
    public class BadRequestException : MtgException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BadRequestException"/> class.
        /// </summary>
        /// <param name="message">The message of the exception.</param>
        public BadRequestException(string message)
            : base(message)
        {
        }
    }
}