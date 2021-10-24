using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exemple.Domain
{
    public record ValidatedCart(Adress Adress, ProductID ProductID , Amount Amount , Amount Price);
}
