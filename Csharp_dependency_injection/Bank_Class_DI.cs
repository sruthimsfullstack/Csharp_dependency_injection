// Interface representing a bank account
using System;


namespace BankDINamespace
{
    public interface IAccount
    {
        decimal GetBalance();
    }

    // Implementation of the bank account
    public class BankAccount : IAccount
    {
        private decimal _balance;

        public BankAccount(decimal balance)
        {
            _balance = balance;
        }

        public decimal GetBalance()
        {
            return _balance;
        }
    }

    // Service that depends on IAccount through dependency injection
    public class BankService
    {
        private readonly IAccount _account;

        // Constructor injection
        public BankService(IAccount account)
        {
            _account = account;
        }

        public void DisplayBalance()
        {
            // Here i am not dependening on class for creating the object rather than i am taking the 
            // interface for calling the method. This is called loosed coupled system.


            //In dependencey interface role is very importannt.
            decimal balance = _account.GetBalance();
            Console.WriteLine($"Account Balance : {balance}");
        }
    }

    class Program
    {
        static void Main()
        {
            // Setting up Dependency Injection manually
            IAccount account = new BankAccount(1000);

            BankService bankService = new BankService(account);

            // Using the service
            bankService.DisplayBalance();
            //bankService.

            Console.ReadLine();
        }
    }
}


#region Explanation
//IAccount is an interface representing a bank account.
//BankAccount is a concrete implementation of IAccount.
//BankService is a service that depends on an IAccount through constructor injection.

//This way, the BankService doesn't need to create its own IAccount instance. Instead, it's provided through the constructor, promoting better decoupling and making the code more testable and maintainable.
#endregion
