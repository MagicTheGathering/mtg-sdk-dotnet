using System;

namespace MtgApiManager.Lib.Core
{
    /// <summary>
    /// Represents paging information
    /// </summary>
    public class PagingInfo
    {
        private PagingInfo(int totalCount, int pageSize)
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
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

        /// <summary>
        /// Creates a new page info class.
        /// </summary>
        /// <param name="totalCount">The total number of items.</param>
        /// <param name="pageSize">The page size for the request.</param>
        /// <returns>A new instance of a paging info.</returns>
        public static PagingInfo Create(int totalCount, int pageSize)
        {
            return new PagingInfo(totalCount, pageSize);
        }
    }
}