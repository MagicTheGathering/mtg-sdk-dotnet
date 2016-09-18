// <copyright file="WebUtilityTest.cs" company="Team7 Productions">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Core
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Lib.Core;
    using Dto.Cards;

    /// <summary>
    /// Tests the functionality of the <see cref="MtgApiServiceAdapter"/> class.
    /// </summary>
    [TestClass]
    public class MtgApiServiceAdapterTest
    {
        /// <summary>
        /// Tests the <see cref="MtgApiServiceAdapter.WebGetAsync{T}(System.Uri)"/> method.
        /// </summary>
        [TestMethod]
        public void WebGetAsyncTest()
        {
            MtgApiServiceAdapter adapter = new MtgApiServiceAdapter();

            var result = adapter.WebGetAsync<CardList>(new System.Uri("https://api.magicthegathering.io/v1/cards")).Result;
        }
    }
}