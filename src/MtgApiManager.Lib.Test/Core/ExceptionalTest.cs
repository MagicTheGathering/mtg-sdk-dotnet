// <copyright file="ExceptionalTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Core
{
    using MtgApiManager.Lib.Core;
    using System;
    using Xunit;

    /// <summary>
    /// Tests the <see cref="Exceptional{T}"/> class.
    /// </summary>

    public class ExceptionalTest
    {
        /// <summary>
        /// Tests the <see cref="Exceptional{T}.Failure(Exception)"/> method.
        /// </summary>
        [Fact]
        public void FailureTest()
        {
            var testObject = Exceptional<int?>.Failure(new ArgumentNullException("test"));
            Assert.False(testObject.IsSuccess);
            Assert.IsType<ArgumentNullException>(testObject.Exception);
            Assert.Equal("test", ((ArgumentNullException)testObject.Exception).ParamName);
        }

        /// <summary>
        /// Tests the <see cref="Exceptional{T}.IfFailure(Action{Exception})"/> method.
        /// </summary>
        [Fact]
        public void IfFailureTest()
        {
            var testObject = 0;

            try
            {
                // Test exception is thrown.
                Exceptional<int>.Success(0, new PagingInfo(10, 5)).IfFailure(null); 
                Assert.True(false);
            }
            catch (ArgumentNullException ex)
            {
                Assert.Equal("action", ex.ParamName);
            }
            catch
            {
                Assert.True(false);
            }

            Exceptional<int>.Success(0, new PagingInfo(10, 5)).IfFailure(x => testObject = 5);
            Assert.NotEqual(5, testObject);

            Exceptional<int>.Failure(new ArgumentNullException("test")).IfFailure(x => testObject = 10);
            Assert.Equal(10, testObject);
        }

        /// <summary>
        /// Tests the <see cref="Exceptional{T}.IfSuccess(Action{T})"/> method.
        /// </summary>
        [Fact]
        public void IfSuccessTest()
        {
            var testObject = 0;

            try
            {
                // Test exception is thrown.
                Exceptional<int>.Success(0, new PagingInfo(10, 5)).IfSuccess(null);
                Assert.True(false);
            }
            catch (ArgumentNullException ex)
            {
                Assert.Equal("action", ex.ParamName);
            }
            catch
            {
                Assert.True(false);
            }

            Exceptional<int>.Success(0, new PagingInfo(10, 5)).IfSuccess(x => testObject = 5);
            Assert.Equal(5, testObject);

            Exceptional<int>.Failure(new ArgumentNullException("test")).IfSuccess(x => testObject = 10);
            Assert.NotEqual(10, testObject);
        }

        /// <summary>
        /// Tests the <see cref="Exceptional{T}.Map{TNewValue}(Func{T, TNewValue})"/> method.
        /// </summary>
        [Fact]
        public void MapTest()
        {
            var testObject = Exceptional<int>.Success(10, new PagingInfo(10, 5));
            var mappedObject = testObject.Map(x => x * 2);

            try
            {
                // Test exception is thrown.
                testObject.Map<int>(null);
                Assert.True(false);
            }
            catch (ArgumentNullException ex)
            {
                Assert.Equal("function", ex.ParamName);
            }
            catch
            {
                Assert.True(false);
            }

            testObject
                .IfSuccess(x => Assert.Equal(10, x))
                .IfFailure(_ => Assert.True(false));

            mappedObject
                .IfSuccess(x => Assert.Equal(20, x))
                .IfFailure(_ => Assert.True(false));

            testObject = Exceptional<int>.Failure(new ArgumentNullException("test"));
            mappedObject = testObject.Map(x => x * 2);

            testObject
                .IfSuccess(_ => Assert.True(false))
                .IfFailure(x => Assert.IsType<ArgumentNullException>(x));

            mappedObject
                .IfSuccess(_ => Assert.True(false))
                .IfFailure(x => Assert.IsType<ArgumentNullException>(x));
        }

        /// <summary>
        /// Tests the <see cref="Exceptional{T}.Success(T)"/> method.
        /// </summary>
        [Fact]
        public void SuccessTest()
        {
            var testObject = Exceptional<int>.Success(5, new PagingInfo(10, 5));

            Assert.True(testObject.IsSuccess);
            Assert.Equal(5, testObject.Value);
        }

        /// <summary>
        /// Tests the <see cref="Exceptional{T}.Then{TNewValue}(Func{T, Exceptional{TNewValue}})"/> method.
        /// </summary>
        [Fact]
        public void ThenTest()
        {
            var testObject = Exceptional<int>.Success(10, new PagingInfo(10, 5));
            var mappedObject = testObject.Then(x => Exceptional<int>.Success(x * 2, new PagingInfo(10, 5)));

            try
            {
                // Test exception is thrown.
                testObject.Then<int>(null);
                Assert.True(false);
            }
            catch (ArgumentNullException ex)
            {
                Assert.Equal("function", ex.ParamName);
            }
            catch
            {
                Assert.True(false);
            }

            testObject
                .IfSuccess(x => Assert.Equal(10, x))
                .IfFailure(_ => Assert.True(false));

            mappedObject
                .IfSuccess(x => Assert.Equal(20, x))
                .IfFailure(_ => Assert.True(false));

            testObject = Exceptional<int>.Success(10, new PagingInfo(10, 5));
            mappedObject = testObject.Then(x => Exceptional<int>.Failure(new ArgumentNullException("test")));

            testObject
                .IfSuccess(x => Assert.Equal(10, x))
                .IfFailure(_ => Assert.True(false));

            mappedObject
                .IfSuccess(_ => Assert.True(false))
                .IfFailure(x => Assert.IsType<ArgumentNullException>(x));

            testObject = Exceptional<int>.Failure(new ArgumentNullException("test"));
            mappedObject = testObject.Then(x => Exceptional<int>.Failure(new ArgumentNullException("test")));

            testObject
                .IfSuccess(_ => Assert.True(false))
                .IfFailure(x => Assert.IsType<ArgumentNullException>(x));

            mappedObject
                .IfSuccess(_ => Assert.True(false))
                .IfFailure(x => Assert.IsType<ArgumentNullException>(x));
        }
    }
}