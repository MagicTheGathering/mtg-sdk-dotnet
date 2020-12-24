using System.Threading.Tasks;
using MtgApiManager.Lib.Core;
using Xunit;

namespace MtgApiManager.Lib.Test.Core
{
    public class RateLimitTest
    {
        [Fact]
        public async Task Delay_OverLimit_Delay()
        {
            // arrange
            const int REQUESTS_PER_HOUR = 1000;
            var rateLimit = new RateLimit(true);
            rateLimit.AddApiCall();
            rateLimit.AddApiCall();

            // act
            var result = await rateLimit.Delay(REQUESTS_PER_HOUR);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Delay_TurnedOff_NoDelay()
        {
            // arrange
            const int REQUESTS_PER_HOUR = 10;
            var rateLimit = new RateLimit(false);

            // act
            var result = await rateLimit.Delay(REQUESTS_PER_HOUR);

            Assert.Equal(0, result);
        }

        [Fact]
        public async Task Delay_UnderLimit_NoDelay()
        {
            // arrange
            const int REQUESTS_PER_HOUR = 2000;
            var rateLimit = new RateLimit(true);
            rateLimit.AddApiCall();

            // act
            var result = await rateLimit.Delay(REQUESTS_PER_HOUR);

            Assert.Equal(0, result);
        }

        [Fact]
        public async Task Delay_ZeroRequestsPerHour_NoDelay()
        {
            // arrange
            const int REQUESTS_PER_HOUR = 0;
            var rateLimit = new RateLimit(true);

            // act
            var result = await rateLimit.Delay(REQUESTS_PER_HOUR);

            Assert.Equal(0, result);
        }
    }
}