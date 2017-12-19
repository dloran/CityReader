using System;
using Microsoft.Extensions.Configuration;

namespace CityReader.Configuration
{
    public static class WorkerConfigurationExtensions
    {
        public static WorkerConfiguration GetWorkerConfiguration(this IConfiguration configuration)
        {
            var inputDirectory = configuration.GetSection("InputDirectory").Value;
            Console.WriteLine($"InputDirectory: {inputDirectory}");

            return new WorkerConfiguration(inputDirectory);
        }    
    }
}
