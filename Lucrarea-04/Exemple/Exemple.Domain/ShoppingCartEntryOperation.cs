using Exemple.Domain.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageExt;
using static LanguageExt.Prelude;
using static Exemple.Domain.Models.ShoppingCarts;
using static Exemple.Domain.Models.ShoppingCartEntry;

namespace Exemple.Domain {
    public static class ShoppingCartEntryOperation {
        public static Task<IShoppingCart> ValidateShoppingCart(Func < ClientID, TryAsync<bool> > checkClientExists, UnvalidatedShoppingCart shoppingCart) =>
            shoppingCart.EntryList
                .Select(ValidateShoppingCartEntry(checkClientExists))
                .Aggregate(CreateEmptyValidatedEntriesList().ToAsync(), ReduceValidEntries)
                .MatchAsync(
                    Right: validatedEntries => new ValidatedShoppingCart(validatedEntries),
                    LeftAsync: errorMessage => Task.FromResult((IShoppingCart) new InvalidatedShoppingCart(errorMessage))
                );

        private static Func<UnvalidatedShoppingCartEntry, EitherAsync<string, ValidatedShoppingCartEntry>> ValidateShoppingCartEntry(Func<ClientID, TryAsync<bool>> checkClientExists) =>
            UnvalidatedShoppingCartEntry => ValidateShoppingCartEntry(checkClientExists, UnvalidatedShoppingCartEntry);

        private static EitherAsync<string, ValidatedShoppingCartEntry> ValidateShoppingCartEntry(Func<ClientID, TryAsync<bool>> checkClientExists, UnvalidatedShoppingCartEntry unvalidatedShoppingCartEntry) =>
            from shoppingCartEntry in TryParseShoppingCartEntry(unvalidatedShoppingCartEntry.code, unvalidatedShoppingCartEntry.quantity)
                                   .ToEitherAsync(() => $"Invalid shopping cart entry (client id: {unvalidatedShoppingCartEntry.ClientId}, code: {unvalidatedShoppingCartEntry.code}, quantity: {unvalidatedShoppingCartEntry.quantity})")
            from clientId in ClientID.TryParse(unvalidatedShoppingCartEntry.ClientId)
                                   .ToEitherAsync(() => $"Invalid shopping cart entry (client id: {unvalidatedShoppingCartEntry.ClientId}, code: {unvalidatedShoppingCartEntry.code}, quantity: {unvalidatedShoppingCartEntry.quantity})")
            from clientExists in checkClientExists(clientId)
                                   .ToEither(error => error.ToString())
            select new ValidatedShoppingCartEntry(clientId, shoppingCartEntry);

        private static Either<string, List<ValidatedShoppingCartEntry>> CreateEmptyValidatedEntriesList() =>
            Right(new List<ValidatedShoppingCartEntry>());

        private static EitherAsync<string, List<ValidatedShoppingCartEntry>> ReduceValidEntries(EitherAsync<string, List<ValidatedShoppingCartEntry>> acc, EitherAsync<string, ValidatedShoppingCartEntry> next) =>
            from list in acc
            from nextEntry in next
            select list.AppendValidEntry(nextEntry);

        private static List<ValidatedShoppingCartEntry> AppendValidEntry(this List<ValidatedShoppingCartEntry> list, ValidatedShoppingCartEntry validEntry)
        {
            list.Add(validEntry);
            return list;
        }

        public static IShoppingCart PayForShoppingCart(IShoppingCart shoppingCart) => shoppingCart.Match(
            whenInvalidatedShoppingCart: invalidatedShoppingCart => invalidatedShoppingCart,
            whenUnvalidatedShoppingCart: unvalidatedShoppingCart => unvalidatedShoppingCart,
            whenPaidShoppingCart: paidShoppingCart => paidShoppingCart,
            whenValidatedShoppingCart: validatedShoppingCart => {
                double totalPrice = 0;
                foreach(ValidatedShoppingCartEntry entry in validatedShoppingCart.EntryList) {
                    double price = ProductsDb.getPrice(entry.ShoppingCartEntry.Code).Match(
                        Succ: price => price,
                        Fail: exception => 0
                    );
                    totalPrice += (price * entry.ShoppingCartEntry.Quantity);
                }
                return new PaidShoppingCart(validatedShoppingCart.EntryList, totalPrice, DateTime.Now);
            }
        );
    }
}