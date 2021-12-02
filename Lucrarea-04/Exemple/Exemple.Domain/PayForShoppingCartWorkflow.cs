using System;
using System.Threading.Tasks;
using LanguageExt;
using static LanguageExt.Prelude;
using Exemple.Domain.Models;
using static Exemple.Domain.Models.ShoppingCarts;
using static Exemple.Domain.Models.ShoppingCartPaidEvent;
using static Exemple.Domain.ShoppingCartEntryOperation;

namespace Exemple.Domain {
    public class PayForShoppingCartWorkflow {
        public async Task<IShoppingCartPaidEvent> Execute(PayForShoppingCartCommand command, Func < ClientID, TryAsync<bool> > checkUserExists) {
            UnvalidatedShoppingCart unvalidatedShoppingCart = new UnvalidatedShoppingCart(command.InputShoppingCartEntries);
            IShoppingCart shoppingCart = await ValidateShoppingCart(checkUserExists, unvalidatedShoppingCart);
            shoppingCart = PayForShoppingCart(shoppingCart);

            return shoppingCart.Match(
                whenInvalidatedShoppingCart: invalidatedShoppingCart => new ShoppingCartPaymentFailedEvent(invalidatedShoppingCart.Reason) as IShoppingCartPaidEvent,
                whenUnvalidatedShoppingCart: unvalidatedShoppingCart => new ShoppingCartPaymentFailedEvent("Ssomething went wrong") as IShoppingCartPaidEvent,
                whenValidatedShoppingCart: validatedShoppingCart => new ShoppingCartPaymentFailedEvent("Something went wrong") as IShoppingCartPaidEvent,
                whenPaidShoppingCart: paidShoppingCart => new ShoppingCartPaymentSucceceededEvent(paidShoppingCart.Price, paidShoppingCart.PaymentDate) as IShoppingCartPaidEvent
            );
        }
    }
}