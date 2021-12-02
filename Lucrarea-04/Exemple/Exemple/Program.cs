using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageExt;
using static Exemple.Domain.Models.ShoppingCarts;
using Example.Data;
using Example.Data.Repositories;
using Exemple.Domain;
using Exemple.Domain.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;

namespace Exemple {
    class Program {

        private static string SqlConnectionString = "Data Source=LAPTOP-PF9DAIU5\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True;";

        static async Task Main(string[] args) {
            using ILoggerFactory loggerFactory = ConfigureLoggerFactory();
            ILogger<PayForShoppingCartWorkflow> logger = loggerFactory.CreateLogger<PayForShoppingCartWorkflow>();
            var dbContextBuilder = new DbContextOptionsBuilder<OrdersContext>()
                                                .UseSqlServer(SqlConnectionString)
                                                .UseLoggerFactory(loggerFactory);
            OrdersContext ordersContext = new OrdersContext(dbContextBuilder.Options);
            OrderLinesRepository orderLinesRepository = new(ordersContext);
            await orderLinesRepository.TryGetExistingShoppingCartEntries(new int[] { 0 }) // todo: remove, this is for test purpose only
                .IfSucc(entries => {
                    foreach(ShoppingCartEntry entry in entries)
					{
                        Console.WriteLine(String.Format("\nGot DB entry: {0}\n", entry));
					}
                });
            var listOfProducts = ReadListOfProducts().ToArray();
            PayForShoppingCartCommand command = new(listOfProducts);
            PayForShoppingCartWorkflow workflow = new PayForShoppingCartWorkflow();
            var result = await workflow.Execute(command, CheckClientExists);
            result.Match(
                whenShoppingCartPaymentFailedEvent: @event =>
                {
                    Console.WriteLine($"Payment failed: {@event.Reason}");
                    return @event;
                },
                whenShoppingCartPaymentSucceceededEvent: @event =>
                {
                    Console.WriteLine($"Payment for {@event.Price}$ successful.");
                    return @event;
                }
            );

            Console.WriteLine("Done");
            Console.ReadLine();
        }

        private static List < UnvalidatedShoppingCartEntry > ReadListOfProducts() {
            List < UnvalidatedShoppingCartEntry > listOfShoppingCartEntries = new();
            var clientId = ReadValue("Client ID: ");
            if (string.IsNullOrEmpty(clientId)) {
                clientId = "1234";
            }
            do {
                var productCodeString = ReadValue("Product code: ");
                if (string.IsNullOrEmpty(productCodeString)) {
                    break;
                }

                var quantityString = ReadValue("Quantity: ");
                if (string.IsNullOrEmpty(quantityString)) {
                    break;
                }
                listOfShoppingCartEntries.Add(new(clientId, productCodeString, quantityString));
            } while (true);
            return listOfShoppingCartEntries;
        }

        private static string ? ReadValue(string prompt) {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        private static TryAsync<bool> CheckClientExists(ClientID clientID) => async () => true;

        private static ILoggerFactory ConfigureLoggerFactory()
        {
            return LoggerFactory.Create(builder =>
                                builder.AddSimpleConsole(options =>
                                {
                                    options.IncludeScopes = true;
                                    options.SingleLine = true;
                                    options.TimestampFormat = "hh:mm:ss ";
                                })
                                .AddProvider(new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider()));
        }
    }
}