using MtgApiManager.Lib.Core.Exceptions;
using Xunit;

namespace MtgApiManager.Lib.Test.Core.Exceptions
{
    public class ForbiddenExceptionTest
    {
        [Fact]
        public void Message_Correct()
        {
            // Given
            const string MESSAGE = "testing";

            // When
            var exception = new ForbiddenException(MESSAGE);

            // Then
            Assert.EndsWith(MESSAGE, exception.Message);
        }
    }
}