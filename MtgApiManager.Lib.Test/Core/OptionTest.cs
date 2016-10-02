// <copyright file="OptionTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Core
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MtgApiManager.Lib.Core;

    /// <summary>
    /// Tests the <see cref="Option{T}"/> class.
    /// </summary>
    [TestClass]
    public class OptionTest
    {
        /// <summary>
        /// Tests the <see cref="Option.IfNone"/> method.
        /// </summary>
        [TestMethod]
        public void IfNoneTest()
        {
            var testObject = 0;

            try
            {
                // Test exception is thrown.
                Option<int>.Some(0).IfNone(null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("action", ex.ParamName);
            }
            catch
            {
                Assert.Fail();
            }

            Option<int>.Some(0).IfNone(() => testObject = 5);
            Assert.AreNotEqual(5, testObject);

            Option<int>.None().IfNone(() => testObject = 10);
            Assert.AreEqual(10, testObject);
        }

        /// <summary>
        /// Tests the <see cref="Option.IfSome"/> method.
        /// </summary>
        [TestMethod]
        public void IfSomeTest()
        {
            var testObject = 0;

            try
            {
                // Test exception is thrown.
                Option<int>.Some(0).IfSome(null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("action", ex.ParamName);
            }
            catch
            {
                Assert.Fail();
            }

            Option<int>.Some(0).IfSome(x => testObject = 5);
            Assert.AreEqual(5, testObject);

            Option<int>.None().IfSome(x => testObject = 10);
            Assert.AreNotEqual(10, testObject);
        }

        /// <summary>
        /// Tests the <see cref="Option.Map"/> method.
        /// </summary>
        [TestMethod]
        public void MapTest()
        {
            int a = 0;
            var testObject = Option<int>.Some(10);
            var mappedObject = testObject.Map(x => x * 2);

            try
            {
                // Test exception is thrown.
                testObject.Map<int>(null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("function", ex.ParamName);
            }
            catch
            {
                Assert.Fail();
            }

            testObject
                .IfSome(x => Assert.AreEqual(10, x))
                .IfNone(() => Assert.Fail());

            mappedObject
                .IfSome(x => Assert.AreEqual(20, x))
                .IfNone(() => Assert.Fail());

            testObject = Option<int>.None();
            mappedObject = testObject.Map(x => x * 2);

            testObject
                .IfSome(_ => Assert.Fail())
                .IfNone(() => a = 1);

            Assert.AreEqual(1, a);

            mappedObject
                .IfSome(_ => Assert.Fail())
                .IfNone(() => a = 2);

            Assert.AreEqual(2, a);
        }

        /// <summary>
        /// Tests the <see cref="Option.None"/> method.
        /// </summary>
        [TestMethod]
        public void NoneTest()
        {
            var testObject = Option<string>.None();

            Assert.AreEqual(false, testObject.HasValue);
        }

        /// <summary>
        /// Tests the <see cref="Option.Success"/> method.
        /// </summary>
        [TestMethod]
        public void SomeTest()
        {
            var testObject = Option<int>.Some(5);

            Assert.AreEqual(true, testObject.HasValue);
            Assert.AreEqual(5, testObject.Value);
        }

        /// <summary>
        /// Tests the <see cref="Option.Map"/> method.
        /// </summary>
        [TestMethod]
        public void ThenTest()
        {
            int a = 0;
            var testObject = Option<int>.Some(10);
            var mappedObject = testObject.Then(x => Option<int>.Some(x * 2));

            try
            {
                // Test exception is thrown.
                testObject.Then<int>(null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("function", ex.ParamName);
            }
            catch
            {
                Assert.Fail();
            }

            testObject
                .IfSome(x => Assert.AreEqual(10, x))
                .IfNone(() => Assert.Fail());

            mappedObject
                .IfSome(x => Assert.AreEqual(20, x))
                .IfNone(() => Assert.Fail());

            testObject = Option<int>.None();
            mappedObject = testObject.Then(x => Option<int>.Some(x * 2));

            testObject
                .IfSome(_ => Assert.Fail())
                .IfNone(() => a = 1);

            Assert.AreEqual(1, a);

            mappedObject
                .IfSome(_ => Assert.Fail())
                .IfNone(() => a = 2);

            Assert.AreEqual(2, a);
        }
    }
}