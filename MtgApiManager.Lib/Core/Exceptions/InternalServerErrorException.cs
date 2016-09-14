// <copyright file="InternalServerErrorException.cs" company="Team7 Productions">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Core.Exceptions
{
    using System.Runtime.Serialization;

    /// <summary>
    /// An exception representing an internal server error.
    /// </summary>
    [DataContract]
    public class InternalServerErrorException : MtgException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InternalServerErrorException"/> class.
        /// </summary>
        /// <param name="message">The message of the exception.</param>
        public InternalServerErrorException(string message)
            : base(message)
        {
        }
    }
}