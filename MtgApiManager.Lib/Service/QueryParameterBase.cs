// <copyright file="QueryParameterBase.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Service
{
    using Newtonsoft.Json;

    /// <summary>
    /// Base class for query parameters.
    /// </summary>
    public class QueryParameterBase
    {
        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        [JsonProperty("page")]
        public int Page
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the page size.
        /// </summary>
        [JsonProperty("pageSize")]
        public int PageSize
        {
            get;
            set;
        }
    }
}