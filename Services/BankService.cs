using System;
using BankingApplication.Classes;
using BankingApplication.Enums;

namespace BankingApplication.Services
{
    public static class BankService
    {
        public static void ShowUserMenu(User user)
        {
            while (true)
            {
                Console.WriteLine($"\nWelcome, {user.Username}!");
                Console.WriteLine("1. Open Account");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Check Balance");
                Console.WriteLine("5. View Statement");
                Console.WriteLine("6. Add Monthly Interest");
                Console.WriteLine("7. Logout");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        OpenAccount(user);
                        break;
                    case "2":
                        Deposit(user);
                        break;
                    case "3":
                        Withdraw(user);
                        break;
                    case "4":
                        CheckBalance(user);
                        break;
                    case "5":
                        ShowStatement(user);
                        break;
                    case "6":
                        AddMonthlyInterest(user);
                        break;
                    case "7":
                        Console.WriteLine("Logging out...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        public static void OpenAccount(User user)
        {
            Console.Write("Enter Account Holder Name: ");
            string? accountHolderName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(accountHolderName))
            {
                Console.WriteLine("Account Holder Name cannot be empty.");
                return;
            }

            Console.Write("Enter Initial Deposit Amount: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal initialDeposit))
            {
                Console.WriteLine("Invalid deposit amount.");
                return;
            }

            Console.WriteLine("Select Account Type: 1. Savings 2. Checking");
            string? accountTypeInput = Console.ReadLine();
            AccountType accountType = accountTypeInput == "1" ? AccountType.Savings : AccountType.Checking;

            string accountNumber = Guid.NewGuid().ToString();
            Account newAccount = new Account(accountNumber, accountHolderName, accountType, initialDeposit);
            user.Accounts.Add(newAccount);

            Console.WriteLine($"Account created successfully! Account Number: {accountNumber}");
        }

        public static void Deposit(User user)
        {
            Console.Write("Enter Account Number: ");
            string? accountNumber = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                Console.WriteLine("Account Number cannot be empty.");
                return;
            }

            Account? account = user.Accounts.Find(a => a.AccountNumber == accountNumber);
            if (account == null)
            {
                Console.WriteLine("Account not found.");
                return;
            }

            Console.Write("Enter Deposit Amount: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine("Invalid amount.");
                return;
            }

            account.Deposit(amount);
            Console.WriteLine($"Deposit successful! New Balance: {account.Balance}");
        }

        public static void Withdraw(User user)
        {
            Console.Write("Enter Account Number: ");
            string? accountNumber = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                Console.WriteLine("Account Number cannot be empty.");
                return;
            }

            Account? account = user.Accounts.Find(a => a.AccountNumber == accountNumber);
            if (account == null)
            {
                Console.WriteLine("Account not found.");
                return;
            }

            Console.Write("Enter Withdrawal Amount: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine("Invalid amount.");
                return;
            }

            if (account.Withdraw(amount))
            {
                Console.WriteLine($"Withdrawal successful! New Balance: {account.Balance}");
            }
            else
            {
                Console.WriteLine("Insufficient balance.");
            }
        }

        public static void CheckBalance(User user)
        {
            Console.Write("Enter Account Number: ");
            string accountNumber = Console.ReadLine();
            Account account = user.Accounts.Find(a => a.AccountNumber == accountNumber);

            if (account != null)
            {
                Console.WriteLine($"Current Balance: {account.Balance}");
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }

        public static void ShowStatement(User user)
        {
            Console.Write("Enter Account Number: ");
            string? accountNumber = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                Console.WriteLine("Account Number cannot be empty.");
                return;
            }

            Account? account = user.Accounts.Find(a => a.AccountNumber == accountNumber);
            if (account != null)
            {
                Console.WriteLine($"\nTransaction History for Account: {accountNumber}");
                Console.WriteLine("Date\t\t\t\tType\t\tAmount");

                foreach (var transaction in account.Transactions)
                {
                    Console.WriteLine($"{transaction.Date}\t{transaction.Type}\t{transaction.Amount:C}");
                }
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }

        public static void AddMonthlyInterest(User user)
        {
            const decimal interestRate = 1.5m; 
            foreach (var account in user.Accounts)
            {
                if (account.Type == AccountType.Savings)
                {
                    
                    if (account.LastInterestDate.AddMonths(1) <= DateTime.Now)
                    {
                        account.AddInterest(interestRate);
                        Console.WriteLine($"Interest added to Account {account.AccountNumber}. New Balance: {account.Balance:C}");
                    }
                    else
                    {
                        Console.WriteLine($"Interest already added this month for Account {account.AccountNumber}.");
                    }
                }
            }
        }
    }
}
