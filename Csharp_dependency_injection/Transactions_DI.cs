using System;


// Interface representing a transaction

namespace TransactionsNamespace
{
    public interface ITransaction
    {
        void Execute();
    }

    // Implementation of a simple transaction
    public class SimpleTransaction : ITransaction
    {
        public void Execute()
        {
            Console.WriteLine("Executing a simple transaction.");
        }
    }

    // Implementation of a complex transaction
    public class ComplexTransaction : ITransaction
    {
        public void Execute()
        {
            Console.WriteLine("Executing a complex transaction.");
        }
    }

    // Service that depends on ITransaction through dependency injection
    public class TransactionProcessor
    {
        private readonly ITransaction _transaction;

        // Constructor injection . here transaction reference will be decide while caling the function
        // wheateher i.e Complex transaction or Simple Transaction.
        public TransactionProcessor(ITransaction transaction)
        {
            _transaction = transaction;
        }

        public void ProcessTransaction()
        {
            Console.WriteLine("Processing transaction...");

            //Focus point is here.
            _transaction.Execute();
            Console.WriteLine("Transaction processed successfully.");
        }
    }

    class Program
    {
        static void Main()
        {
            // Setting up Dependency Injection manually. Please focus here the way how we are reference 
            // class with interface.
            ITransaction simpleTransaction = new SimpleTransaction();
            TransactionProcessor simpleProcessor = new TransactionProcessor(simpleTransaction);

            // Using the simple processor
            simpleProcessor.ProcessTransaction();

            // Setting up Dependency Injection manually for a different type of transaction
            ITransaction complexTransaction = new ComplexTransaction();
            TransactionProcessor complexProcessor = new TransactionProcessor(complexTransaction);

            // Using the complex processor
            complexProcessor.ProcessTransaction();

            Console.ReadLine();
        }
    }
}


#region Explanation

//ITransaction is an interface representing a transaction.
//SimpleTransaction and ComplexTransaction are concrete implementations of ITransaction.
//TransactionProcessor is a service that depends on an ITransaction through constructor injection.

//By using dependency injection, you can easily switch between different types of transactions without modifying the TransactionProcessor class. This flexibility allows you to adapt your application to changing requirements and makes testing more straightforward.

//In a real-world scenario, the dependency injection might be handled by a framework like ASP.NET Core's built-in dependency injection container, where dependencies are automatically resolved and injected into classes. This promotes better separation of concerns and maintainability in large-scale applications.

#endregion
