using System.Text.RegularExpressions;
using LanguageExt;
using static LanguageExt.Prelude;

namespace Exemple.Domain.Models {
    public record ClientID {
        private static readonly Regex ValidPattern = new("^[0-9]{4}$");

        public string Id {
            get;
        }

        private ClientID(string id) {
            if (IsValid(id)) {
                Id = id;
            } else {
                throw new InvalidClientIdException("");
            }
        }

        public static Option<ClientID> TryParse(string stringId) {
            if (IsValid(stringId)) {
                return Some<ClientID>(new(stringId));
            }
            return None;
        }

        private static bool IsValid(string stringId) => ValidPattern.IsMatch(stringId);

        public override string ToString() {
            return Id;
        }
    }
}