using System;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Xml.Serialization;
using CityReader.Models;

namespace CityReader.Services
{
    public class CityReaderService
    {
        public IObservable<string[]> GetCities(string fullPath)
        {
            var ser = new XmlSerializer(typeof(Root));
            var myFileStream = new FileStream(fullPath, FileMode.Open);
            var root = (Root) ser.Deserialize(myFileStream);
            if (root == null)
            {
                throw new ArgumentNullException(nameof(root));
            }

            var cities = root.World.Continent
                .SelectMany(continent => continent.Country)
                .SelectMany(country => country.City);

            return Observable.Return(cities.ToArray());
        }
    }
}