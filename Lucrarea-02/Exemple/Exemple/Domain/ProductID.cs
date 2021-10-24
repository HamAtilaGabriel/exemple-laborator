using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exemple.Domain
{
    public record ProductID
    {
        public decimal Value { get; }

        public ProductID(decimal value)
        {
            if (IsValid(value))
            {
                Value = value;
            }
            else
            {
                throw new InvalidProductIDException($"{value:0.##} is an invalid product ID value.");
            }
        }

        public ProductID Round()
        {
            var roundedValue = Math.Round(Value);
            return new ProductID(roundedValue);
        }

        public override string ToString()
        {
            return $"{Value:0.##}";
        }

        public static bool TryParseProductID(string productIDString, out ProductID productID)
        {
            bool isValid = false;
            productID = null;
            if(decimal.TryParse(productIDString, out decimal numericProductID))
            {
                if(IsValid(numericProductID))
                {
                    isValid = true;
                    productID = new(numericProductID);
                }
            }
            return isValid;
        }

        private static bool IsValid(decimal numericProductID) => numericProductID > 0 && numericProductID <= 10;
    }
}
