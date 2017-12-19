using System;

namespace CityReader.Configuration
{
    public class WorkerConfiguration
    {
        public string InputDirectory { get; }

        public WorkerConfiguration(string inputDirectory)
        {
            if (string.IsNullOrWhiteSpace(inputDirectory))
            {
                throw new ArgumentNullException(nameof(inputDirectory));
            }

            InputDirectory = inputDirectory;
        }
    }
}
