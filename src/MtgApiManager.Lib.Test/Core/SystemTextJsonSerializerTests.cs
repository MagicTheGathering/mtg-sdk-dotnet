using System;
using System.IO;
using MtgApiManager.Lib.Core;
using MtgApiManager.Lib.Dto;
using Xunit;

namespace MtgApiManager.Lib.Test.Core
{
    public class SystemTextJsonSerializerTests
    {
        [Fact]
        public void Deserialize_Stream_Success()
        {
            // arrange
            using var stream = File.OpenRead(@"Data\card1.json");

            var serializer = new SystemTextJsonSerializer();

            // act
            var result = serializer.Deserialize<RootCardDto>(stream);

            // assert
            Assert.Equal("Abundance", result.Card.Name);
        }

        [Fact]
        public void Deserialize_String_Success()
        {
            // arrange
            var jsonString = File.ReadAllText(@"Data\card1.json");

            var serializer = new SystemTextJsonSerializer();

            // act
            var result = serializer.Deserialize<RootCardDto>(jsonString);

            // assert
            Assert.Equal("Abundance", result.Card.Name);
        }

        [Fact]
        public void Serialize_NotImplemented()
        {
            // arrange
            var serializer = new SystemTextJsonSerializer();

            var obj = new
            {
                Prop1 = "Value1",
                Prop2 = "Value2",
                Prop3 = "Value3",
            };

            // act
            var result = serializer.Serialize(obj);

            // assert
            Assert.NotEmpty(result);
            Assert.Contains("Value", result);
        }
    }
}