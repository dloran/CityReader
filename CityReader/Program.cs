using System;
using System.IO;
using System.Reactive.Linq;
using CityReader.Configuration;
using CityReader.Runners;
using CityReader.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CityReader
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Listening...");
            
            Observable.Return(CreateServiceProvider(CreateConfiguration()))
                .Select(serviceProvider => serviceProvider.GetService<IRunner>())
                .SelectMany(runner => runner.Run())
                .Wait();
        }

        private static IConfiguration CreateConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .Build();
        }

        private static IServiceProvider CreateServiceProvider(IConfiguration configuration)
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection
                .AddSingleton(configuration.GetWorkerConfiguration())
                .AddSingleton<FileWatcherService>()
                .AddSingleton<CityReaderService>()
                .AddSingleton<IRunner, Runner>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}
