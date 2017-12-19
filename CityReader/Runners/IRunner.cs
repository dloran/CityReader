using System;
using System.Reactive;

namespace CityReader.Runners
{
    public interface IRunner
    {
        IObservable<Unit> Run();
    }
}
