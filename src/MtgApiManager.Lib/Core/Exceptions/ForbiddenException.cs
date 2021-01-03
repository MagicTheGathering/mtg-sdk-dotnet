namespace MtgApiManager.Lib.Core.Exceptions
{
    /// <summary>
    /// An exception representing a forbidden request.
    /// </summary>
    public class ForbiddenException : MtgExceptionBase
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