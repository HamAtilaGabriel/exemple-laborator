using CSharp.Choices;
using System;

namespace Exemple.Domain.Models {
    [AsChoice]
    public static partial class ShoppingCartPaidEvent {
        public interface IShoppingCartPaidEvent { }

        public record ShoppingCartPaymentSucceceededEvent: IShoppingCartPaidEvent {
            public double Price {
                get;
            }
            public DateTime PaidDate {
                get;
            }

            internal ShoppingCartPaymentSucceceededEvent(double price, DateTime paidDate) {
                Price = price;
                PaidDate = paidDate;
            }
        }

        public record ShoppingCartPaymentFailedEvent: IShoppingCartPaidEvent {
            public string Reason {
                get;
            }

            internal ShoppingCartPaymentFailedEvent(string reason) {
                Reason = reason;
            }
        }
    }
}