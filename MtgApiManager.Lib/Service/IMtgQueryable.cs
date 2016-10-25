// <copyright file="IMtgQueryable.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Service
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Defines an object to be query able against the MTG API.
    /// </summary>
    /// <typeparam name="TService">The type of service.</typeparam>
    /// <typeparam name="TQuery">The type of query.</typeparam>
    public interface IMtgQueryable<TService, TQuery>
        where TQuery : QueryParameterBase
    {
        /// <summary>
        /// Adds a query parameter.
        /// </summary>
        /// <typeparam name="U">The type of property to add the query for.</typeparam>
        /// <param name="property">The property to add the query for.</param>
        /// <param name="value">The value of the query.</param>
        /// <returns>The instance of its self with the new query parameter.</returns>
        TService Where<U>(Expression<Func<TQuery, U>> property, U value);
    }
}