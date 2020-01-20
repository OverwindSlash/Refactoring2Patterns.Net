using System;
using System.Collections.Generic;
using System.Text;

namespace Strategy.LoanCalculator.Finish
{
    public class CapitalStrategyRevolver : CapitalStrategy
    {
        public override double Capital(Loan loan)
        {
            return (loan.OutstandingRiskAmount() * loan.Duration() * RiskFactor(loan))
                   + (loan.UnusedRiskAmount() * loan.Duration() * UnusedRiskFactor(loan));
        }

        public override double Duration(Loan loan)
        {
            return YearsTo(loan.Expiry, loan);
        }
    }
}
