// <copyright file="CardServiceTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Service
{
    using System;
    using System.Collections.Specialized;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using Lib.Core;
    using Lib.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MtgApiManager.Lib.Service;

    /// <summary>
    /// Tests the <see cref="CardService"/> class.
    /// </summary>
    [TestClass]
    public class CardServiceTest
    {
        /// <summary>
        /// Tests the constructors in the <see cref="CardService"/> class.
        /// </summary>
        [TestMethod]
        public void ContructorTest()
        {
            CardService service;
            PrivateObject privateObject;

            service = new CardService();
            privateObject = new PrivateObject(service, new PrivateType(typeof(ServiceBase<CardService, Card>)));
            Assert.IsInstanceOfType(privateObject.GetFieldOrProperty("Adapter"), typeof(MtgApiServiceAdapter));
            Assert.AreEqual(ApiVersion.V1_0, privateObject.GetFieldOrProperty("Version"));
            Assert.AreEqual(ApiEndPoint.Cards, privateObject.GetFieldOrProperty("EndPoint"));

            service = new CardService(new MtgApiServiceAdapter());
            privateObject = new PrivateObject(service, new PrivateType(typeof(ServiceBase<CardService, Card>)));
            Assert.IsInstanceOfType(privateObject.GetFieldOrProperty("Adapter"), typeof(MtgApiServiceAdapter));
            Assert.AreEqual(ApiVersion.V1_0, privateObject.GetFieldOrProperty("Version"));
            Assert.AreEqual(ApiEndPoint.Cards, privateObject.GetFieldOrProperty("EndPoint"));

            service = new CardService(new MtgApiServiceAdapter(), ApiVersion.V1_0);
            privateObject = new PrivateObject(service, new PrivateType(typeof(ServiceBase<CardService, Card>)));
            Assert.IsInstanceOfType(privateObject.GetFieldOrProperty("Adapter"), typeof(MtgApiServiceAdapter));
            Assert.AreEqual(ApiVersion.V1_0, privateObject.GetFieldOrProperty("Version"));
            Assert.AreEqual(ApiEndPoint.Cards, privateObject.GetFieldOrProperty("EndPoint"));
        }
    }
}