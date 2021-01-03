using System;
using System.Collections.Generic;
using System.Linq;
using AutoBogus;
using MtgApiManager.Lib.Dto;
using MtgApiManager.Lib.Model;
using Xunit;

namespace MtgApiManager.Lib.Test.Model
{
    public class ModelMapperTests
    {
        private readonly IAutoFaker _faker;

        public ModelMapperTests()
        {
            _faker = AutoFaker.Create();
        }

        [Fact]
        public void MapCard_DtoNotNull_Success()
        {
            // arrange
            var mapper = new ModelMapper();
            var dto = _faker.Generate<CardDto>();

            // act
            var model = mapper.MapCard(dto);

            // assert
            foreach (var (Name, Value, PropType) in GetAllPropertyNames(dto))
            {
                var modelValue = GetPropValue(model, Name);
                if (PropType == modelValue.Type)
                {
                    Assert.Equal(Value, modelValue.Value);
                }
            }
        }

        [Fact]
        public void MapCard_DtoNull_Throws()
        {
            // arrange
            var mapper = new ModelMapper();

            // act
            // assert
            Assert.Throws<ArgumentNullException>(() => mapper.MapCard(null));
        }

        [Fact]
        public void MapForeignName_DtoNotNull_Success()
        {
            // arrange
            var mapper = new ModelMapper();
            var dto = _faker.Generate<ForeignNameDto>();

            // act
            var model = mapper.MapForeignName(dto);

            // assert
            foreach (var (Name, Value, PropType) in GetAllPropertyNames(dto))
            {
                var modelValue = GetPropValue(model, Name);
                if (PropType == modelValue.Type)
                {
                    Assert.Equal(Value, modelValue.Value);
                }
            }
        }

        [Fact]
        public void MapForeignName_DtoNull_Throws()
        {
            // arrange
            var mapper = new ModelMapper();

            // act
            // assert
            Assert.Throws<ArgumentNullException>(() => mapper.MapForeignName(null));
        }

        [Fact]
        public void MapLegality_DtoNotNull_Success()
        {
            // arrange
            var mapper = new ModelMapper();
            var dto = _faker.Generate<LegalityDto>();

            // act
            var model = mapper.MapLegality(dto);

            // assert
            foreach (var (Name, Value, PropType) in GetAllPropertyNames(dto))
            {
                var modelValue = GetPropValue(model, Name);
                if (PropType == modelValue.Type)
                {
                    Assert.Equal(Value, modelValue.Value);
                }
            }
        }

        [Fact]
        public void MapLegality_DtoNull_Throws()
        {
            // arrange
            var mapper = new ModelMapper();

            // act
            // assert
            Assert.Throws<ArgumentNullException>(() => mapper.MapLegality(null));
        }

        [Fact]
        public void MapRuling_DtoNotNull_Success()
        {
            // arrange
            var mapper = new ModelMapper();
            var dto = _faker.Generate<RulingDto>();

            // act
            var model = mapper.MapRuling(dto);

            // assert
            foreach (var (Name, Value, PropType) in GetAllPropertyNames(dto))
            {
                var modelValue = GetPropValue(model, Name);
                if (PropType == modelValue.Type)
                {
                    Assert.Equal(Value, modelValue.Value);
                }
            }
        }

        [Fact]
        public void MapRuling_DtoNull_Throws()
        {
            // arrange
            var mapper = new ModelMapper();

            // act
            // assert
            Assert.Throws<ArgumentNullException>(() => mapper.MapRuling(null));
        }

        [Fact]
        public void MapSet_DtoNotNull_Success()
        {
            // arrange
            var mapper = new ModelMapper();
            var dto = _faker.Generate<SetDto>();

            // act
            var model = mapper.MapSet(dto);

            // assert
            foreach (var (Name, Value, PropType) in GetAllPropertyNames(dto))
            {
                var modelValue = GetPropValue(model, Name);
                if (PropType == modelValue.Type)
                {
                    Assert.Equal(Value, modelValue.Value);
                }
            }
        }

        [Fact]
        public void MapSet_DtoNull_Throws()
        {
            // arrange
            var mapper = new ModelMapper();

            // act
            // assert
            Assert.Throws<ArgumentNullException>(() => mapper.MapSet(null));
        }

        private static IEnumerable<(string Name, object Value, Type type)> GetAllPropertyNames(object obj) =>
            obj.GetType()
            .GetProperties()
            .Select(p => (p.Name, p.GetValue(obj), p.PropertyType));

        private static (object Value, Type Type) GetPropValue(object obj, string name)
        {
            var propInfo = obj.GetType().GetProperty(name);
            return (propInfo.GetValue(obj), propInfo.PropertyType);
        }
    }
}