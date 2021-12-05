using System.Collections.Generic;

namespace Exemple.Domain.Models {
	public record PayForShoppingCartCommand {
		public PayForShoppingCartCommand(IReadOnlyCollection < UnvalidatedShoppingCartEntry > inputShoppingCartEntries) {
			InputShoppingCartEntries = inputShoppingCartEntries;
		}

		public IReadOnlyCollection < UnvalidatedShoppingCartEntry > InputShoppingCartEntries {
			get;
		}
	}
}