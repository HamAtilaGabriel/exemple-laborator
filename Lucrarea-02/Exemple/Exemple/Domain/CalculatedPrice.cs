using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exemple.Domain
{
    public record CalculatedPricee(ProductID ProductID, Adress Adress, Amount Amount, Amount Price, Amount FinalPrice);
}
