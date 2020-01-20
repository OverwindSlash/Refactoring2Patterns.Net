using System;
using System.Collections.Generic;
using System.Text;

namespace Strategy.LoanCalculator.Finish
{
    public class CapitalStrategyAdvisedLine : CapitalStrategy
    {
        public override double Capital(Loan loan)
        {
            return loan.Commitment * loan.GetUnusedPercentage() * loan.Duration() * RiskFactor(loan);
        }

        public override double Duration(Loan loan)
        {
            return YearsTo(loan.Expiry, loan);
        }
    }
}
