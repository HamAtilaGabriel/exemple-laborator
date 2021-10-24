using System;
using Exemple.Domain;
using System.Collections.Generic;
using static Exemple.Domain.Cos;

namespace Exemple
{
    class Program
    {

        static void Main(string[] args)
        {

            string answer = ReadValue("Cumparati?: ");
            if (answer.Contains("Da"))
            {
                var listOfProduse = ReadProduse().ToArray();
                var cosDetails = ReadDetails();

                UnvalidatedCos unvalidatedCos = new(listOfProduse, cosDetails);

                ICos result = CheckCos(unvalidatedCos);
                result.Match(
                    whenUnvalidatedCos: unvalidatedCos => unvalidatedCos,
                    whenGolCos: invalidResult => invalidResult,
                    whenInvalidatedCos: invalidResult => invalidResult,
                    whenValidatedCos: validatedCos => CosPlatit(validatedCos, cosDetails,DateTime.Now),
                    whenCosPlatit: cosPlatit => cosPlatit
                );

                Console.WriteLine(result);

            }
            else Console.WriteLine("Pa!");

        }
        private static ICos CheckCos(UnvalidatedCos unvalidatedCos) =>
           ( (unvalidatedCos.ProduseList.Count == 0) ? new GolCos(new List<UnvalidatedProduse>(), "cos gol")
                : ((string.IsNullOrEmpty(unvalidatedCos.CosDetails.PaymentAddress.Value))? new InvalidatedCos(new List<UnvalidatedProduse>(), "Cos Invalid")
                      :( (unvalidatedCos.CosDetails.PaymentState.Value == 0) ? new ValidatedCos(new List<ValidatedProduse>(), unvalidatedCos.CosDetails)
                             :new CosPlatit(new List<ValidatedProduse>(), unvalidatedCos.CosDetails, DateTime.Now))));
        
        private static ICos CosPlatit(ValidatedCos validatedResult, CosDetails cosDetails, DateTime PublishedDate) =>
                new CosPlatit(new List<ValidatedProduse>(), cosDetails, DateTime.Now);

        private static List<UnvalidatedProduse> ReadProduse()
        {
            List<UnvalidatedProduse> listOfProduse = new();
            object answer = null;
            do
            {
                answer = ReadValue("Adaugi produs?: ");

                if (answer.Equals("Da"))
                {
                    var ProdusID = ReadValue("ProdusID: ");
                    if (string.IsNullOrEmpty(ProdusID))
                    {
                        break;
                    }

                    var ProdusCantitate = ReadValue("ProdusCantitate: ");
                    if (string.IsNullOrEmpty(ProdusCantitate))
                    {
                        break;
                    }
                    UnvalidatedProduse toAdd = new(ProdusID, ProdusCantitate);
                    listOfProduse.Add(toAdd);
                }

            } while (!answer.Equals("Nu"));
            
            return listOfProduse;
        }

        public static CosDetails ReadDetails()
        {
            PaymentState paymentState;
            PaymentAddress paymentAddress;
            CosDetails cosDetails;

            string answer = ReadValue("Finalizezi Comanda?: ");

            if (answer.Contains("Da"))
            {

                var Address = ReadValue("Adresa: ");
                if (string.IsNullOrEmpty(Address))
                {
                    paymentAddress = new PaymentAddress("");
                }
                else
                {
                    paymentAddress = new PaymentAddress(Address);
                }
                var payment = ReadValue("Platesti?: ");
                if (payment.Contains("Da"))
                {
                    paymentState = new PaymentState(1);
                }
                else
                {
                    paymentState = new PaymentState(0);
                }
            }
            else
            {
                paymentAddress = new PaymentAddress("");
                paymentState = new PaymentState(0);
            }
            cosDetails = new CosDetails(paymentAddress, paymentState);
            return cosDetails;
         }

        private static string? ReadValue(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

    }
}
