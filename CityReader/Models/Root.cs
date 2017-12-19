using System.Collections.Generic;
using System.Xml.Serialization;

namespace CityReader.Models
{
    [XmlRoot(ElementName = "country")]
    public class Country
    {
        [XmlElement(ElementName = "city")]
        public List<string> City { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "continent")]
    public class Continent
    {
        [XmlElement(ElementName = "country")]
        public List<Country> Country { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "world")]
    public class World
    {
        [XmlElement(ElementName = "continent")]
        public List<Continent> Continent { get; set; }
    }

    [XmlRoot(ElementName = "root")]
    public class Root
    {
        [XmlElement(ElementName = "world")]
        public World World { get; set; }
    }

}
