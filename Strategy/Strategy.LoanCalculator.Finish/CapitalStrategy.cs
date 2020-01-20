using System;
using System.Collections.Generic;
using System.Text;

namespace Strategy.LoanCalculator.Finish
{
    public abstract class CapitalStrategy
    {
        private const int MILLIS_PER_DAY = 86400000;
        private const int DAYS_PER_YEAR = 365;

        public abstract double Capital(Loan loan);

        protected double RiskFactor(Loan loan)
        {
            return Finish.RiskFactor.GetFactors().ForRating(loan.RiskRating);
        }

        protected double UnusedRiskFactor(Loan loan)
        {
            return UnusedRiskFactors.GetFactors().ForRating(loan.RiskRating);
        }

        public abstract double Duration(Loan loan);

        protected double WeightedAverageDuration(Loan loan)
        {
            double duration = 0.0;
            double weightedAverage = 0.0;
            double sumOfPayments = 0.0;
            foreach (Payment payment in loan.Payments)
            {
                sumOfPayments += payment.GetAmount();
                weightedAverage += YearsTo(payment.GetDate(), loan) * payment.GetAmount();
            }
            if (Math.Abs(loan.Commitment) > 0.001)
                duration = weightedAverage / sumOfPayments;
            return duration;
        }

        protected double YearsTo(Date endDate, Loan loan)
        {
            Date beginDate = (loan.Today == null ? loan.Start : loan.Today);
            return ((endDate.GetTime() - beginDate.GetTime()) / MILLIS_PER_DAY) / DAYS_PER_YEAR;
        }
    }
}
