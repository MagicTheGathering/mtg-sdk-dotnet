namespace MtgApiManager.Lib.Core.Exceptions
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Object representing an MTG exception.
    /// </summary>
    public class MtgExceptionBase : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MtgExceptionBase"/> class.
        /// </summary>
        /// <param name="message">The message of the exception.</param>
        public MtgExceptionBase(string message)
            : base(string.Format(CultureInfo.InvariantCulture, "{0}, {1}", Properties.Resources.MtgError, message))
        {
        }
    }
}