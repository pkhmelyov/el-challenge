using System;
using System.Globalization;
using Newtonsoft.Json;

namespace EL_Challenge
{
    class Program
    {
        static void Main(string[] args)
        {
            double? amount = null,
                interest = null,
                downPayment = null,
                term = null;

            while (true)
            {
                string line = Console.ReadLine();
                string[] input = line.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (input.Length < 2) continue;
                switch (input[0].Trim().ToUpperInvariant())
                {
                    case "AMOUNT":
                        amount = GetAmount(input[1].Trim());
                        break;
                    case "INTEREST":
                        interest = GetInterest(input[1].Trim());
                        break;
                    case "DOWNPAYMENT":
                        downPayment = GetDownPayment(input[1].Trim());
                        break;
                    case "TERM":
                        term = GetTerm(input[1].Trim());
                        break;
                }

                if (amount.HasValue && interest.HasValue && downPayment.HasValue && term.HasValue)
                {
                    Console.ReadLine();
                    break;
                }
            }

            var calc = new LoanCalculator(amount.Value, interest.Value, downPayment.Value, term.Value);
            Console.WriteLine(JsonConvert.SerializeObject(calc, Formatting.Indented));
        }

        static double GetAmount(string input) => double.Parse(input, CultureInfo.InvariantCulture);

        static double GetInterest(string input) => input.EndsWith("%") ?
            double.Parse(input.TrimEnd('%'), CultureInfo.InvariantCulture) :
            double.Parse(input, CultureInfo.InvariantCulture) * 100;

        static double GetDownPayment(string input) => double.Parse(input, CultureInfo.InvariantCulture);

        static double GetTerm(string input) => double.Parse(input, CultureInfo.InvariantCulture);
    }
}
