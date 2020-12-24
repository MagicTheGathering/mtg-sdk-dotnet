using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading;

namespace MtgApiManager.Lib.Core
{
    internal class HeaderManager : IHeaderManager
    {
        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);
        private ILookup<string, string> _headersCache;

        public T Get<T>(ResponseHeader responseHeader)
        {
            if (_headersCache == null)
            {
                return default;
            }

            _semaphoreSlim.Wait();

            try
            {
                var value = _headersCache[responseHeader.Name].FirstOrDefault();
                if (string.IsNullOrWhiteSpace(value))
                {
                    return default;
                }

                return (T)Convert.ChangeType(value, typeof(T));
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public void Update(HttpResponseHeaders headers)
        {
            if (headers == null)
            {
                throw new ArgumentNullException(nameof(headers));
            }

            _semaphoreSlim.Wait();

            try
            {
                _headersCache = headers.ToLookup(
                    k => k.Key,
                    v => v.Value.FirstOrDefault());
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }
    }
}