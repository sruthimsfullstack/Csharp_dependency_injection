using Microsoft.Extensions.DependencyInjection;
using System;


/** 
 * 
 * We can set up the interface along class by using Dependency injection extension plugin...
 * Without giving the referece like previous examples we can add the interface and its related the class
   into the service collection.


   So that we need to
 * install Microsoft.Extensions.DependencyInjection referece 
 *  Go to Manage Nuget packages. browser and search for 'Microsoft.Extensions.DependencyInjection referece' ;
 *  and then install.
 */

namespace DISettingUpNamespace
{

    // Step 1: Define interfaces for services
    public interface ILoggingService
    {

        void Log(string message);
    }

    public interface IDataAccessService
    {
        void SaveData(string data);
    }

    public interface IDownloadService
    {
        void fileDownload();
    }

    // Step 2: Implement concrete classes for services
    public class ConsoleLoggingService : ILoggingService
    {
        private readonly string _prefix;
        public ConsoleLoggingService(string prefix)
        {
            _prefix = prefix;
        }

        public void Log(string message)
        {
            Console.WriteLine($"Logging: {message}");
        }
    }

    public class FileDataAccessService : IDataAccessService
    {
        private readonly string _filePath;

        // Constructor with parameter
        public FileDataAccessService(string filePath)
        {
            _filePath = filePath;
        }
        public void SaveData(string data)
        {
            Console.WriteLine($"Saving data to file: {data}");
        }
    }
    public class FileDownloadService : IDownloadService
    {
        private readonly string _filePath;
        private readonly string _fileName;

        // Constructor with parameter
        public FileDownloadService(string filePath, string fileName)
        {
            _filePath = filePath;
            _fileName = fileName;
        }
        public void fileDownload()
        {
            Console.WriteLine($"File has downloaded from: {this._filePath} . and file Name is {this._fileName}");
        }
    }

    // Step 3: Configure and build the service provider
    class Program
    {
        static void Main()
        {


            // Step 3.1: Create a service collection and configure 
            var serviceCollection = new ServiceCollection();

            // Step 3.2: Register services with their implementations
            serviceCollection.AddTransient<ILoggingService, ConsoleLoggingService>();
            serviceCollection.AddTransient<IDataAccessService, FileDataAccessService>();
            serviceCollection.AddTransient<IDownloadService, FileDownloadService>();

            // Step 3.3: Build the service provider
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            // Step 4: Use the services
            UseServices(serviceProvider);
        }

        static void UseServices(IServiceProvider serviceProvider)
        {
            // Step 4.1: Resolve services from the service provider
            ILoggingService loggingService = serviceProvider.GetService<ILoggingService>();
            IDataAccessService dataAccessService = serviceProvider.GetService<IDataAccessService>();
            IDownloadService downloadService = serviceProvider.GetService<IDownloadService>();


            // Step 4.2: Use the services
            loggingService = new ConsoleLoggingService("prefix");
            loggingService.Log("Hello from the software company!");

            dataAccessService = new FileDataAccessService("log.txt");
            dataAccessService.SaveData("Important data");

            downloadService = new FileDownloadService("c drive", "log.txt");
            downloadService.fileDownload();
            // The services are automatically disposed of when the scope is disposed

            Console.ReadLine();
        }
    }
}



#region Explanation

//Explanation of the steps:

//Define Interfaces for Services (Step 1):

//Create interfaces(ILoggingService and IDataAccessService) that represent the services your software company will provide.
//Implement Concrete Classes for Services (Step 2):

//Implement classes(ConsoleLoggingService and FileDataAccessService) that fulfill the contracts defined by the interfaces.
//Configure and Build the Service Provider (Step 3):

//Create a ServiceCollection to hold the service registrations.
//Register services and their implementations using AddTransient.In a more complex scenario, you might use AddScoped or AddSingleton depending on the desired lifetime of the service.
//Build the service provider using BuildServiceProvider.
//Use the Services(Step 4):

//In the UseServices method, resolve the services from the service provider using GetService<T>.
//Use the services as needed.
//The services are automatically disposed of when the scope is disposed.

#endregion
