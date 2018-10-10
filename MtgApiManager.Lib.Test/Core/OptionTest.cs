// <copyright file="OptionTest.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Core
{
    using System;

    using MtgApiManager.Lib.Core;
    using Xunit;

    /// <summary>
    /// Tests the <see cref="Option{T}"/> class.
    /// </summary>

    public class OptionTest
    {
        /// <summary>
        /// Tests the <see cref="Option.IfNone"/> method.
        /// </summary>
        [Fact]
        public void IfNoneTest()
        {
            var testObject = 0;

            try
            {
                // Test exception is thrown.
                Option<int>.Some(0).IfNone(null);
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

            Option<int>.Some(0).IfNone(() => testObject = 5);
            Assert.NotEqual(5, testObject);

            Option<int>.None().IfNone(() => testObject = 10);
            Assert.Equal(10, testObject);
        }

        /// <summary>
        /// Tests the <see cref="Option.IfSome"/> method.
        /// </summary>
        [Fact]
        public void IfSomeTest()
        {
            var testObject = 0;

            try
            {
                // Test exception is thrown.
                Option<int>.Some(0).IfSome(null);
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

            Option<int>.Some(0).IfSome(x => testObject = 5);
            Assert.Equal(5, testObject);

            Option<int>.None().IfSome(x => testObject = 10);
            Assert.NotEqual(10, testObject);
        }

        /// <summary>
        /// Tests the <see cref="Option.Map"/> method.
        /// </summary>
        [Fact]
        public void MapTest()
        {
            int a = 0;
            var testObject = Option<int>.Some(10);
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
                .IfSome(x => Assert.Equal(10, x))
                .IfNone(() => Assert.True(false));

            mappedObject
                .IfSome(x => Assert.Equal(20, x))
                .IfNone(() => Assert.True(false));

            testObject = Option<int>.None();
            mappedObject = testObject.Map(x => x * 2);

            testObject
                .IfSome(_ => Assert.True(false))
                .IfNone(() => a = 1);

            Assert.Equal(1, a);

            mappedObject
                .IfSome(_ => Assert.True(false))
                .IfNone(() => a = 2);

            Assert.Equal(2, a);
        }

        /// <summary>
        /// Tests the <see cref="Option.None"/> method.
        /// </summary>
        [Fact]
        public void NoneTest()
        {
            var testObject = Option<string>.None();

            Assert.False(testObject.HasValue);
        }

        /// <summary>
        /// Tests the <see cref="Option.Success"/> method.
        /// </summary>
        [Fact]
        public void SomeTest()
        {
            var testObject = Option<int>.Some(5);

            Assert.True(testObject.HasValue);
            Assert.Equal(5, testObject.Value);
        }

        /// <summary>
        /// Tests the <see cref="Option.Map"/> method.
        /// </summary>
        [Fact]
        public void ThenTest()
        {
            int a = 0;
            var testObject = Option<int>.Some(10);
            var mappedObject = testObject.Then(x => Option<int>.Some(x * 2));

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
                .IfSome(x => Assert.Equal(10, x))
                .IfNone(() => Assert.True(false));

            mappedObject
                .IfSome(x => Assert.Equal(20, x))
                .IfNone(() => Assert.True(false));

            testObject = Option<int>.None();
            mappedObject = testObject.Then(x => Option<int>.Some(x * 2));

            testObject
                .IfSome(_ => Assert.True(false))
                .IfNone(() => a = 1);

            Assert.Equal(1, a);

            mappedObject
                .IfSome(_ => Assert.True(false))
                .IfNone(() => a = 2);

            Assert.Equal(2, a);
        }
    }
}