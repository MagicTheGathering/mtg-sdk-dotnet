using System;

namespace MtgApiManager.Lib.Core
{
    /// <inheritdoc />
    internal class OperationResult<T> : IOperationResult<T> where T : class
    {
        private OperationResult(
            bool isSuccess,
            T value,
            PagingInfo pagingInfo,
            Exception exception)
        {
            IsSuccess = isSuccess;
            Value = value;
            PagingInfo = pagingInfo;
            Exception = exception;
        }

        /// <inheritdoc />
        public Exception Exception { get; }

        /// <inheritdoc />
        public bool IsSuccess { get; }

        /// <inheritdoc />
        public PagingInfo PagingInfo { get; }

        /// <inheritdoc />
        public T Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationResult{T}"/> structure in a failure state.
        /// </summary>
        /// <param name="exception">The exception that was caught.</param>
        /// <returns>A <see cref="OperationResult{T}"/> with the given exception.</returns>
        public static OperationResult<T> Failure(Exception exception)
        {
            return new OperationResult<T>(
                false,
                null,
                null,
                exception);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationResult{T}"/> structure in a success state.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="pagingInfo">The paging information.</param>
        /// <returns>A <see cref="OperationResult{T}"/> with the given value.</returns>
        public static OperationResult<T> Success(T value, PagingInfo pagingInfo)
        {
            return new OperationResult<T>(
                true,
                value,
                pagingInfo,
                null);
        }
    }
}