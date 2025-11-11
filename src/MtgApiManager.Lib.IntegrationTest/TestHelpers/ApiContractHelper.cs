using System;
using MtgApiManager.Lib.Core;
using MtgApiManager.Lib.Service;
using Xunit;

namespace MtgApiManager.Lib.Test.TestHelpers
{
    /// <summary>
    /// Helper methods for API contract integration tests.
    /// </summary>
    public static class ApiContractHelper
    {
        /// <summary>
        /// Asserts that an operation result is successful and returns the value.
        /// </summary>
        public static T AssertSuccess<T>(IOperationResult<T> result) where T : class
        {
            Assert.NotNull(result);
            Assert.True(result.IsSuccess,
                $"Expected successful operation but got: {result.Exception?.Message}");
            Assert.Null(result.Exception);
            Assert.NotNull(result.Value);
            return result.Value;
        }

        /// <summary>
        /// Asserts that an operation result failed with an exception.
        /// </summary>
        public static Exception AssertFailure<T>(IOperationResult<T> result) where T : class
        {
            Assert.NotNull(result);
            Assert.False(result.IsSuccess, "Expected operation to fail but it succeeded");
            Assert.NotNull(result.Exception);
            return result.Exception;
        }

        /// <summary>
        /// Asserts that paging info is present and valid.
        /// </summary>
        public static void AssertValidPagingInfo(PagingInfo pagingInfo)
        {
            Assert.NotNull(pagingInfo);
            Assert.True(pagingInfo.PageSize > 0, "PageSize should be greater than 0");
            Assert.True(pagingInfo.TotalCount >= 0, "TotalCount should be non-negative");
        }

        /// <summary>
        /// Validates that a string property is not null or empty.
        /// </summary>
        public static void AssertNotNullOrEmpty(string value, string propertyName)
        {
            Assert.False(string.IsNullOrWhiteSpace(value),
                $"{propertyName} should not be null or empty");
        }

        /// <summary>
        /// Creates a service provider for integration tests.
        /// </summary>
        public static IMtgServiceProvider CreateServiceProvider()
        {
            return new MtgServiceProvider();
        }

        /// <summary>
        /// Gets the timeout for API calls in integration tests.
        /// Can be configured via environment variable API_TEST_TIMEOUT (in seconds).
        /// </summary>
        public static TimeSpan GetApiTimeout()
        {
            var timeoutEnv = Environment.GetEnvironmentVariable("API_TEST_TIMEOUT");
            if (int.TryParse(timeoutEnv, out int timeoutSeconds))
            {
                return TimeSpan.FromSeconds(timeoutSeconds);
            }
            return TimeSpan.FromSeconds(30); // Default 30 seconds
        }

        /// <summary>
        /// Checks if live API tests should be skipped based on environment variable.
        /// Set SKIP_LIVE_API_TESTS=true to skip these tests.
        /// </summary>
        public static bool ShouldSkipLiveTests()
        {
            var skipEnv = Environment.GetEnvironmentVariable("SKIP_LIVE_API_TESTS");
            return string.Equals(skipEnv, "true", StringComparison.OrdinalIgnoreCase);
        }
    }
}
