using System.Linq;
using MtgApiManager.Lib.Core;
using Xunit;

namespace MtgApiManager.Lib.Test.Core
{
    public class EnumerationTests
    {
        [Fact]
        public void CompareTo_Equal_ReturnsZero()
        {
            // arrange
            var otherEnum = ApiEndPoint.Cards;

            // act
            var results = ApiEndPoint.Cards.CompareTo(otherEnum);

            // assert
            Assert.Equal(0, results);
        }

        [Fact]
        public void CompareTo_NotEqual_ReturnsOne()
        {
            // arrange
            var otherEnum = ApiEndPoint.Cards;

            // act
            var results = ApiEndPoint.Sets.CompareTo(otherEnum);

            // assert
            Assert.Equal(1, results);
        }

        [Fact]
        public void Equals_DifferentEnumeration_False()
        {
            // arrange
            var otherEnum = ApiEndPoint.Sets;

            // act
            var results = ApiEndPoint.Cards.Equals(otherEnum);

            // assert
            Assert.False(results);
        }

        [Fact]
        public void Equals_NotEnumeration_False()
        {
            // arrange
            var otherObj = new object();

            // act
            var results = ApiEndPoint.Cards.Equals(otherObj);

            // assert
            Assert.False(results);
        }

        [Fact]
        public void Equals_SameEnumeration_True()
        {
            // arrange
            var otherEnum = ApiEndPoint.Cards;

            // act
            var results = ApiEndPoint.Cards.Equals(otherEnum);

            // assert
            Assert.True(results);
        }

        [Fact]
        public void GetAll_GetsAllEnumerations()
        {
            // arrange
            // act
            var results = Enumeration.GetAll<ApiEndPoint>();

            // assert
            Assert.Equal(7, results.Count());
        }

        [Fact]
        public void GetHashCode_Equal_True()
        {
            // arrange
            var otherHashCode = ApiEndPoint.Cards.GetHashCode();

            // act
            var results = ApiEndPoint.Cards.GetHashCode();

            // assert
            Assert.Equal(otherHashCode, results);
        }

        [Fact]
        public void GetHashCode_NotEqual_False()
        {
            // arrange
            var otherHashCode = ApiEndPoint.Sets.GetHashCode();

            // act
            var results = ApiEndPoint.Cards.GetHashCode();

            // assert
            Assert.NotEqual(otherHashCode, results);
        }

        [Fact]
        public void ToString_EqualName()
        {
            // arrange
            // act
            var results = ApiEndPoint.Cards.ToString();

            // assert
            Assert.Equal(ApiEndPoint.Cards.Name, results);
        }
    }
}