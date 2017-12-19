using System;
using System.Reactive;
using System.Reactive.Linq;
using CityReader.Services;

namespace CityReader.Runners
{
    public class Runner : IRunner
    {
        private readonly FileWatcherService _fileWatcherService;

        private readonly CityReaderService _cityRepository;

        public Runner(FileWatcherService fileWatcherService, CityReaderService cityRepository)
        {
            if (fileWatcherService == null)
            {
                throw new ArgumentNullException(nameof(fileWatcherService));
            }
            if (cityRepository == null)
            {
                throw new ArgumentNullException(nameof(cityRepository));
            }

            _fileWatcherService = fileWatcherService;
            _cityRepository = cityRepository;
        }

        public IObservable<Unit> Run()
        {            
            _fileWatcherService.WatchFiles()
                .SelectMany(fullPath => _cityRepository.GetCities(fullPath))
                .Do(cities => PrintArray(cities))                
                .Wait();

            return Observable.Return(Unit.Default);
        }

        private static void PrintArray(string[] input)
        {
            foreach (var item in input)
            {
                Console.WriteLine(item);
            }
        }
   }
}
