// <copyright file="BadRequestException.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Core.Exceptions
{
    /// <summary>
    /// An exception representing a bad request.
    /// </summary>
    public class BadRequestException : MtgExceptionBase
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