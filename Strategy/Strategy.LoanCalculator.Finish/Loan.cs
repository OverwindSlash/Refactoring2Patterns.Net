using System;
using System.Collections.Generic;

namespace Strategy.LoanCalculator.Finish
{
    // 银行贷款的资金计算类，可以处理定期、循环、建议信用额度三种类型的贷款额度计算
    // 代码中包含了数量可观的用来执行资金计算的条件逻辑，我们将通过 Strategy 模式将其重构
    public class Loan
    {
        /*private const int MILLIS_PER_DAY = 86400000;
        private const int DAYS_PER_YEAR = 365;*/

        private Date _expiry;
        private Date _maturity;
        private Date _today;
        private Date _start;

        private double _commitment;
        private readonly double _outstanding;
        private double _riskRating;
        private double _unusedPercentage;

        private List<Payment> payments = new List<Payment>();
        private CapitalStrategy _capitalStrategy;

        private Loan(double commitment, double outstanding, Date start, Date expiry, Date maturity, int riskRating, CapitalStrategy capitalStrategy)
        {
            this.Commitment = commitment;
            this._outstanding = outstanding;
            this.Start = start;
            this.Expiry = expiry;
            this.Maturity = maturity;
            this.RiskRating = riskRating;
            this.Today = null;
            _capitalStrategy = capitalStrategy;
        }

        public Date Expiry
        {
            get => _expiry;
            set => _expiry = value;
        }

        public Date Maturity
        {
            get => _maturity;
            set => _maturity = value;
        }

        public double Commitment
        {
            get => _commitment;
            set => _commitment = value;
        }

        public double RiskRating
        {
            get => _riskRating;
            set => _riskRating = value;
        }

        public List<Payment> Payments
        {
            get => payments;
            set => payments = value;
        }

        public Date Today
        {
            get => _today;
            set => _today = value;
        }

        public Date Start
        {
            get => _start;
            set => _start = value;
        }

        public static Loan NewTermLoan(double commitment, Date start, Date maturity, int riskRating)
        {
            return new Loan(commitment, commitment, start, null, maturity, riskRating, new CapitalStrategyTermLoan());
        }

        public static Loan NewAdvisedLine(double commitment, Date start, Date expiry, int riskRating)
        {
            if (riskRating > 3) return null;
            Loan advisedLine =
                new Loan(commitment, 2, start, expiry, null, riskRating, new CapitalStrategyAdvisedLine());
            advisedLine.SetUnusedPercentage(0.1);
            return advisedLine;
        }

        public static Loan NewRevolver(double commitment, Date start, Date expiry, int riskRating)
        {
            Loan revoler = new Loan(commitment, 0, start, expiry, null, riskRating, new CapitalStrategyRevolver());
            revoler.SetUnusedPercentage(1.0);
            return revoler;
        }

        public void Payment(double amount, Date date)
        {
            Payments.Add(new Payment(amount, date));
        }

        public double Capital()
        {
            /*// 有效期为空，到期日不为空，此为定期贷款
            if (Expiry == null && Maturity != null)
                return Commitment * Duration() * RiskFactor();

            // 有效期不为空，到期日为空，此为循环贷款或建议信用额度贷款
            if (Expiry != null && Maturity == null)
            {
                // 信用额度贷款
                if (Math.Abs(GetUnusedPercentage() - 1.0) > 0.001)
                    return Commitment * GetUnusedPercentage() * Duration() * RiskFactor();
                else
                    // 循环贷款
                    return (OutstandingRiskAmount() * Duration() * RiskFactor())
                        + (UnusedRiskAmount() * Duration() * UnusedRiskFactor());
            }

            return 0.0;*/

            return _capitalStrategy.Capital(this);
        }

        public double Duration()
        {
            /*// 有效期为空，到期日不为空，此为定期贷款
            if (Expiry == null && Maturity != null)
                return WeightedAverageDuration();
            // 有效期不为空，到期日为空，此为循环贷款或建议信用额度贷款
            else if (Expiry != null && Maturity == null)
                return YearsTo(Expiry);
            return 0.0;*/

            return _capitalStrategy.Duration(this);
        }

        /*private double WeightedAverageDuration()
        {
            double duration = 0.0;
            double weightedAverage = 0.0;
            double sumOfPayments = 0.0;
            foreach (Payment payment in Payments)
            {
                sumOfPayments += payment.GetAmount();
                weightedAverage += YearsTo(payment.GetDate()) * payment.GetAmount();
            }
            if (Math.Abs(Commitment) > 0.001)
                duration = weightedAverage / sumOfPayments;
            return duration;
        }

        private double YearsTo(Date endDate)
        {
            Date beginDate = (Today == null ? Start : Today);
            return ((endDate.GetTime() - beginDate.GetTime()) / MILLIS_PER_DAY) / DAYS_PER_YEAR;
        }*/

        /*private double RiskFactor()
        {
            return Finish.RiskFactor.GetFactors().ForRating(RiskRating);
        }*/

        internal double GetUnusedPercentage()
        {
            return _unusedPercentage;
        }

        private void SetUnusedPercentage(double unusedPercentage)
        {
            this._unusedPercentage = unusedPercentage;
        }

        internal double UnusedRiskAmount()
        {
            return (Commitment - _outstanding);
        }

        internal double OutstandingRiskAmount()
        {
            return _outstanding;
        }

        /*private double UnusedRiskFactor()
        {
            return UnusedRiskFactors.GetFactors().ForRating(RiskRating);
        }*/
    }
}
