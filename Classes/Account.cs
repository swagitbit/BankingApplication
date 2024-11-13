using BankingApplication.Enums;
using System.Collections.Generic;

namespace BankingApplication.Classes
{
    public class Account
    {
        public string AccountNumber { get; set; }
        public string AccountHolderName { get; set; }
        public AccountType Type { get; set; }
        public decimal Balance { get; private set; }
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
        public DateTime LastInterestDate { get; set; } = DateTime.MinValue;

        public Account(string accountNumber, string accountHolderName, AccountType type, decimal initialDeposit)
        {
            AccountNumber = accountNumber;
            AccountHolderName = accountHolderName;
            Type = type;
            Balance = initialDeposit;
        }

        public void Deposit(decimal amount)
        {
            Balance += amount;
            Transactions.Add(new Transaction(Guid.NewGuid().ToString(), "Deposit", amount));
        }

        public bool Withdraw(decimal amount)
        {
            if (amount <= Balance)
            {
                Balance -= amount;
                Transactions.Add(new Transaction(Guid.NewGuid().ToString(), "Withdrawal", amount));
                return true;
            }
            return false;
        }

        public void AddInterest(decimal interestRate)
        {
            decimal interest = Balance * interestRate / 100;
            Balance += interest;
            Transactions.Add(new Transaction(Guid.NewGuid().ToString(), "Interest", interest));
            LastInterestDate = DateTime.Now;
        }
    }
}
