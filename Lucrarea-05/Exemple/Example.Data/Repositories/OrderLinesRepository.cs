using Exemple.Domain.Models;
using Exemple.Domain.Repositories;
using LanguageExt;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Example.Data.Repositories
{
    public class OrderLinesRepository: IOrderLinesRepository
    {
        private readonly OrdersContext ordersContext;

        public OrderLinesRepository(OrdersContext ordersContext)
        {
            this.ordersContext = ordersContext;  
        }

        public TryAsync<List<ShoppingCartEntry>> TryGetExistingShoppingCartEntries(IEnumerable<int> ordersToCheck) => async () =>
        {
            var orders = await ordersContext.OrderLines
                                              .Where(orderLine => ordersToCheck.Contains(orderLine.OrderLineId))
                                              .AsNoTracking()
                                              .ToListAsync();
            return orders.Select(order => new ShoppingCartEntry(order.ProductId, order.Quantity)) // todo: get code from Product db
                           .ToList();
        };
    }
}
