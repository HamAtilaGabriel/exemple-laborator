using CSharp.Choices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exemple.Domain
{
    public record PublishShoppingCartCommand
    {
        public PublishShoppingCartCommand(IReadOnlyCollection<UnvalidatedCart> inputShoppingCart)
        {
            InputShoppingCart = inputShoppingCart;
        }

        public IReadOnlyCollection<UnvalidatedCart> InputShoppingCart { get; }
    }
}
