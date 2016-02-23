using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace PhaseTicket.Models.Dicts
{
    public class TravelPoint
    {
        public string Code { get; set; }
        public string NameRu { get; set; }
        public string NameEn { get; set; }
        public string CountryCode { get; set; }
        public int Popularity { get; set; }
        public string CountryName { get; set; }
    }

    public class Airport : TravelPoint{}
    public class City : TravelPoint {}
    public class Country : TravelPoint { }
    

    public class AirportsAndCities
    {
        List<City> Cities { get; set; }
        List<Airport> Airports { get; set; } 
        List<Country> Countries { get; set; } 

        public AirportsAndCities(HttpServerUtility server)
        {
            string citiesPath = "C:/Airports/Cities.csv";// = server.MapPath("App_Data/Cities.csv");
            string airportsPath = "C:/Airports/Airports.csv";  //= server.MapPath("App_Data/Airports.csv");
            string countriesPath = "C:/Airports/Countries.csv";  //= server.MapPath("App_Data/Airports.csv");
            var cities = new StreamReader(File.OpenRead(citiesPath), Encoding.UTF8);
            var airports = new StreamReader(File.OpenRead(airportsPath), Encoding.UTF8);
            var countries = new StreamReader(File.OpenRead(countriesPath), Encoding.UTF8);
            
            Airports = new List<Airport>();
            while (!airports.EndOfStream)
            {
                var line = airports.ReadLine();
                var values = line.Split(';');
                var airport = new Airport()
                {
                    Code = values[0],
                    NameRu = values[1],
                    NameEn = values[2],
                    CountryCode = values[4],

                };
                Airports.Add(airport);
            }
            airports.Close();

            Cities = new List<City>();
            while (!cities.EndOfStream)
            {
                var line = cities.ReadLine();
                var values = line.Split(';');
                var city = new City()
                {
                    Code = values[0],
                    Popularity = int.Parse(values[1] == "Popularity" ? "0" : values[1]),
                    NameRu = values[2],
                    NameEn = values[3],
                    CountryCode = values[5],

                };
                Cities.Add(city);
            }
            cities.Close();

            Countries = new List<Country>();
            while (!countries.EndOfStream)
            {
                var line = countries.ReadLine();
                var values = line.Split(';');
                //if (values.Length == 3)
              {
                    var city = new Country()
                    {
                        Code = values[0],
                        NameRu = values[2],
                        NameEn = values[1],
                    };
                    Countries.Add(city);
                }
                
            }
            countries.Close();
        }

        public List<TravelPoint> GetSuggest(string query)
        {
            var ports =
                Airports.Where(
                    c => c.NameEn.ToLower().Contains(query.ToLower()) || c.NameRu.ToLower().Contains(query.ToLower()))
                    .Distinct()
                    .ToList();

            var cities = Cities.Where(
                    c => c.NameEn.ToLower().Contains(query.ToLower()) || c.NameRu.ToLower().Contains(query.ToLower()))
                    .Where(c=>c.CountryCode == "RU")
                    .Distinct()
                    .OrderByDescending(c=>c.Popularity)
                    .ToList();

            foreach (var city in cities.Where(c=>!string.IsNullOrWhiteSpace(c.CountryCode)))
            {
                city.CountryName = Countries.FirstOrDefault(c => c.Code == city.CountryCode).NameRu;
            }

            var total = new List<TravelPoint>();
            //total.AddRange(ports);
            total.AddRange(cities);

            return total;
        }
    }


}