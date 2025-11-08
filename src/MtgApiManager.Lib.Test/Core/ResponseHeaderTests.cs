using System.Collections;
using System.Collections.Generic;

using MtgApiManager.Lib.Core;
using Xunit;

namespace MtgApiManager.Lib.Test.Core
{
    public class ResponseHeaderIdTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { ResponseHeader.Count.Id, 0 };
            yield return new object[] { ResponseHeader.Link.Id, 1 };
            yield return new object[] { ResponseHeader.PageSize.Id, 2 };
            yield return new object[] { ResponseHeader.RatelimitLimit.Id, 3 };
            yield return new object[] { ResponseHeader.RatelimitRemaining.Id, 4 };
            yield return new object[] { ResponseHeader.TotalCount.Id, 5 };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class ResponseHeaderNameTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { ResponseHeader.Count.Name, "Count" };
            yield return new object[] { ResponseHeader.Link.Name, "Link" };
            yield return new object[] { ResponseHeader.PageSize.Name, "Page-Size" };
            yield return new object[] { ResponseHeader.RatelimitLimit.Name, "Ratelimit-Limit" };
            yield return new object[] { ResponseHeader.RatelimitRemaining.Name, "Ratelimit-Remaining" };
            yield return new object[] { ResponseHeader.TotalCount.Name, "Total-Count" };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class ResponseHeaderTests
    {
        [Theory]
        [ClassData(typeof(ResponseHeaderIdTestData))]
        public void Id_Correct(int id, int expectedValue) => Assert.Equal(expectedValue, id);

        [Theory]
        [ClassData(typeof(ResponseHeaderNameTestData))]
        public void Name_Correct(string name, string expectedValue) => Assert.Equal(expectedValue, name);
    }
}