using MtgApiManager.Lib.Model;
using Xunit;

namespace MtgApiManager.Lib.Test.Model
{
    public class CardTests
    {
        [Fact]
        public void IsColorless_ColorsEqualOne_False()
        {
            // arrange
            var card = new Card
            {
                Colors = new[] { "color1" }
            };

            // act
            var result = card.IsColorless;

            // assert
            Assert.False(result);
        }

        [Fact]
        public void IsColorless_ColorsGreaterThanOne_False()
        {
            // arrange
            var card = new Card
            {
                Colors = new[] { "color1", "color2" }
            };

            // act
            var result = card.IsColorless;

            // assert
            Assert.False(result);
        }

        [Fact]
        public void IsColorless_ColorsNull_False()
        {
            // arrange
            var card = new Card();

            // act
            var result = card.IsColorless;

            // assert
            Assert.True(result);
        }

        [Fact]
        public void IsMultiColor_ColorsEqualOne_False()
        {
            // arrange
            var card = new Card
            {
                Colors = new[] { "color1" }
            };

            // act
            var result = card.IsMultiColor;

            // assert
            Assert.False(result);
        }

        [Fact]
        public void IsMultiColor_ColorsGreaterThanOne_True()
        {
            // arrange
            var card = new Card
            {
                Colors = new[] { "color1", "color2" }
            };

            // act
            var result = card.IsMultiColor;

            // assert
            Assert.True(result);
        }

        [Fact]
        public void IsMultiColor_ColorsNull_False()
        {
            // arrange
            var card = new Card();

            // act
            var result = card.IsMultiColor;

            // assert
            Assert.False(result);
        }
    }
}