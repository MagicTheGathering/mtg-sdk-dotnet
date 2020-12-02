using System.Collections;
using System.Collections.Generic;

using MtgApiManager.Lib.Core;
using Xunit;

namespace MtgApiManager.Lib.Test.Core
{
    public class ApiVersionTests
    {
        [Theory]
        [ClassData(typeof(ApiVersionIdTestData))]
        public void Id_Correct(int id, int expectedValue)
        {
            Assert.Equal(expectedValue, id);
        }

        [Theory]
        [ClassData(typeof(ApiVersionNameTestData))]
        public void Name_Correct(string name, string expectedValue)
        {
            Assert.Equal(expectedValue, name);
        }
    }

    public class ApiVersionIdTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { ApiVersion.None.Id, 0 };
            yield return new object[] { ApiVersion.V1.Id, 1 };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class ApiVersionNameTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { ApiVersion.None.Name, nameof(ApiVersion.None).ToLower() };
            yield return new object[] { ApiVersion.V1.Name, nameof(ApiVersion.V1).ToLower() };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}