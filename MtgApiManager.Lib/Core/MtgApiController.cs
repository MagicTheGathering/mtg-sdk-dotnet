using MtgApiManager.Lib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("MtgApiManager.Lib.Test")]

/// <summary>
/// Managers MTG API related functionality.
/// </summary>
internal static class MtgApiController
{
    private static readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

    /// <summary>
    /// The rate limit which controls the calls to the API.
    /// </summary>
    private static RateLimit _apiRateLimit = new RateLimit();

    /// <summary>
    /// Gets or sets the number of elements returned.
    /// </summary>
    public static int Count
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the headers with previous, last, next, first links (when appropriate).
    /// </summary>
    public static string Link
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the page size for the request.
    /// </summary>
    public static int PageSize
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the rate limit for a given user.
    /// </summary>
    public static int RatelimitLimit
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the number of requests left before the rate limit is exceeded.
    /// </summary>
    public static int RatelimitRemaining
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the total number of elements (across all pages).
    /// </summary>
    public static int TotalCount
    {
        get;
        set;
    }

    public static PagingInfo CreatePagingInfo()
    {
        return new PagingInfo(TotalCount, PageSize);
    }

    public async static Task HandleRateLimit()
    {
        await _semaphoreSlim.WaitAsync().ConfigureAwait(false);

        try
        {
            int delayInMilliseconds = _apiRateLimit.GetDelay(RatelimitLimit);

            if (delayInMilliseconds > 0)
            {
                await Task.Delay(delayInMilliseconds).ConfigureAwait(false);
            }

            _apiRateLimit.AddApiCall();
        }
        finally
        {
            _semaphoreSlim.Release();
        }
    }

    public static void ResetRateLimit()
    {
        _semaphoreSlim.Wait();

        _apiRateLimit = new RateLimit();

        _semaphoreSlim.Release();
    }

    /// <summary>
    /// Gets all the related headers from the response.
    /// </summary>
    /// <param name="headers">The header to parse.</param>
    public static void ParseHeaders(HttpResponseHeaders headers)
    {
        if (headers == null)
        {
            throw new ArgumentNullException("headers");
        }

        _semaphoreSlim.Wait();

        try
        {
            if (headers.TryGetValues("Link", out IEnumerable<string> resultHeaders))
            {
                Link = resultHeaders.FirstOrDefault();
            }

            if (headers.TryGetValues("Page-Size", out resultHeaders))
            {
                PageSize = int.Parse(resultHeaders.FirstOrDefault());
            }

            if (headers.TryGetValues("Count", out resultHeaders))
            {
                Count = int.Parse(resultHeaders.FirstOrDefault());
            }

            if (headers.TryGetValues("Total-Count", out resultHeaders))
            {
                TotalCount = int.Parse(resultHeaders.FirstOrDefault());
            }

            if (headers.TryGetValues("Ratelimit-Limit", out resultHeaders))
            {
                RatelimitLimit = int.Parse(resultHeaders.FirstOrDefault());
            }

            if (headers.TryGetValues("Ratelimit-Remaining", out resultHeaders))
            {
                RatelimitRemaining = int.Parse(resultHeaders.FirstOrDefault());
            }
        }
        finally
        {
            _semaphoreSlim.Release();
        }
    }
}