using System;
using BankingApplication.Services;

namespace BankingApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Banking Application!");
            ShowMainMenu();
        }

        static void ShowMainMenu()
        {
            while (true)
            {
                Console.WriteLine("\nMain Menu:");
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit");
                Console.Write("Select an option: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AuthenticationService.Register();
                        break;
                    case "2":
                        var user = AuthenticationService.Login();
                        if (user != null)
                        {
                            BankService.ShowUserMenu(user);
                        }
                        break;
                    case "3":
                        Console.WriteLine("Thank you for using the Banking Application!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        break;
                }
            }
        }
    }
}
