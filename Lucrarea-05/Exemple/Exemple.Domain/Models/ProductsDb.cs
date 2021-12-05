using System;
using System.Collections.Generic;
using LanguageExt;
using LanguageExt.Common;

namespace Exemple.Domain.Models {
	class ProductsDb {
		static Dictionary<int, double> products = new Dictionary<int, double> {
			{ 1, 9.99 },
			{ 2, 18.49 },
			{ 3, 4.20 }
		};

		public static Try<bool> productExists(int productCode) => () => {
			if (productCode < 0) {
				throw new Exception("Negative product code");
			}
			return products.ContainsKey(productCode);
		};

		public static Try<double> getPrice(int productCode) => () =>
		{
			if (!products.ContainsKey(productCode))
			{
				throw new NotImplementedException("Found no product with code: " + productCode);
			}
			double price;
			products.TryGetValue(productCode, out price);
			return price;
		};
	}
}
