using CSharp.Choices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exemple.Domain
{
    [AsChoice]
    public static partial class ShoppingCartPublishedEvent
    {
        public interface IShoppingCartPublishedEvent { }

        public record ShoppingCartPublishSucceededEvent : IShoppingCartPublishedEvent
        {
            public string Csv { get; }
            public DateTime PublishedDate { get; }

            internal ShoppingCartPublishSucceededEvent(string csv, DateTime publishedDate)
            {
                Csv = csv;
                PublishedDate = publishedDate;
            }
        }

        public record ShoppingCartPublishFaildEvent : IShoppingCartPublishedEvent
        {
            public string Reason { get; }

            internal ShoppingCartPublishFaildEvent(string reason)
            {
                Reason = reason;
            }
        }
    }
}
