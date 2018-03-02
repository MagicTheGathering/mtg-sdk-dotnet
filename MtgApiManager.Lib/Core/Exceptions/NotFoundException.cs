// <copyright file="NotFoundException.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Core.Exceptions
{
    /// <summary>
    /// An exception representing a not found request.
    /// </summary>
    public class NotFoundException : MtgExceptionBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message of the exception.</param>
        public NotFoundException(string message)
            : base(message)
        {
        }
    }
}