// <copyright file="Exceptional.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Core
{
    using System;

    /// <summary>
    /// Contains the result of an operation that may or may not succeed. (Exception monad)
    /// </summary>
    /// <typeparam name="T">The type of success value.</typeparam>
    public struct Exceptional<T>
    {
        /// <summary>
        /// Gets the exception is one was caught.
        /// </summary>
        public Exception Exception
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether the result is successful.
        /// </summary>
        public bool IsSuccess
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the paging information.
        /// </summary>
        public PagingInfo PagingInfo
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value of the result if it has a value.
        /// </summary>
        public T Value
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Exceptional{T}"/> structure in a failure state.
        /// </summary>
        /// <param name="exception">The exception that was caught.</param>
        /// <returns>A <see cref="Exceptional{T}"/> with the given exception.</returns>
        public static Exceptional<T> Failure(Exception exception)
        {
            return new Exceptional<T>()
            {
                IsSuccess = false,
                Exception = exception
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Exceptional{T}"/> structure in a success state.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="pagingInfo">The paging information.</param>
        /// <returns>A <see cref="Exceptional{T}"/> with the given value.</returns>
        public static Exceptional<T> Success(T value, PagingInfo pagingInfo)
        {
            return new Exceptional<T>()
            {
                IsSuccess = true,
                Value = value,
                PagingInfo = pagingInfo
            };
        }

        /// <summary>
        /// Runs the given action if the result is a failure.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        /// <returns>The original result.</returns>
        /// <remarks>The result itself is returned to allow function chaining.</remarks>
        public Exceptional<T> IfFailure(Action<Exception> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            if (!IsSuccess)
            {
                action(Exception);
            }

            return this;
        }

        /// <summary>
        /// Runs the given action if the result is a success.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        /// <returns>The original result.</returns>
        /// <remarks>The result itself is returned to allow function chaining.</remarks>
        public Exceptional<T> IfSuccess(Action<T> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            if (IsSuccess)
            {
                action(Value);
            }

            return this;
        }

        /// <summary>
        /// Maps the given function to the contents of the result. If the result is a success, the
        /// function will be applied to the success value; otherwise, the function will not be applied.
        /// </summary>
        /// <typeparam name="TNewValue">The type that the success value will be mapped to.</typeparam>
        /// <param name="function">The function to apply to the success value of the result.</param>
        /// <returns>A copy of the result with the mapping applied.</returns>
        public Exceptional<TNewValue> Map<TNewValue>(Func<T, TNewValue> function)
        {
            if (function == null)
            {
                throw new ArgumentNullException("function");
            }

            if (IsSuccess)
            {
                return Exceptional<TNewValue>.Success(function(Value), PagingInfo);
            }
            else
            {
                return Exceptional<TNewValue>.Failure(Exception);
            }
        }

        /// <summary>
        /// Chains two results together by taking the value of the result, and passing it to the
        /// given function which returns another result. If the result is a failure, then the
        /// function will not be applied.
        /// </summary>
        /// <typeparam name="TNewValue">
        /// The type of the result that the value will be mapped to.
        /// </typeparam>
        /// <param name="function">The function to apply to the value.</param>
        /// <returns>A new result with the mapping applied.</returns>
        public Exceptional<TNewValue> Then<TNewValue>(Func<T, Exceptional<TNewValue>> function)
        {
            if (function == null)
            {
                throw new ArgumentNullException("function");
            }

            if (IsSuccess)
            {
                return function(Value);
            }
            else
            {
                return Exceptional<TNewValue>.Failure(Exception);
            }
        }
    }
}