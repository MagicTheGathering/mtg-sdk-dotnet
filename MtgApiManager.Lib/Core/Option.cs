// <copyright file="Option.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Core
{
    using System;

    /// <summary>
    /// Contains the result of an operation that may or may not produce a result (option monad).
    /// </summary>
    /// <typeparam name="T">The type of success value.</typeparam>
    public struct Option<T>
    {
        /// <summary>
        /// Gets a value indicating whether a value is present.
        /// </summary>
        public bool HasValue
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
        /// Initializes a new instance of the <see cref="Option{T}"/> structure without a value.
        /// </summary>
        /// <returns>A <see cref="Option{T}"/> with no value.</returns>
        public static Option<T> None()
        {
            return new Option<T>()
            {
                HasValue = false
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Option{T}"/> structure with a value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="Option{T}"/> with the given value.</returns>
        public static Option<T> Some(T value)
        {
            return new Option<T>()
            {
                Value = value,
                HasValue = true
            };
        }

        /// <summary>
        /// Runs the given action if the result has no value.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        /// <returns>The original result.</returns>
        /// <remarks>The result itself is returned to allow function chaining.</remarks>
        public Option<T> IfNone(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            if (!HasValue)
            {
                action();
            }

            return this;
        }

        /// <summary>
        /// Runs the given action if the result has a value.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        /// <returns>The original result.</returns>
        /// <remarks>The result itself is returned to allow function chaining.</remarks>
        public Option<T> IfSome(Action<T> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            if (HasValue)
            {
                action(Value);
            }

            return this;
        }

        /// <summary>
        /// Maps the given function to the contents of the result. If the result has a value, the function will be applied to that value; otherwise, the function will not be applied.
        /// </summary>
        /// <typeparam name="TNewValue">The type that the value will be mapped to.</typeparam>
        /// <param name="function">The function to apply to the value.</param>
        /// <returns>A copy of the result with the mapping applied.</returns>
        public Option<TNewValue> Map<TNewValue>(Func<T, TNewValue> function)
        {
            if (function == null)
            {
                throw new ArgumentNullException("function");
            }

            if (HasValue)
            {
                return Option<TNewValue>.Some(function(Value));
            }
            else
            {
                return Option<TNewValue>.None();
            }
        }

        /// <summary>
        /// Chains two results together by taking the value of the result, and passing it to the given function which returns another result. If the result is nothing, then the function will not be applied.
        /// </summary>
        /// <typeparam name="TNewValue">The type of the optional that the value will be mapped to.</typeparam>
        /// <param name="function">The function to apply to the value.</param>
        /// <returns>A new result with the mapping applied.</returns>
        public Option<TNewValue> Then<TNewValue>(Func<T, Option<TNewValue>> function)
        {
            if (function == null)
            {
                throw new ArgumentNullException("function");
            }

            if (HasValue)
            {
                return function(Value);
            }
            else
            {
                return Option<TNewValue>.None();
            }
        }
    }
}