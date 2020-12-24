using System;

namespace MtgApiManager.Lib.Core
{
    /// <summary>
    /// Represents the result of an operation.
    /// </summary>
    /// <typeparam name="T">The data type.</typeparam>
    public interface IOperationResult<out T> where T : class
    {
        /// <summary>
        /// Gets the exception is one was caught.
        /// </summary>
        Exception Exception { get; }

        /// <summary>
        /// Gets a value indicating whether the operation was successful.
        /// </summary>
        bool IsSuccess { get; }

        /// <summary>
        /// Gets the paging information.
        /// </summary>
        PagingInfo PagingInfo { get; }

        /// <summary>
        /// Gets the value of the result if it has a value.
        /// </summary>
        T Value { get; }
    }
}