// <copyright file="MtgApiException.cs" company="Team7 Productions">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Core
{
    using Exceptions;
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents an MTG Api exception.
    /// </summary>
    /// <typeparam name="T">The type of exception.</typeparam>
    [DataContract]
    public class MtgApiException<T> : Exception
        where T : MtgException
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