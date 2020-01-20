using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Strategy.LoanCalculator.Start.Tests
{
    [TestClass]
    public class LoanCalculatorTest
    {
        [TestMethod]
        public void TestTermLoanSamePayments()
        {
            Date start = new Date(2003, 11, 20);
            Date maturity = new Date(2006, 11, 20);
            Loan termLoan = Loan.NewTermLoan(3000, start, maturity, 1);
            termLoan.Payment(1000.00, new Date(2004, 11, 20));
            termLoan.Payment(1000.00, new Date(2005, 11, 20));
            termLoan.Payment(1000.00, new Date(2006, 11, 20));
            Assert.AreEqual(2.0, termLoan.Duration(), 0.01);
            Assert.AreEqual(210.00, termLoan.Capital(), 0.01);
        }

        [TestMethod]
        public void TestAdvisedLineSamePayments()
        {
            Date start = new Date(2003, 11, 20);
            Date maturity = new Date(2006, 11, 20);
            Loan advisedLine = Loan.NewAdvisedLine(3000, start, maturity, 2);
            advisedLine.Payment(1000.00, new Date(2004, 11, 20));
            advisedLine.Payment(1000.00, new Date(2005, 11, 20));
            advisedLine.Payment(1000.00, new Date(2006, 11, 20));
            Assert.AreEqual(3.0, advisedLine.Duration(), 0.01);
            Assert.AreEqual(31.5, advisedLine.Capital(), 0.01);
        }

        [TestMethod]
        public void TestRevolverSamePayments()
        {
            Date start = new Date(2003, 11, 20);
            Date maturity = new Date(2006, 11, 20);
            Loan advisedLine = Loan.NewRevolver(3000, start, maturity, 0);
            advisedLine.Payment(1000.00, new Date(2004, 11, 20));
            advisedLine.Payment(1000.00, new Date(2005, 11, 20));
            advisedLine.Payment(1000.00, new Date(2006, 11, 20));
            Assert.AreEqual(3.0, advisedLine.Duration(), 0.01);
            Assert.AreEqual(315, advisedLine.Capital(), 0.01);
        }
    }
}
