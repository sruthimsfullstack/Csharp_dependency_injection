using Microsoft.Extensions.DependencyInjection;
using System;

namespace SimpleBankingApp
{
    public interface IAccountService
    {
        void CreateAccount(string accountHolder);
        void Deposit(decimal amount);
        void Withdraw(decimal amount);
        decimal GetBalance();
    }

    public class AccountService : IAccountService
    {
        private string _accountHolder;
        private decimal _balance;

        public void CreateAccount(string accountHolder)
        {
            _accountHolder = accountHolder;
            _balance = 0;
            Console.WriteLine($"Account created for {_accountHolder}.");
        }

        public void Deposit(decimal amount)
        {
            _balance += amount;
            Console.WriteLine($"Deposited {amount:C}. New balance: {_balance:C}");
        }

        public void Withdraw(decimal amount)
        {
            if (_balance >= amount)
            {
                _balance -= amount;
                Console.WriteLine($"Withdrew {amount:C}. New balance: {_balance:C}");
            }
            else
            {
                Console.WriteLine("Insufficient funds.");
            }
        }

        public decimal GetBalance()
        {
            return _balance;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create a new ServiceCollection
            var serviceCollection = new ServiceCollection();

            // Register services
            serviceCollection.AddTransient<IAccountService, AccountService>();

            // Build the service provider
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // Resolve and use the AccountService
            var accountService = serviceProvider.GetService<IAccountService>();

            // Simulate some operations
            accountService.CreateAccount("John Doe");
            accountService.Deposit(1000);
            accountService.Withdraw(500);

            Console.WriteLine($"Final balance: {accountService.GetBalance():C}");
        }
    }

}