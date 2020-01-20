using System;

namespace Strategy.LoanCalculator.Start
{
    public class Date
    {
        private readonly DateTime _dateTime;

        public Date(int year, int month, int day)
        {
            this._dateTime = new DateTime(year, month, day);
        }

        public long GetTime()
        {
            return _dateTime.Ticks / 10000;
        }
    }
}
