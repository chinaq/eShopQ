using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Ordering.API.Application.Commands;
using Ordering.API.Application.IntegrationEvents.Events;
using Ordering.API.Application.Models;
using Ordering.API.IntegrationEvents.EventHanding;
using Xunit;

namespace Ordering.UnitTests.API.Application.IntegrationEvents.EventHandling
{
    public class UserCheckoutAcceptedIntegrationEventHandlerTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ILogger<UserCheckoutAcceptedIntegrationEventHandler>> _logger;

        public UserCheckoutAcceptedIntegrationEventHandlerTest()
        {
            _mediatorMock = new Mock<IMediator>();
            // _orderQueriesMock = new Mock<IOrderQueries>();
            // _identityServiceMock = new Mock<IIdentityService>();
            _logger = new Mock<ILogger<UserCheckoutAcceptedIntegrationEventHandler>>();
        }

        [Fact]
        public async Task Handle_mediator_sent_once_Test() {
            ////Arrange
            UserCheckoutAcceptedIntegrationEvent @event = new UserCheckoutAcceptedIntegrationEvent(
                "user123", null, null, null, null, null, null, null, null,
                DateTime.Now, null,
                333, null,
                Guid.NewGuid(),
                new CustomerBasket("user123")
            );
            // action
            var handler = new UserCheckoutAcceptedIntegrationEventHandler(_mediatorMock.Object, _logger.Object);
            await handler.Handle(@event);
            // assert
            _mediatorMock.Verify(
                h => h.Send(
                    It.IsAny<IdentifiedCommand<CreateOrderCommand, bool>>(),
                    It.IsAny<CancellationToken>()),
                Times.Once());
        }

        [Fact]
        public async Task Handle_get_request_Id_Test() {
            ////Arrange
            IdentifiedCommand<CreateOrderCommand, bool> reqCreateOrder = null;
            _mediatorMock
                .Setup(x => x.Send(
                    It.IsAny<IdentifiedCommand<CreateOrderCommand, bool>>(),
                    It.IsAny<CancellationToken>()))
                .Callback<IRequest<bool>, CancellationToken>(
                    (req, token) => reqCreateOrder = (IdentifiedCommand<CreateOrderCommand, bool>)req)
                .Returns(Task.FromResult(true));

            UserCheckoutAcceptedIntegrationEvent @event = new UserCheckoutAcceptedIntegrationEvent(
                "user123", null, null, null, null, null, null, null, null,
                DateTime.Now, null,
                333, null,
                Guid.NewGuid(),
                new CustomerBasket("user123")
            );
            // action
            var handler = new UserCheckoutAcceptedIntegrationEventHandler(_mediatorMock.Object, _logger.Object);
            await handler.Handle(@event);
            // assert
            Assert.Equal("user123", reqCreateOrder.Command.UserId);
        }
    }
}