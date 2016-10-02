// <copyright file="ExceptionalTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Core
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MtgApiManager.Lib.Core;

    /// <summary>
    /// Tests the <see cref="Exceptional{T}"/> class.
    /// </summary>
    [TestClass]
    public class ExceptionalTest
    {
        /// <summary>
        /// Tests the <see cref="Exceptional{T}.Failure(Exception)"/> method.
        /// </summary>
        [TestMethod]
        public void FailureTest()
        {
            var testObject = Exceptional<int?>.Failure(new ArgumentNullException("test"));
            Assert.AreEqual(false, testObject.IsSuccess);
            Assert.IsInstanceOfType(testObject.Exception, typeof(ArgumentNullException));
            Assert.AreEqual("test", ((ArgumentNullException)testObject.Exception).ParamName);
        }

        /// <summary>
        /// Tests the <see cref="Exceptional{T}.IfFailure(Action{Exception})"/> method.
        /// </summary>
        [TestMethod]
        public void IfFailureTest()
        {
            var testObject = 0;

            try
            {
                // Test exception is thrown.
                Exceptional<int>.Success(0).IfFailure(null);
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

            Exceptional<int>.Success(0).IfFailure(x => testObject = 5);
            Assert.AreNotEqual(5, testObject);

            Exceptional<int>.Failure(new ArgumentNullException("test")).IfFailure(x => testObject = 10);
            Assert.AreEqual(10, testObject);
        }

        /// <summary>
        /// Tests the <see cref="Exceptional{T}.IfSuccess(Action{T})"/> method.
        /// </summary>
        [TestMethod]
        public void IfSuccessTest()
        {
            var testObject = 0;

            try
            {
                // Test exception is thrown.
                Exceptional<int>.Success(0).IfSuccess(null);
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

            Exceptional<int>.Success(0).IfSuccess(x => testObject = 5);
            Assert.AreEqual(5, testObject);

            Exceptional<int>.Failure(new ArgumentNullException("test")).IfSuccess(x => testObject = 10);
            Assert.AreNotEqual(10, testObject);
        }

        /// <summary>
        /// Tests the <see cref="Exceptional{T}.Map{TNewValue}(Func{T, TNewValue})"/> method.
        /// </summary>
        [TestMethod]
        public void MapTest()
        {
            var testObject = Exceptional<int>.Success(10);
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
                .IfSuccess(x => Assert.AreEqual(10, x))
                .IfFailure(_ => Assert.Fail());

            mappedObject
                .IfSuccess(x => Assert.AreEqual(20, x))
                .IfFailure(_ => Assert.Fail());

            testObject = Exceptional<int>.Failure(new ArgumentNullException("test"));
            mappedObject = testObject.Map(x => x * 2);

            testObject
                .IfSuccess(_ => Assert.Fail())
                .IfFailure(x => Assert.IsInstanceOfType(x, typeof(ArgumentException)));

            mappedObject
                .IfSuccess(_ => Assert.Fail())
                .IfFailure(x => Assert.IsInstanceOfType(x, typeof(ArgumentException)));
        }

        /// <summary>
        /// Tests the <see cref="Exceptional{T}.Success(T)"/> method.
        /// </summary>
        [TestMethod]
        public void SuccessTest()
        {
            var testObject = Exceptional<int>.Success(5);

            Assert.AreEqual(true, testObject.IsSuccess, "The Result was expected to be a success, but it was a failure.");
            Assert.AreEqual(5, testObject.Value, "The Result value did not match the expected result.");
        }

        /// <summary>
        /// Tests the <see cref="Exceptional{T}.Then{TNewValue}(Func{T, Exceptional{TNewValue}})"/> method.
        /// </summary>
        [TestMethod]
        public void ThenTest()
        {
            var testObject = Exceptional<int>.Success(10);
            var mappedObject = testObject.Then(x => Exceptional<int>.Success(x * 2));

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
                .IfSuccess(x => Assert.AreEqual(10, x))
                .IfFailure(_ => Assert.Fail());

            mappedObject
                .IfSuccess(x => Assert.AreEqual(20, x))
                .IfFailure(_ => Assert.Fail());

            testObject = Exceptional<int>.Success(10);
            mappedObject = testObject.Then(x => Exceptional<int>.Failure(new ArgumentNullException("test")));

            testObject
                .IfSuccess(x => Assert.AreEqual(10, x))
                .IfFailure(_ => Assert.Fail());

            mappedObject
                .IfSuccess(_ => Assert.Fail())
                .IfFailure(x => Assert.IsInstanceOfType(x, typeof(ArgumentException)));

            testObject = Exceptional<int>.Failure(new ArgumentNullException("test"));
            mappedObject = testObject.Then(x => Exceptional<int>.Failure(new ArgumentNullException("test")));

            testObject
                .IfSuccess(_ => Assert.Fail())
                .IfFailure(x => Assert.IsInstanceOfType(x, typeof(ArgumentException)));

            mappedObject
                .IfSuccess(_ => Assert.Fail())
                .IfFailure(x => Assert.IsInstanceOfType(x, typeof(ArgumentException)));
        }
    }
}