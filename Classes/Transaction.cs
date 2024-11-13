using System;

namespace BankingApplication.Classes
{
    public class Transaction
    {
        public string TransactionID { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }

        public Transaction(string transactionID, string type, decimal amount)
        {
            TransactionID = transactionID;
            Date = DateTime.Now;
            Type = type;
            Amount = amount;
        }
    }
}
