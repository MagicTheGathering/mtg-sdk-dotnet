namespace MtgApiManager.Lib.Core.Exceptions
{
    /// <summary>
    /// An exception representing an internal server error.
    /// </summary>
    public class InternalServerErrorException : MtgExceptionBase
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