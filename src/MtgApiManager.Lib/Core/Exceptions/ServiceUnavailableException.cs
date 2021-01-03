﻿namespace MtgApiManager.Lib.Core.Exceptions
{
    /// <summary>
    /// An exception representing that the service is unavailable.
    /// </summary>
    public class ServiceUnavailableException : MtgExceptionBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceUnavailableException"/> class.
        /// </summary>
        /// <param name="message">The message of the exception.</param>
        public ServiceUnavailableException(string message)
            : base(message)
        {
        }
    }
}