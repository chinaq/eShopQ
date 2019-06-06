using Ordering.API.IntegrationEvents.EventHanding;
using Xunit;

namespace Ordering.UnitTests.API.Application.IntegrationEvents.EventHandling
{
    public class UserCheckoutAcceptedIntegrationEventHandlerTest
    {
        [Fact]
        public void HandleTest() {
            var handler = new UserCheckoutAcceptedIntegrationEventHandler(null, null);
            Assert.False(true);
        }

    }
}