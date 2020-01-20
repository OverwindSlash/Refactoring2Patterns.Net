namespace Strategy.LoanCalculator.Finish
{
    public class Payment
    {
        private readonly double _amount;
        private readonly Date _date;

        public Payment(double getAmount, Date getDate)
        {
            this._amount = getAmount;
            this._date = getDate;
        }

        internal double GetAmount()
        {
            return _amount;
        }

        internal Date GetDate()
        {
            return _date;
        }
    }
}
