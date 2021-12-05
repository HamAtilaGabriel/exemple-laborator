using Exemple.Domain;
using Exemple.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System;
using Example.Api.Models;
using Exemple.Domain.Models;

namespace Example.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private ILogger<OrdersController> logger;

        public OrdersController(ILogger<OrdersController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders([FromServices] IOrderLinesRepository ordersRepository) =>
            await ordersRepository.TryGetExistingShoppingCartEntries(new int[] { 0 }).Match(
               Succ: GetAllOrdersHandleSuccess,
               Fail: GetAllOrdersHandleError
            );

        private ObjectResult GetAllOrdersHandleError(Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return base.StatusCode(StatusCodes.Status500InternalServerError, "UnexpectedError");
        }

        private OkObjectResult GetAllOrdersHandleSuccess(List<Exemple.Domain.Models.ShoppingCartEntry> entries) =>
        Ok(entries.Select(entry => new
        {
            Order = entry.Code,
            entry.Quantity
        }));

        [HttpPost]
        public async Task<IActionResult> PublishGrades([FromServices]PayForShoppingCartWorkflow payForShoppingCartWorkflow, [FromBody]InputOrder[] cartEntries)
        {
            var unvalidatedCartEntries = cartEntries.Select(MapInputGradeToUnvalidatedGrade)
                                          .ToList()
                                          .AsReadOnly();
            PayForShoppingCartCommand command = new(unvalidatedCartEntries);
            var result = await payForShoppingCartWorkflow.Execute(command, (ClientID clientID) => async () => true);
            return result.Match(
                whenShoppingCartPaymentFailedEvent: failedEvent => StatusCode(StatusCodes.Status500InternalServerError, failedEvent.Reason),
                whenShoppingCartPaymentSucceceededEvent: successEvent => Ok(successEvent.Price)
            );
        }

        private static UnvalidatedShoppingCartEntry MapInputGradeToUnvalidatedGrade(InputOrder cartEntry) => new UnvalidatedShoppingCartEntry(
            ClientId: cartEntry.ClientId,
            code: cartEntry.ProductCode,
            quantity: cartEntry.Quantity);
    }
}
