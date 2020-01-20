using System;
using System.Collections.Generic;
using System.Text;

namespace Strategy.LoanCalculator.Finish
{
    public class CapitalStrategyTermLoan : CapitalStrategy
    {
        public override double Capital(Loan loan)
        {
            return loan.Commitment * loan.Duration() * RiskFactor(loan);
        }

        public override double Duration(Loan loan)
        {
            return WeightedAverageDuration(loan);
        }
    }
}
