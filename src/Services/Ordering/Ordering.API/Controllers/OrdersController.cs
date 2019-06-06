using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
// using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Extensions;
// using Ordering.API.Application.Commands;
// using Ordering.API.Application.Queries;
// using Ordering.API.Infrastructure.Services;
using Microsoft.Extensions.Logging;
using Ordering.API.Application.Commands;
// using Ordering.API.Application.Behaviors;
// using Ordering.API.Application.Commands;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Ordering.API.Controllers
{
    public class OrdersController: ControllerBase
    {
        private readonly IMediator _mediator;
        // private readonly IOrderQueries _orderQueries;
        // private readonly IIdentityService _identityService;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(
            IMediator mediator, 
            // IOrderQueries orderQueries, 
            // IIdentityService identityService,
            ILogger<OrdersController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            // _orderQueries = orderQueries ?? throw new ArgumentNullException(nameof(orderQueries));
            // _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [Route("cancel")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CancelOrderAsync(
            [FromBody]CancelOrderCommand command,
            [FromHeader(Name = "x-requestid")] string requestId)
        {
            bool commandResult = false;
            if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
            {
                var requestCancelOrder = new IdentifiedCommand<CancelOrderCommand, bool>(command, guid);
                // _logger.LogInformation(
                //     "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                //     requestCancelOrder.GetGenericTypeName(),
                //     nameof(requestCancelOrder.Command.OrderNumber),
                //     requestCancelOrder.Command.OrderNumber,
                //     requestCancelOrder);
                commandResult = await _mediator.Send(requestCancelOrder);
            }
            if (!commandResult)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}