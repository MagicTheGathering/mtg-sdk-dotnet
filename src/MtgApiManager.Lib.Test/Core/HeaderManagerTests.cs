using System;
using MtgApiManager.Lib.Core;
using Xunit;
using Flurl.Util;

namespace MtgApiManager.Lib.Test.Core
{
    public class HeaderManagerTests
    {
        [Fact]
        public void Get_CacheNull_ReturnsDefault()
        {
            // arrange
            var manager = new HeaderManager();

            // act
            var result = manager.Get<string>(ResponseHeader.PageSize);

            // assert
            Assert.Null(result);
        }

        [Fact]
        public void Get_FailsConvertion_ReturnsDefault()
        {
            // arrange
            var items = new []
            {
                (ResponseHeader.PageSize.Name, "Not an integer"),
                (ResponseHeader.Count.Name, "100"),
            };

            var headers = new NameValueList<string>(items, false);

            var manager = new HeaderManager();
            manager.Update(headers);

            // act
            var result = manager.Get<int>(ResponseHeader.PageSize);

            // assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Get_KeyDoesNotExist_ReturnsDefault()
        {
            // arrange
            var items = new []
            {
                ("key1", "value1"),
                ("key2", "value2"),
            };

            var headers = new NameValueList<string>(items, false);

            var manager = new HeaderManager();
            manager.Update(headers);

            // act
            var result = manager.Get<string>(ResponseHeader.PageSize);

            // assert
            Assert.Null(result);
        }

        [Fact]
        public void Get_KeyExists_Success()
        {
            // arrange
            const int PAGE_SIZE = 50;
            var items = new []
            {
                (ResponseHeader.PageSize.Name, PAGE_SIZE.ToString()),
                (ResponseHeader.Count.Name, "100"),
            };

            var headers = new NameValueList<string>(items, false);

            var manager = new HeaderManager();
            manager.Update(headers);

            // act
            var result = manager.Get<int>(ResponseHeader.PageSize);

            // assert
            Assert.Equal(PAGE_SIZE, result);
        }

        [Fact]
        public void Update_CacheUpdated_Success()
        {
            // arrange
            var items = new []
            {
                (ResponseHeader.PageSize.Name, "50"),
                (ResponseHeader.Count.Name, "100"),
            };

            var headers = new NameValueList<string>(items, false);

            var manager = new HeaderManager();

            // act
            manager.Update(headers);

            // assert
            Assert.NotEmpty(manager.Get<string>(ResponseHeader.PageSize));
        }

        [Fact]
        public void Update_HeadersNull_Throws()
        {
            // arrange
            var manager = new HeaderManager();

            // act
            // assert
            Assert.Throws<ArgumentNullException>(() => manager.Update(null));
        }
    }
}