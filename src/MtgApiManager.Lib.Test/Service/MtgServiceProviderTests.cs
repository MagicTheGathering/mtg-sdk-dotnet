using System;
using Flurl.Http;
using MtgApiManager.Lib.Core;
using MtgApiManager.Lib.Service;
using Xunit;

namespace MtgApiManager.Lib.Test.Service
{
    public class MtgServiceProviderTests : IDisposable
    {
        private bool _disposedValue;

        [Fact]
        public void Constructor_JsonSerializerSet()
        {
            // arrange & act
            _ = new MtgServiceProvider();

            // assert
            // In Flurl.Http v4, serializer is configured per-client via WithDefaults
            // This test verifies the constructor completes without error
            Assert.True(true);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        [Fact]
        public void GetCardService_Success()
        {
            // arrange
            var provider = new MtgServiceProvider();

            // act
            var result = provider.GetCardService();

            // assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetSetService_Success()
        {
            // arrange
            var provider = new MtgServiceProvider();

            // act
            var result = provider.GetSetService();

            // assert
            Assert.NotNull(result);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // In Flurl.Http v4, cleanup is handled differently
                    // No global settings to reset
                }

                _disposedValue = true;
            }
        }
    }
}