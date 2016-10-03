// <copyright file="CardServiceTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Service
{
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
            var service = new CardService();
            var result = service.Find(12121212);
        }
    }
}