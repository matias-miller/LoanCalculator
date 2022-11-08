using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LoanCalculator
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        
        public void clearButton_Clicked(object sender, EventArgs ex)
        {
            finalLoanInfo.Text = string.Empty;
            finalLoanInfo.IsVisible = false;

            interestRate.Text = string.Empty;
            loanAmount.Text = string.Empty;
            loanPeriod.Text = string.Empty;

        }

        public void caclculateButton_Clicked(object sender, EventArgs e)
        {
            finalLoanInfo.Text = string.Empty;
            finalLoanInfo.TextColor = Color.White;
            finalLoanInfo.IsVisible = false;

            if ( (loanAmount.Text == String.Empty) | (interestRate.Text == String.Empty) | (loanPeriod.Text == String.Empty))
            {
                finalLoanInfo.Text = "One or more of the entries are empty. Fill out all of the entry fields with numeric (double) values.";
                finalLoanInfo.TextColor = Color.Red;
                finalLoanInfo.IsVisible = true;
                return;
            }

            if ((double.TryParse(loanAmount.Text, out double value)) == false)
            {
                finalLoanInfo.Text = "Fill out form corrently and try again. Entries must be numeric (double) values.";
                finalLoanInfo.TextColor = Color.Red;
                finalLoanInfo.IsVisible = true;
                return;
            }

            if ((double.TryParse(interestRate.Text, out double value1)) == false)
            {
                finalLoanInfo.Text = "Fill out form corrently and try again. Entries must be numeric (double) values.";
                finalLoanInfo.TextColor = Color.Red;
                finalLoanInfo.IsVisible = true;
                return;
            }

            if ((int.TryParse(loanPeriod.Text, out int value2)) == false)
            {
                finalLoanInfo.Text = "Fill out form corrently and try again. Entries must be numeric (double) values.";
                finalLoanInfo.TextColor = Color.Red;
                finalLoanInfo.IsVisible = true;
                return;
            }

            Loan loanInfo = new Loan
            {
                loanAmount = Convert.ToDouble(loanAmount.Text),
                interestRate = Convert.ToDouble(interestRate.Text),
                loanPeriod = Convert.ToInt32(loanPeriod.Text)

            };

            if ( (loanInfo.interestRate < 1) | (loanInfo.loanPeriod < 1) | (loanInfo.loanAmount < 1))
            {
                finalLoanInfo.Text = "One or more of the entires are negative. Enter a positive number in all of the enties to determine to calculate loan information.";
                finalLoanInfo.IsVisible = true;
                finalLoanInfo.TextColor = Color.Red;
                return;
            }

            double interestOwed = (loanInfo.loanAmount * loanInfo.loanPeriod * loanInfo.interestRate) / 100;
            double totalLoanOwed = interestOwed + loanInfo.loanAmount;
            double monthlyPayment = totalLoanOwed / loanInfo.loanPeriod;
            finalLoanInfo.Text = String.Format("If you were to take out a loan of ${0}, you would owe ${1} in interest, and ${2} in total. This would be at the set interest rate of {3}% and you would need to pay ${4} a month for {5} months to pay off your loan.", Math.Round(loanInfo.loanAmount, 2), Math.Round(interestOwed, 2), Math.Round(totalLoanOwed, 2), loanInfo.interestRate, Math.Round(monthlyPayment, 2), loanInfo.loanPeriod);
            finalLoanInfo.IsVisible = true;

        }

    }

}
