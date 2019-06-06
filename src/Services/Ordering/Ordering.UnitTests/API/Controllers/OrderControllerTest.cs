using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.API.Application.Commands;
// using Ordering.API.Application.Queries;
using Ordering.API.Controllers;
// using Ordering.API.Infrastructure.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;


namespace Ordering.UnitTests.API.Controllers
{
    public class OrderControllerTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        // private readonly Mock<IOrderQueries> _orderQueriesMock;
        // private readonly Mock<IIdentityService> _identityServiceMock;
        private readonly Mock<ILogger<OrdersController>> _loggerMock;

        public OrderControllerTest()
        {
            _mediatorMock = new Mock<IMediator>();
            // _orderQueriesMock = new Mock<IOrderQueries>();
            // _identityServiceMock = new Mock<IIdentityService>();
            _loggerMock = new Mock<ILogger<OrdersController>>();
        }

        [Fact]
        public async Task Cancel_order_with_requestId_success()
        {
            //Arrange
            _mediatorMock
                .Setup(x => x.Send(
                    It.IsAny<IdentifiedCommand<CancelOrderCommand, bool>>(),
                    default(CancellationToken)))
                .Returns(Task.FromResult(true));

            //Act
            // var orderController = new OrdersController(_mediatorMock.Object, _orderQueriesMock.Object, _identityServiceMock.Object, _loggerMock.Object);
            var orderController = new OrdersController(_mediatorMock.Object, _loggerMock.Object);
            var actionResult = await orderController
                .CancelOrderAsync(new CancelOrderCommand(1), Guid.NewGuid().ToString()) as OkResult;

            //Assert
            Assert.Equal(actionResult.StatusCode, (int)System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task Cancel_order_bad_request()
        {
            //Arrange
            _mediatorMock
                .Setup(x => x.Send(
                    It.IsAny<IdentifiedCommand<CancelOrderCommand, bool>>(),
                    default(CancellationToken)))
                .Returns(Task.FromResult(true));

            //Act
            // var orderController = new OrdersController(_mediatorMock.Object, _orderQueriesMock.Object, _identityServiceMock.Object, _loggerMock.Object);
            var orderController = new OrdersController(_mediatorMock.Object, _loggerMock.Object);
            var actionResult = await orderController
                .CancelOrderAsync(new CancelOrderCommand(1), String.Empty) as BadRequestResult;

            //Assert
            Assert.Equal(actionResult.StatusCode, (int)System.Net.HttpStatusCode.BadRequest);
        }
    }
}