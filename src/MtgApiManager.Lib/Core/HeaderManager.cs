using System;
using System.Linq;
using System.Threading;
using Flurl.Util;

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
            catch
            {
                return default;
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public void Update(IReadOnlyNameValueList<string> headers)
        {
            if (headers == null)
            {
                throw new ArgumentNullException(nameof(headers));
            }

            _semaphoreSlim.Wait();

            try
            {
                // nested GetAll is technically O(n^2) instead of the original O(n)

                _headersCache = Enumeration
                    .GetAll<ResponseHeader>()
                    .SelectMany(
                        rh => headers.GetAll(rh.Name),
                        (rh, Value) => (rh.Name, Value))
                    .ToLookup(
                        k => k.Name,
                        v => v.Value);
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }
    }
}