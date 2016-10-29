// <copyright file="CardTypeServiceTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Service
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Lib.Core;
    using Lib.Core.Exceptions;
    using Lib.Dto;
    using Lib.Service;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    /// <summary>
    /// Tests the <see cref="CardTypeService"/> class.
    /// </summary>
    [TestClass]
    public class CardTypeServiceTest
    {
        /// <summary>
        /// Tests the <see cref="CardTypeService.AllAsync"/> method.
        /// </summary>
        /// <returns>The asynchronous task.</returns>
        [TestMethod]
        public async Task AllAsyncTest()
        {
            var cardTypes = new List<string>
            {
                "type1",
                "type2",
                "type3",
                "type4",
                "type5",
            };

            // Test the All method.
            var moqAdapter = new Mock<IMtgApiServiceAdapter>();
            moqAdapter
                .SetupSequence(x => x.WebGetAsync<RootCardTypeDto>(new Uri("https://api.magicthegathering.io/v1/types")))
                .Throws<ArgumentNullException>()
                .Throws(new MtgApiException<BadRequestException>("bad request"))
                .Throws(new MtgApiException<ForbiddenException>("forbidden"))
                .Throws(new MtgApiException<InternalServerErrorException>("server error"))
                .Throws(new MtgApiException<NotFoundException>("not found"))
                .Throws(new MtgApiException<ServiceUnavailableException>("unavailable"))
                .Throws<Exception>()
                .ReturnsAsync(new RootCardTypeDto() { Types = cardTypes });

            var service = new CardTypeService(moqAdapter.Object);

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
        /// Tests the <see cref="CardTypeService.All"/> method.
        /// </summary>
        [TestMethod]
        public void AllTest()
        {
            var cardTypes = new List<string>
            {
                "type1",
                "type2",
                "type3",
                "type4",
                "type5",
            };

            // Test the All method.
            var moqAdapter = new Mock<IMtgApiServiceAdapter>();
            moqAdapter
                .SetupSequence(x => x.WebGetAsync<RootCardTypeDto>(new Uri("https://api.magicthegathering.io/v1/types")))
                .Throws<ArgumentNullException>()
                .Throws(new MtgApiException<BadRequestException>("bad request"))
                .Throws(new MtgApiException<ForbiddenException>("forbidden"))
                .Throws(new MtgApiException<InternalServerErrorException>("server error"))
                .Throws(new MtgApiException<NotFoundException>("not found"))
                .Throws(new MtgApiException<ServiceUnavailableException>("unavailable"))
                .Throws<Exception>()
                .ReturnsAsync(new RootCardTypeDto() { Types = cardTypes });

            var service = new CardTypeService(moqAdapter.Object);

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
        /// Tests the constructors in the <see cref="CardTypeService"/> class.
        /// </summary>
        [TestMethod]
        public void ContructorTest()
        {
            CardTypeService service;
            PrivateObject privateObject;

            service = new CardTypeService();
            privateObject = new PrivateObject(service, new PrivateType(typeof(ServiceBase<CardTypeService, string>)));
            Assert.IsInstanceOfType(privateObject.GetFieldOrProperty("Adapter"), typeof(MtgApiServiceAdapter));
            Assert.AreEqual(ApiVersion.V1_0, privateObject.GetFieldOrProperty("Version"));
            Assert.AreEqual(ApiEndPoint.CardTypes, privateObject.GetFieldOrProperty("EndPoint"));

            service = new CardTypeService(new MtgApiServiceAdapter());
            privateObject = new PrivateObject(service, new PrivateType(typeof(ServiceBase<CardTypeService, string>)));
            Assert.IsInstanceOfType(privateObject.GetFieldOrProperty("Adapter"), typeof(MtgApiServiceAdapter));
            Assert.AreEqual(ApiVersion.V1_0, privateObject.GetFieldOrProperty("Version"));
            Assert.AreEqual(ApiEndPoint.CardTypes, privateObject.GetFieldOrProperty("EndPoint"));

            service = new CardTypeService(new MtgApiServiceAdapter(), ApiVersion.V1_0);
            privateObject = new PrivateObject(service, new PrivateType(typeof(ServiceBase<CardTypeService, string>)));
            Assert.IsInstanceOfType(privateObject.GetFieldOrProperty("Adapter"), typeof(MtgApiServiceAdapter));
            Assert.AreEqual(ApiVersion.V1_0, privateObject.GetFieldOrProperty("Version"));
            Assert.AreEqual(ApiEndPoint.CardTypes, privateObject.GetFieldOrProperty("EndPoint"));
        }
    }
}