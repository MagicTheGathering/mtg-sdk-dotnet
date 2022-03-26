using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MtgApiManager.Lib.Core
{
    internal class RateLimit : IRateLimit
    {
        private static readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);
        private readonly List<DateTime> _webServiceCalls;

        public RateLimit(bool isTurnedOn)
        {
            IsTurnedOn = isTurnedOn;

            _webServiceCalls = new List<DateTime>();
        }

        public bool IsTurnedOn { get; }

        public void AddApiCall()
        {
            if (!IsTurnedOn)
            {
                return;
            }

            _semaphoreSlim.Wait();

            try
            {
                _webServiceCalls.Add(DateTime.Now);
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public async Task<int> Delay(int requestsPerHour, CancellationToken cancellationToken)
        {
            if (!IsTurnedOn)
            {
                return 0;
            }

            await _semaphoreSlim.WaitAsync(cancellationToken).ConfigureAwait(false);

            try
            {
                int delayInMilliseconds = GetDelay(requestsPerHour);

                if (delayInMilliseconds > 0)
                {
                    await Task.Delay(delayInMilliseconds, cancellationToken).ConfigureAwait(false);
                }

                return delayInMilliseconds;
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public void Reset()
        {
            _webServiceCalls.Clear();
        }

        private int GetDelay(int requestsPerHour)
        {
            int delay = 0;

            if (requestsPerHour == 0)
            {
                return 0;
            }

            // Figure out the requests per 10 seconds in order to spread out the calls.
            var requestsPerTenSeconds = (int)Math.Floor((float)requestsPerHour / (TimeSpan.FromHours(1).TotalSeconds / 10f));

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
                    var diff = lastTenSeconds.First() - DateTime.Now.AddSeconds(-10);
                    delay = (int)Math.Abs(diff.TotalMilliseconds);
                }
            }

            return delay;
        }
    }
}