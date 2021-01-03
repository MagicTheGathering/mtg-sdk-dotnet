using System;
using System.Collections.Generic;
using System.Linq;
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
            catch
            {
                return default;
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public void Update(IReadOnlyList<(string Name, string Value)> headers)
        {
            if (headers == null)
            {
                throw new ArgumentNullException(nameof(headers));
            }

            _semaphoreSlim.Wait();

            try
            {
                _headersCache = headers.ToLookup(
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