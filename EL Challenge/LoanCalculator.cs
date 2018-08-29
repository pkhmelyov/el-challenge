using System;
using Newtonsoft.Json;

namespace EL_Challenge
{
    class LoanCalculator
    {
        public LoanCalculator(double amount, double interest, double downPayment, double term)
        {
            Amount = amount;
            Interest = interest;
            DownPayment = downPayment;
            Term = term;
        }

        [JsonIgnore]
        public double Amount { get; }
        [JsonIgnore]
        public double Interest { get; }
        [JsonIgnore]
        public double DownPayment { get; }
        [JsonIgnore]
        public double Term { get; }

        [JsonIgnore]
        public double MonthlyPayment
        {
            get
            {
                double amount = Amount - DownPayment;
                double interest = Interest / 100 / 12;
                double term = Term * 12;
                return amount * (interest + interest / (Math.Pow(1 + interest, term) - 1));
            }
        }

        [JsonIgnore]
        public double TotalPayment { get => MonthlyPayment * Term * 12; }

        [JsonIgnore]
        public double TotalInterest { get => Math.Round(TotalPayment - Amount + DownPayment, 2); }

        [JsonProperty("monthly payment", Order = 1)]
        public double MonthlyPaymentRounded { get => Math.Round(MonthlyPayment, 2); }

        [JsonProperty("total payment", Order = 3)]
        public double TotalPaymentRounded { get => Math.Round(TotalPayment, 2); }

        [JsonProperty("total interest", Order = 2)]
        public double TotalInterestRounded { get => Math.Round(TotalInterest, 2); }
    }
}
