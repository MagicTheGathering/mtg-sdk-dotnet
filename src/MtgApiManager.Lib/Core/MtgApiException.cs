using System;
using System.Globalization;

namespace MtgApiManager.Lib.Core
{
    /// <summary>
    /// Object representing an MTG exception.
    /// </summary>
    public class MtgApiException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MtgApiException"/> class.
        /// </summary>
        /// <param name="message">The message of the exception.</param>
        public MtgApiException(string message)
            : base(string.Format(CultureInfo.InvariantCulture, "{0}, {1}", Properties.Resources.MtgError, message))
        {
        }
    }
}