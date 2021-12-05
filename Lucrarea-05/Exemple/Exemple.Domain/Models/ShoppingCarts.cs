using CSharp.Choices;
using System;
using System.Collections.Generic;

namespace Exemple.Domain.Models {
    [AsChoice]
    public static partial class ShoppingCarts {
        public interface IShoppingCart {}

        public record UnvalidatedShoppingCart: IShoppingCart {
            public UnvalidatedShoppingCart(IReadOnlyCollection < UnvalidatedShoppingCartEntry > entryList) {
                EntryList = entryList;
            }

            public IReadOnlyCollection < UnvalidatedShoppingCartEntry > EntryList {
                get;
            }
        }

        public record ValidatedShoppingCart: IShoppingCart {
            public ValidatedShoppingCart(IReadOnlyCollection < ValidatedShoppingCartEntry > entryList) {
                EntryList = entryList;
            }

            public IReadOnlyCollection < ValidatedShoppingCartEntry > EntryList {
                get;
            }
        }

        public record InvalidatedShoppingCart : IShoppingCart
        {
            public InvalidatedShoppingCart(string reason)
            {
                Reason = reason;
            }

            public string Reason
            {
                get;
            }
        }

        public record PaidShoppingCart: IShoppingCart {
            public PaidShoppingCart(IReadOnlyCollection < ValidatedShoppingCartEntry > entryList, double price, DateTime paymentDate) {
                EntryList = entryList;
                Price = price;
                PaymentDate = paymentDate;
            }

            public IReadOnlyCollection < ValidatedShoppingCartEntry > EntryList {
                get;
            }

            public DateTime PaymentDate {
                get;
            }

            public double Price
            {
                get;
            }
        }
    }
}