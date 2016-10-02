// <copyright file="MtgApiException.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Core
{
    using System;
    using Exceptions;

    /// <summary>
    /// Represents an MTG API exception.
    /// </summary>
    /// <typeparam name="T">The type of exception.</typeparam>
    public class MtgApiException<T> : Exception
        where T : MtgExceptionBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MtgApiException{T}"/> class.
        /// </summary>
        /// <param name="message">The message of the exception.</param>
        public MtgApiException(string message)
            : base(message)
        {
        }
    }
}