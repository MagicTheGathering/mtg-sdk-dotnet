using System.Collections;
using System.Collections.Generic;

using MtgApiManager.Lib.Core;
using Xunit;

namespace MtgApiManager.Lib.Test.Core
{
    public class MtgApiErrorDescriptionTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { MtgApiError.BadRequest.Description, "Your request is not valid" };
            yield return new object[] { MtgApiError.Forbidden.Description, "You have exceeded the rate limit" };
            yield return new object[] { MtgApiError.InternalServerError.Description, "We had a problem with our server. Try again later" };
            yield return new object[] { MtgApiError.None.Description, string.Empty };
            yield return new object[] { MtgApiError.NotFound.Description, "The specified resource could not be found" };
            yield return new object[] { MtgApiError.ServiceUnavailable.Description, "We’re temporarily off line for maintenance. Please try again later." };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class MtgApiErrorIdTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { MtgApiError.BadRequest.Id, 400 };
            yield return new object[] { MtgApiError.Forbidden.Id, 403 };
            yield return new object[] { MtgApiError.InternalServerError.Id, 500 };
            yield return new object[] { MtgApiError.None.Id, 0 };
            yield return new object[] { MtgApiError.NotFound.Id, 404 };
            yield return new object[] { MtgApiError.ServiceUnavailable.Id, 503 };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class MtgApiErrorNameTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { MtgApiError.BadRequest.Name, nameof(MtgApiError.BadRequest) };
            yield return new object[] { MtgApiError.Forbidden.Name, nameof(MtgApiError.Forbidden) };
            yield return new object[] { MtgApiError.InternalServerError.Name, nameof(MtgApiError.InternalServerError) };
            yield return new object[] { MtgApiError.None.Name, nameof(MtgApiError.None) };
            yield return new object[] { MtgApiError.NotFound.Name, nameof(MtgApiError.NotFound) };
            yield return new object[] { MtgApiError.ServiceUnavailable.Name, nameof(MtgApiError.ServiceUnavailable) };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class MtgApiErrorTests
    {
        [Theory]
        [ClassData(typeof(MtgApiErrorIdTestData))]
        public void Description_Correct(int description, int expectedValue) => Assert.Equal(expectedValue, description);

        [Theory]
        [ClassData(typeof(MtgApiErrorIdTestData))]
        public void Id_Correct(int id, int expectedValue) => Assert.Equal(expectedValue, id);

        [Theory]
        [ClassData(typeof(MtgApiErrorNameTestData))]
        public void Name_Correct(string name, string expectedValue) => Assert.Equal(expectedValue, name);
    }
}