// <copyright file="SetServiceTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Service
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Threading.Tasks;
    using Lib.Core;
    using Lib.Core.Exceptions;
    using Lib.Dto;
    using Lib.Dto.Set;
    using Lib.Model;
    using Lib.Service;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    /// <summary>
    /// Tests the <see cref="SetService"/> class.
    /// </summary>
    [TestClass]
    public class SetServiceTest
    {
        /// <summary>
        /// Tests the <see cref="SetService.AllAsync"/> method.
        /// </summary>
        /// <returns>The asynchronous task.</returns>
        [TestMethod]
        public async Task AllAsyncTest()
        {
            var sets = new List<SetDto>()
            {
                new SetDto()
                {
                    Block = "block1",
                    Booster = new List<List<string>>(),
                    Border = "border1",
                    Code = "code1",
                    Expansion = "expansion1",
                    GathererCode = "gathererCode1",
                    MagicCardsInfoCode = "magicCardsInfoCode1",
                    Name = "name1",
                    OldCode = "oldCode1",
                    OnlineOnly = true,
                    ReleaseDate = new DateTime(2016, 1, 1)
                },
                new SetDto()
                {
                    Block = "block2",
                    Booster = new List<List<string>>(),
                    Border = "border2",
                    Code = "code2",
                    Expansion = "expansion2",
                    GathererCode = "gathererCode2",
                    MagicCardsInfoCode = "magicCardsInfoCode2",
                    Name = "name2",
                    OldCode = "oldCode2",
                    OnlineOnly = true,
                    ReleaseDate = new DateTime(2016, 2, 2)
                }
            };

            // Test the All method.
            var moqAdapter = new Mock<IMtgApiServiceAdapter>();
            moqAdapter
                .SetupSequence(x => x.WebGetAsync<RootSetListDto>(It.IsAny<Uri>()))
                .Throws<ArgumentNullException>()
                .Throws(new MtgApiException<BadRequestException>("bad request"))
                .Throws(new MtgApiException<ForbiddenException>("forbidden"))
                .Throws(new MtgApiException<InternalServerErrorException>("server error"))
                .Throws(new MtgApiException<NotFoundException>("not found"))
                .Throws(new MtgApiException<ServiceUnavailableException>("unavailable"))
                .Throws<Exception>()
                .ReturnsAsync(new RootSetListDto() { Sets = sets });

            var service = new SetService(moqAdapter.Object);
            service = service.Where(x => x.Name, "name1");

            var result = await service.AllAsync();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Value cannot be null.", result.Exception.Message);

            result = await service.AllAsync();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("bad request", result.Exception.Message);

            result = await service.AllAsync();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("forbidden", result.Exception.Message);

            result = await service.AllAsync();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("server error", result.Exception.Message);

            result = await service.AllAsync();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("not found", result.Exception.Message);

            result = await service.AllAsync();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("unavailable", result.Exception.Message);

            result = await service.AllAsync();
            Assert.IsFalse(result.IsSuccess);
            Assert.IsInstanceOfType(result.Exception, typeof(Exception));

            result = await service.AllAsync();
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNull(result.Exception);
            Assert.IsNotNull(result.Value);
        }

        /// <summary>
        /// Tests the <see cref="CardService.All"/> method.
        /// </summary>
        [TestMethod]
        public void AllTest()
        {
            var sets = new List<SetDto>()
            {
                new SetDto()
                {
                    Block = "block1",
                    Booster = new List<List<string>>(),
                    Border = "border1",
                    Code = "code1",
                    Expansion = "expansion1",
                    GathererCode = "gathererCode1",
                    MagicCardsInfoCode = "magicCardsInfoCode1",
                    Name = "name1",
                    OldCode = "oldCode1",
                    OnlineOnly = true,
                    ReleaseDate = new DateTime(2016, 1, 1)
                },
                new SetDto()
                {
                    Block = "block2",
                    Booster = new List<List<string>>(),
                    Border = "border2",
                    Code = "code2",
                    Expansion = "expansion2",
                    GathererCode = "gathererCode2",
                    MagicCardsInfoCode = "magicCardsInfoCode2",
                    Name = "name2",
                    OldCode = "oldCode2",
                    OnlineOnly = true,
                    ReleaseDate = new DateTime(2016, 2, 2)
                }
            };

            // Test the All method.
            var moqAdapter = new Mock<IMtgApiServiceAdapter>();
            moqAdapter
                .SetupSequence(x => x.WebGetAsync<RootSetListDto>(It.IsAny<Uri>()))
                .Throws<ArgumentNullException>()
                .Throws(new MtgApiException<BadRequestException>("bad request"))
                .Throws(new MtgApiException<ForbiddenException>("forbidden"))
                .Throws(new MtgApiException<InternalServerErrorException>("server error"))
                .Throws(new MtgApiException<NotFoundException>("not found"))
                .Throws(new MtgApiException<ServiceUnavailableException>("unavailable"))
                .Throws<Exception>()
                .ReturnsAsync(new RootSetListDto() { Sets = sets });

            var service = new SetService(moqAdapter.Object);
            service = service.Where(x => x.Name, "name1");

            var result = service.All();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Value cannot be null.", result.Exception.Message);

            result = service.All();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("bad request", result.Exception.Message);

            result = service.All();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("forbidden", result.Exception.Message);

            result = service.All();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("server error", result.Exception.Message);

            result = service.All();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("not found", result.Exception.Message);

            result = service.All();
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("unavailable", result.Exception.Message);

            result = service.All();
            Assert.IsFalse(result.IsSuccess);
            Assert.IsInstanceOfType(result.Exception, typeof(Exception));

            result = service.All();
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNull(result.Exception);
            Assert.IsNotNull(result.Value);
        }

        /// <summary>
        /// Tests the constructors in the <see cref="SetService"/> class.
        /// </summary>
        [TestMethod]
        public void ContructorTest()
        {
            SetService service;
            PrivateObject privateObject;

            service = new SetService();
            privateObject = new PrivateObject(service, new PrivateType(typeof(ServiceBase<SetService, Set>)));
            Assert.IsInstanceOfType(privateObject.GetFieldOrProperty("Adapter"), typeof(MtgApiServiceAdapter));
            Assert.AreEqual(ApiVersion.V1_0, privateObject.GetFieldOrProperty("Version"));
            Assert.AreEqual(ApiEndPoint.Sets, privateObject.GetFieldOrProperty("EndPoint"));

            service = new SetService(new MtgApiServiceAdapter());
            privateObject = new PrivateObject(service, new PrivateType(typeof(ServiceBase<SetService, Set>)));
            Assert.IsInstanceOfType(privateObject.GetFieldOrProperty("Adapter"), typeof(MtgApiServiceAdapter));
            Assert.AreEqual(ApiVersion.V1_0, privateObject.GetFieldOrProperty("Version"));
            Assert.AreEqual(ApiEndPoint.Sets, privateObject.GetFieldOrProperty("EndPoint"));

            service = new SetService(new MtgApiServiceAdapter(), ApiVersion.V1_0);
            privateObject = new PrivateObject(service, new PrivateType(typeof(ServiceBase<SetService, Set>)));
            Assert.IsInstanceOfType(privateObject.GetFieldOrProperty("Adapter"), typeof(MtgApiServiceAdapter));
            Assert.AreEqual(ApiVersion.V1_0, privateObject.GetFieldOrProperty("Version"));
            Assert.AreEqual(ApiEndPoint.Sets, privateObject.GetFieldOrProperty("EndPoint"));
        }

        /// <summary>
        /// Tests the <see cref="SetService.FindAsync(int)"/> methods.
        /// </summary>
        /// <returns>The asynchronous task.</returns>
        [TestMethod]
        public async Task FindAsyncTest()
        {
            var setDto = new SetDto()
            {
                Block = "block1",
                Booster = new List<List<string>>(),
                Border = "border1",
                Code = "code1",
                Expansion = "expansion1",
                GathererCode = "gathererCode1",
                MagicCardsInfoCode = "magicCardsInfoCode1",
                Name = "name1",
                OldCode = "oldCode1",
                OnlineOnly = true,
                ReleaseDate = new DateTime(2016, 1, 1)
            };

            // Test the FindAsync method.
            var moqAdapter = new Mock<IMtgApiServiceAdapter>();
            moqAdapter
                .SetupSequence(x => x.WebGetAsync<RootSetDto>(It.IsAny<Uri>()))
                .Throws<ArgumentNullException>()
                .Throws(new MtgApiException<BadRequestException>("bad request"))
                .Throws(new MtgApiException<ForbiddenException>("forbidden"))
                .Throws(new MtgApiException<InternalServerErrorException>("server error"))
                .Throws(new MtgApiException<NotFoundException>("not found"))
                .Throws(new MtgApiException<ServiceUnavailableException>("unavailable"))
                .Throws<Exception>()
                .ReturnsAsync(new RootSetDto() { Set = setDto });

            var service = new SetService(moqAdapter.Object);

            var result = await service.FindAsync("code1");
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Value cannot be null.", result.Exception.Message);

            result = await service.FindAsync("code1");
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("bad request", result.Exception.Message);

            result = await service.FindAsync("code1");
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("forbidden", result.Exception.Message);

            result = await service.FindAsync("code1");
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("server error", result.Exception.Message);

            result = await service.FindAsync("code1");
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("not found", result.Exception.Message);

            result = await service.FindAsync("code1");
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("unavailable", result.Exception.Message);

            result = await service.FindAsync("code1");
            Assert.IsFalse(result.IsSuccess);
            Assert.IsInstanceOfType(result.Exception, typeof(Exception));

            result = await service.FindAsync("code1");
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNull(result.Exception);
            Assert.IsNotNull(result.Value);
        }

        /// <summary>
        /// Tests the <see cref="SetService.Find(int)"/> method.
        /// </summary>
        [TestMethod]
        public void FindTest()
        {
            var setDto = new SetDto()
            {
                Block = "block1",
                Booster = new List<List<string>>(),
                Border = "border1",
                Code = "code1",
                Expansion = "expansion1",
                GathererCode = "gathererCode1",
                MagicCardsInfoCode = "magicCardsInfoCode1",
                Name = "name1",
                OldCode = "oldCode1",
                OnlineOnly = true,
                ReleaseDate = new DateTime(2016, 1, 1)
            };

            // Test the FindAsync method.
            var moqAdapter = new Mock<IMtgApiServiceAdapter>();
            moqAdapter
                .SetupSequence(x => x.WebGetAsync<RootSetDto>(It.IsAny<Uri>()))
                .Throws<ArgumentNullException>()
                .Throws(new MtgApiException<BadRequestException>("bad request"))
                .Throws(new MtgApiException<ForbiddenException>("forbidden"))
                .Throws(new MtgApiException<InternalServerErrorException>("server error"))
                .Throws(new MtgApiException<NotFoundException>("not found"))
                .Throws(new MtgApiException<ServiceUnavailableException>("unavailable"))
                .Throws<Exception>()
                .ReturnsAsync(new RootSetDto() { Set = setDto });

            var service = new SetService(moqAdapter.Object);

            var result = service.Find("code1");
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Value cannot be null.", result.Exception.Message);

            result = service.Find("code1");
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("bad request", result.Exception.Message);

            result = service.Find("code1");
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("forbidden", result.Exception.Message);

            result = service.Find("code1");
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("server error", result.Exception.Message);

            result = service.Find("code1");
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("not found", result.Exception.Message);

            result = service.Find("code1");
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("unavailable", result.Exception.Message);

            result = service.Find("code1");
            Assert.IsFalse(result.IsSuccess);
            Assert.IsInstanceOfType(result.Exception, typeof(Exception));

            result = service.Find("code1");
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNull(result.Exception);
            Assert.IsNotNull(result.Value);
        }

        /// <summary>
        /// Tests the MapSetsList method.
        /// </summary>
        [TestMethod]
        public void MapSetsListTest()
        {
            SetService setService = new SetService();
            PrivateObject privateObject = new PrivateObject(setService);

            try
            {
                // Test sending a null parameter.
                privateObject.Invoke("MapSetsList", new object[] { null });
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("setListDto", ex.ParamName);
            }
            catch
            {
                Assert.Fail();
            }

            // Test a null sets collection inside the RootSetsListDto object.
            var setListDto = new RootSetListDto();
            Assert.IsNull(privateObject.Invoke("MapSetsList", new object[] { setListDto }));

            setListDto = new RootSetListDto()
            {
                Sets = new List<SetDto>()
                {
                    new SetDto()
                    {
                        Block = "block1",
                        Booster = new List<List<string>>(),
                        Border = "border1",
                        Code = "code1",
                        Expansion = "expansion1",
                        GathererCode = "gathererCode1",
                        MagicCardsInfoCode = "magicCardsInfoCode1",
                        Name = "name1",
                        OldCode = "oldCode1",
                        OnlineOnly = true,
                        ReleaseDate = new DateTime(2016, 1, 1)
                    },
                    new SetDto()
                    {
                        Block = "block2",
                        Booster = new List<List<string>>(),
                        Border = "border2",
                        Code = "code2",
                        Expansion = "expansion2",
                        GathererCode = "gathererCode2",
                        MagicCardsInfoCode = "magicCardsInfoCode2",
                        Name = "name2",
                        OldCode = "oldCode2",
                        OnlineOnly = true,
                        ReleaseDate = new DateTime(2016, 2, 2)
                    }
                }
            };

            var result = privateObject.Invoke("MapSetsList", new object[] { setListDto }) as List<Set>;
            Assert.AreEqual(2, result.Count);
        }

        /// <summary>
        /// Tests the <see cref="SetService.Where{U}(System.Linq.Expressions.Expression{Func{SetQueryParameter, U}}, string)"/>> method.
        /// </summary>
        [TestMethod]
        public void WhereTest()
        {
            SetService service = new SetService();

            try
            {
                // Test sending a null parameter.
                service.Where<int>(null, null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("property", ex.ParamName);
            }
            catch
            {
                Assert.Fail();
            }

            try
            {
                // Test sending a null parameter.
                service.Where(x => x.Name, null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("value", ex.ParamName);
            }
            catch
            {
                Assert.Fail();
            }

            service = service.Where(x => x.Name, "test");

            PrivateObject privateObject = new PrivateObject(service);
            var whereQuery = privateObject.GetFieldOrProperty("_whereQueries") as NameValueCollection;
            Assert.AreEqual(1, whereQuery.Count);
            Assert.AreEqual("name", whereQuery.AllKeys[0]);
            Assert.AreEqual("test", whereQuery["name"]);
        }
    }
}