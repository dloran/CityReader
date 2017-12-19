using System;
using System.IO;
using System.Reactive.Linq;
using CityReader.Configuration;

namespace CityReader.Services
{
    public class FileWatcherService
    {
        private readonly WorkerConfiguration _workerConfiguration;

        public FileWatcherService(WorkerConfiguration workerConfiguration)
        {
            if (workerConfiguration == null)
            {
                throw new ArgumentNullException(nameof(workerConfiguration));
            }

            _workerConfiguration = workerConfiguration;
        }

        public IObservable<string> WatchFiles()
        {
            var inputDirectory = _workerConfiguration.InputDirectory;

            if (!Directory.Exists(inputDirectory))
                Directory.CreateDirectory(inputDirectory);

            var fsw = new FileSystemWatcher
            {
                Path = inputDirectory,
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName,
                EnableRaisingEvents = true
            };

            var fswCreated = Observable.FromEvent<FileSystemEventHandler, FileSystemEventArgs>(handler =>
            {
                return (sender, e) =>
                {
                    Console.WriteLine($"File added: {e.FullPath}");
                    handler(e);
                };
            }, fsHandler => fsw.Created += fsHandler, fsHandler => fsw.Created -= fsHandler);

            return fswCreated.Select(args => args.FullPath);
        }
    }
}
