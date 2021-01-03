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
            // arrange
            _ = new MtgServiceProvider();

            // act
            var result = FlurlHttp.GlobalSettings.JsonSerializer;

            // assert
            Assert.IsType<SystemTextJsonSerializer>(result);
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
                    FlurlHttp.GlobalSettings.ResetDefaults();
                }

                _disposedValue = true;
            }
        }
    }
}