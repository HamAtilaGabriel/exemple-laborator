using Exemple.Domain;
using static Exemple.Domain.ShoppingCartPublishedEvent;
using static Exemple.Domain.ShoppingCartOperation;
using System;
using static Exemple.Domain.ShoppingCart;




namespace Exemple.Domain
{
    public class PublishCartWorkflow
    {
        public IShoppingCartPublishedEvent Execute(PublishShoppingCartCommand command, Func<ProductID, bool> checkProductExists)
        {
            UnvalidatedSCart unvalidatedCart = new UnvalidatedSCart(command.InputShoppingCart);
            IShoppingCart cart = ValidateShoppingCart(checkProductExists, unvalidatedCart);
            cart = CalculateFinalPrice(cart);
            cart = PublishSCart(cart);

            return cart.Match(
                whenUnvalidatedSCart: unvalidatedCart => new ShoppingCartPublishFaildEvent("Unexpected unvalidated state") as IShoppingCartPublishedEvent,
                whenInvalidatedSCart: invalidCart => new ShoppingCartPublishFaildEvent(invalidCart.Reason),
                whenValidatedSCart: validatedCart => new ShoppingCartPublishFaildEvent("Unexpected validated state"),
                whenCalculatedSCart: calculatedCart => new ShoppingCartPublishFaildEvent("Unexpected validated state"),
                whenPublishedSCart: publishedCart => new ShoppingCartPublishSucceededEvent(publishedCart.Csv, publishedCart.PublishedDate)
                );
        }
    }
}
