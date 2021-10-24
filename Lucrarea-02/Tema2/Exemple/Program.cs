using Exemple.Domain;
using System;
using System.Collections.Generic;
using static Exemple.Domain.ShoppingCart;
using static Exemple.Domain.ShoppingCartOperation;

namespace Exemple
{
    class Program
    {
        private static readonly Random random = new Random();

        static void Main(string[] args)
        {
            var listOfProductID = ReadListOfProduct().ToArray();
            PublishShoppingCartCommand command = new(listOfProductID);
            PublishCartWorkflow workflow = new PublishCartWorkflow();
            var result = workflow.Execute(command, (productID) => true);

            result.Match(
                whenShoppingCartPublishFaildEvent: @event =>
                {
                    Console.WriteLine($"Publish failed: {@event.Reason }");
                    return @event;
                },
                whenShoppingCartPublishSucceededEvent: @event =>
                {
                    Console.WriteLine($"Publish succeded.");
                    Console.WriteLine(@event.Csv);
                    return @event;
                }
           );

            Console.WriteLine("Goodbye!");
        }

        private static List<UnvalidatedCart> ReadListOfProduct()
        {
            List<UnvalidatedCart> listOfProducts = new();
            do
            {
                var ProductID = ReadValue("Product ID: ");
                if (string.IsNullOrEmpty(ProductID))
                {
                    break;
                }

                var Adress = ReadValue("Adress: ");
                if (string.IsNullOrEmpty(Adress))
                {
                    break;
                }

                var Amount = ReadValue("Amount: ");
                if (string.IsNullOrEmpty(Amount))
                {
                    break;
                }

                var Price = ReadValue("Price: ");
                if (string.IsNullOrEmpty(Price))
                {
                    break;
                }

                listOfProducts.Add(new(ProductID, Adress, Amount, Price));

                string check = ReadValue("Keep shopping (Y/N) ? - ");
                if (check.Contains('N'))
                {
                    break;
                }
                
            } while (true);
            return listOfProducts;
        }

        private static string? ReadValue(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }
    }
}
