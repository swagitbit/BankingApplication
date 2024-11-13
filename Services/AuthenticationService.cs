using System;
using System.Collections.Generic;
using BankingApplication.Classes;

namespace BankingApplication.Services
{
    public static class AuthenticationService
    {
        private static List<User> users = new List<User>();

        public static void Register()
        {
            Console.Write("Enter Username: ");
            string? username = Console.ReadLine();
            Console.Write("Enter Password: ");
            string? password = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Username and Password cannot be empty.");
                return;
            }

            users.Add(new User(username, password));
            Console.WriteLine("Registration successful!");
        }

        public static User? Login()
        {
            Console.Write("Enter Username: ");
            string? username = Console.ReadLine();
            Console.Write("Enter Password: ");
            string? password = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Username and Password cannot be empty.");
                return null;
            }

            User? user = users.Find(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                Console.WriteLine("Login successful!");
                return user;
            }
            else
            {
                Console.WriteLine("Invalid credentials.");
                return null;
            }
        }
    }
}
