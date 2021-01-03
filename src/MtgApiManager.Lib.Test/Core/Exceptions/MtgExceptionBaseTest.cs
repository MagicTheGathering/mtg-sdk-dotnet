using MtgApiManager.Lib.Core.Exceptions;
using Xunit;
namespace MtgApiManager.Lib.Test.Core.Exceptions
{
    public class MtgExceptionBaseTest
    {
        [Fact]
        public void Message_Correct()
        {
            // Given
            const string MESSAGE = "testing";

            // When
            var exception = new MtgExceptionBase(MESSAGE);

            // Then
            Assert.StartsWith(Properties.Resources.MtgError, exception.Message);
            Assert.EndsWith(MESSAGE, exception.Message);
        }
    }
}