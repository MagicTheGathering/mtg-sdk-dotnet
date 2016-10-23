// <copyright file="DtoTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Dto
{
    using System.Linq;
    using System.Reflection;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;

    /// <summary>
    /// Tests the data transfer objects.
    /// </summary>
    [TestClass]
    public class DtoTest
    {
        /// <summary>
        /// Tests that the DTO objects contain a <see cref="JsonPropertyAttribute"/> attribute.
        /// </summary>
        [TestMethod]
        public void AttributeTest()
        {
            var dtoTypes = from type in Assembly.LoadFrom("MtgApiManager.Lib.dll").GetTypes()
                           where type.Namespace == "MtgApiManager.Lib.Dto" && !type.IsAbstract && type.IsAssignableFrom(typeof(JsonConverter))
                           select type;

            foreach (var type in dtoTypes)
            {
                var properties = type.GetProperties();

                Assert.IsTrue(properties.All(x => x.GetCustomAttributes(typeof(JsonPropertyAttribute), true).Any()));
            }
        }
    }
}