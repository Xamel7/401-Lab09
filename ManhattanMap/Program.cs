using System.Data.Common;
using System.Text.Json;

namespace ManhattanMap
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string json = File.ReadAllText("C:\\Users\\Student-16\\Documents\\GitHub\\Dev\\401-Lab06\\ManhattanMap\\data.json");
            Console.WriteLine("Read file string");

            FeatureCollection featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json);
            Console.WriteLine("Deserialize the json data");

            Location[] locations = featureCollection.features;
            Console.WriteLine(locations);
            Part1WithLINQ(locations);
        }
        public static void Part1(Location[] items)
        {
            Dictionary<string, int> locationAppearances = new Dictionary<string, int>();    
            for (int i = 0; i < items.Length; i++)
            {
                Location currentLocation = items[i];
                string neighborhood = currentLocation.properties.neighborhood;
                bool neighborhoodAlreadyInDictionary = locationAppearances.ContainsKey(neighborhood);
                if (neighborhoodAlreadyInDictionary == false)
                {
                    locationAppearances.Add(neighborhood, 1);
                }
                else
                {
                    int currentValue = locationAppearances.GetValueOrDefault(neighborhood);
                    int newValue = currentValue + 1;
                    locationAppearances[neighborhood] = newValue;
                }
            }
            foreach (var location in locationAppearances) 
            {
                Console.WriteLine($"{location.Key}: {location.Value}");
            }
        }
        public static void Part1WithLINQ(Location[] items)
        {
            var neighborHoodQuery = from item in items
                                    group item by item.properties.neighborhood into grouped
                                    select new { Key = grouped.Key, Value = grouped.Count() };

            foreach (var location in  neighborHoodQuery)
            {
                Console.WriteLine($"{location.Key}:{location.Value}");
            }
        }
    }
}
