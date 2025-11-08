using System.Collections;
using System.Collections.Generic;

using MtgApiManager.Lib.Core;
using Xunit;

namespace MtgApiManager.Lib.Test.Core
{
    public class ApiEndPointIdTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { ApiEndPoint.Cards.Id, 1 };
            yield return new object[] { ApiEndPoint.Formats.Id, 6 };
            yield return new object[] { ApiEndPoint.None.Id, 0 };
            yield return new object[] { ApiEndPoint.Sets.Id, 2 };
            yield return new object[] { ApiEndPoint.SubTypes.Id, 5 };
            yield return new object[] { ApiEndPoint.SuperTypes.Id, 4 };
            yield return new object[] { ApiEndPoint.Types.Id, 3 };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class ApiEndPointNameTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { ApiEndPoint.Cards.Name, nameof(ApiEndPoint.Cards).ToLower() };
            yield return new object[] { ApiEndPoint.Formats.Name, nameof(ApiEndPoint.Formats).ToLower() };
            yield return new object[] { ApiEndPoint.None.Name, nameof(ApiEndPoint.None).ToLower() };
            yield return new object[] { ApiEndPoint.Sets.Name, nameof(ApiEndPoint.Sets).ToLower() };
            yield return new object[] { ApiEndPoint.SubTypes.Name, nameof(ApiEndPoint.SubTypes).ToLower() };
            yield return new object[] { ApiEndPoint.SuperTypes.Name, nameof(ApiEndPoint.SuperTypes).ToLower() };
            yield return new object[] { ApiEndPoint.Types.Name, nameof(ApiEndPoint.Types).ToLower() };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class ApiEndPointsTests
    {
        [Theory]
        [ClassData(typeof(ApiEndPointIdTestData))]
        public void Id_Correct(int id, int expectedValue) => Assert.Equal(expectedValue, id);

        [Theory]
        [ClassData(typeof(ApiEndPointNameTestData))]
        public void Name_Correct(string name, string expectedValue) => Assert.Equal(expectedValue, name);
    }
}