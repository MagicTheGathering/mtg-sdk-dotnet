// <copyright file="RateLimit.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Object representing a rate limit which helps spread out the calls to API.
    /// </summary>
    internal class RateLimit
    {
        /// <summary>
        /// The calls that have been made to the web service.
        /// </summary>
        private readonly List<DateTime> _webServiceCalls;

        /// <summary>
        /// Initializes a new instance of the <see cref="RateLimit"/> class.
        /// </summary>
        public RateLimit()
        {
            _webServiceCalls = new List<DateTime>();
        }

        /// <summary>
        /// Add a new call to the managed collection.
        /// </summary>
        public void AddApiCall()
        {
            _webServiceCalls.Add(DateTime.Now);
        }

        /// <summary>
        /// Returns the delay needed to make the next call. The per hour rate will get converted to a per 10 second rate in
        /// order to spread out the calls over the hour.
        /// </summary>
        /// <param name="requestsPerHour">The number of calls permitted per hour.</param>
        /// <returns>the delay in milliseconds.</returns>
        public int GetDelay(int requestsPerHour)
        {
            int delay = 0;

            if (requestsPerHour == 0)
            {
                return 0;
            }

            // Figure out the requests per 10 seconds in order to spread out the calls.
            var requestsPerTenSeconds = (int)Math.Floor((float)requestsPerHour / (3600f / 10f));

            if (_webServiceCalls.Any())
            {
                // Remove any extra traces that are not in the last seconds plus a 5 second buffer.
                _webServiceCalls.RemoveAll(x => x < DateTime.Now.AddSeconds(-10));

                // Get the request that are within the rate passed in.
                var lastTenSeconds = _webServiceCalls
                                        .Where(x => x > DateTime.Now.AddSeconds(-10))
                                        .OrderBy(x => x)
                                        .ToList();

                // If the limit has been reached then calculate the needed delay.
                if (lastTenSeconds.Count >= requestsPerTenSeconds)
                {
                    TimeSpan diff = lastTenSeconds.First() - DateTime.Now.AddSeconds(-10);
                    delay = (int)Math.Abs(diff.TotalMilliseconds);
                }
            }

            return delay;
        }
    }
}