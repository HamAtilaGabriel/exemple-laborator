using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Exemple.Domain
{
    public record Adress
    {
        private static readonly Regex ValidPattern = new("^LM[0-9]{10}$");

        public string Value { get; }

        private Adress(string value)
        {
            if (IsValid(value))
            {
                Value = value;
            }
            else
            {
                throw new InvalidAdressException("");
            }
        }

        private static bool IsValid(string stringValue) => ValidPattern.IsMatch(stringValue);

        public override string ToString()
        {
            return Value;
        }

        public static bool TryParseAdress(string stringValue, out Adress adress)
        {
            bool isValid = false;
            adress = null;

            if (IsValid(stringValue))
            {
                isValid = true;
                adress = new(stringValue);
            }

            return isValid;
        }
    }
}
