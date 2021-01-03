using MtgApiManager.Lib.Core.Exceptions;
using Xunit;

namespace MtgApiManager.Lib.Test.Core.Exceptions
{
    public class ServiceUnavailableExceptionTest
    {
        [Fact]
        public void Message_Correct()
        {
            // Given
            const string MESSAGE = "testing";

            // When
            var exception = new ServiceUnavailableException(MESSAGE);

            // Then
            Assert.EndsWith(MESSAGE, exception.Message);
        }
    }
}