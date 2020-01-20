using System;
using System.Collections.Generic;

namespace Strategy.LoanCalculator.Start
{
    // 银行贷款的资金计算类，可以处理定期、循环、建议信用额度三种类型的贷款额度计算
    // 代码中包含了数量可观的用来执行资金计算的条件逻辑，我们将通过 Strategy 模式将其重构
    public class Loan
    {
        private const int MILLIS_PER_DAY = 86400000;
        private const int DAYS_PER_YEAR = 365;

        private readonly Date _expiry;
        private readonly Date _maturity;
        private readonly Date _today;
        private readonly Date _start;

        private readonly double _commitment;
        private readonly double _outstanding;
        private readonly double _riskRating;
        private double _unusedPercentage;

        private List<Payment> payments = new List<Payment>();

        private Loan(double commitment, double outstanding, Date start, Date expiry, Date maturity, int riskRating)
        {
            this._commitment = commitment;
            this._outstanding = outstanding;
            this._start = start;
            this._expiry = expiry;
            this._maturity = maturity;
            this._riskRating = riskRating;
            this._today = null;
        }

        public static Loan NewTermLoan(double commitment, Date start, Date maturity, int riskRating)
        {
            return new Loan(commitment, commitment, start, null, maturity, riskRating);
        }
        public static Loan NewAdvisedLine(double commitment, Date start, Date expiry, int riskRating)
        {
            if (riskRating > 3) return null;
            Loan advisedLine =
                new Loan(commitment, 2, start, expiry, null, riskRating);
            advisedLine.SetUnusedPercentage(0.1);
            return advisedLine;
        }

        public static Loan NewRevolver(double commitment, Date start, Date expiry, int riskRating)
        {
            Loan revoler = new Loan(commitment, 0, start, expiry, null, riskRating);
            revoler.SetUnusedPercentage(1.0);
            return revoler;
        }

        public void Payment(double amount, Date date)
        {
            payments.Add(new Payment(amount, date));
        }

        public double Capital()
        {
            // 有效期为空，到期日不为空，此为定期贷款
            if (_expiry == null && _maturity != null)
                return _commitment * Duration() * RiskFactor();

            // 有效期不为空，到期日为空，此为循环贷款或建议信用额度贷款
            if (_expiry != null && _maturity == null)
            {
                // 信用额度贷款
                if (Math.Abs(GetUnusedPercentage() - 1.0) > 0.001)
                    return _commitment * GetUnusedPercentage() * Duration() * RiskFactor();
                else
                    // 循环贷款
                    return (OutstandingRiskAmount() * Duration() * RiskFactor())
                        + (UnusedRiskAmount() * Duration() * UnusedRiskFactor());
            }

            return 0.0;
        }

        public double Duration()
        {
            // 有效期为空，到期日不为空，此为定期贷款
            if (_expiry == null && _maturity != null)
                return WeightedAverageDuration();
            // 有效期不为空，到期日为空，此为循环贷款或建议信用额度贷款
            else if (_expiry != null && _maturity == null)
                return YearsTo(_expiry);
            return 0.0;
        }

        private double WeightedAverageDuration()
        {
            double duration = 0.0;
            double weightedAverage = 0.0;
            double sumOfPayments = 0.0;
            foreach (Payment payment in payments)
            {
                sumOfPayments += payment.GetAmount();
                weightedAverage += YearsTo(payment.GetDate()) * payment.GetAmount();
            }
            if (Math.Abs(_commitment) > 0.001)
                duration = weightedAverage / sumOfPayments;
            return duration;
        }

        private double YearsTo(Date endDate)
        {
            Date beginDate = (_today == null ? _start : _today);
            return ((endDate.GetTime() - beginDate.GetTime()) / MILLIS_PER_DAY) / DAYS_PER_YEAR;
        }

        private double RiskFactor()
        {
            return Start.RiskFactor.GetFactors().ForRating(_riskRating);
        }

        private double GetUnusedPercentage()
        {
            return _unusedPercentage;
        }

        private void SetUnusedPercentage(double unusedPercentage)
        {
            this._unusedPercentage = unusedPercentage;
        }

        private double UnusedRiskAmount()
        {
            return (_commitment - _outstanding);
        }

        private double OutstandingRiskAmount()
        {
            return _outstanding;
        }

        private double UnusedRiskFactor()
        {
            return UnusedRiskFactors.GetFactors().ForRating(_riskRating);
        }
    }
}
