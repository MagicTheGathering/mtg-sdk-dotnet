// <copyright file="PagingInfo.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>

namespace MtgApiManager.Lib.Core
{
    /// <summary>
    /// Represents paging information
    /// </summary>
    public class PagingInfo
    {
        /// <summary>
        /// Initialize a new instance of the <see cref="PagingInfo"/> class.
        /// </summary>
        /// <param name="totalCount">The total number of items.</param>
        /// <param name="pageSize">The page size for the request.</param>
        public PagingInfo(int totalCount, int pageSize)
        {
            TotalCount = totalCount;
            PageSize = pageSize;
        }

        /// <summary>
        /// Gets or sets the page size for the request.
        /// </summary>
        public int PageSize { get; }

        /// <summary>
        /// Gets or sets the total number of items
        /// </summary>
        public int TotalCount { get; }

        /// <summary>
        /// Gets the total number of pages based on the page size and total number.
        /// </summary>
        public int TotalPages => TotalCount / PageSize;
    }
}