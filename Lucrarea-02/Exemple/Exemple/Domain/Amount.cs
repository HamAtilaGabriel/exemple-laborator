using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exemple.Domain
{
    public record Amount
    {
        public decimal Value { get; }

        public Amount(decimal value)
        {
            if (IsValid(value))
            {
                Value = value;
            }
            else
            {
                throw new InvalidAmountException($"{value:0.##} is an invalid amount value.");
            }
        }

        public static Amount operator *(Amount a, Amount b) => new Amount((a.Value * b.Value));

        public Amount Round()
        {
            var roundedValue = Math.Round(Value);
            return new Amount(roundedValue);
        }

        public override string ToString()
        {
            return $"{Value:0.##}";
        }

        public static bool TryParseAmount(string amountString, out Amount amount)
        {
            bool isValid = false;
            amount = null;
            if(decimal.TryParse(amountString, out decimal numericAmount))
            {
                if (IsValid(numericAmount))
                {
                    isValid = true;
                    amount = new(numericAmount);
                }
            }
            return isValid;
        }

        private static bool IsValid(decimal numericAmount) => numericAmount > 0 && numericAmount <= 20;
    }
}
