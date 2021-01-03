using MtgApiManager.Lib.Core.Exceptions;
using Xunit;

namespace MtgApiManager.Lib.Test.Core.Exceptions
{
    public class NotFoundExceptionTest
    {
        [Fact]
        public void Message_Correct()
        {
            // Given
            const string MESSAGE = "testing";

            // When
            var exception = new NotFoundException(MESSAGE);

            // Then
            Assert.EndsWith(MESSAGE, exception.Message);
        }
    }
}