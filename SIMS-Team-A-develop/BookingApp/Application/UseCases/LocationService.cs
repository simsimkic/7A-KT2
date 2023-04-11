using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Injector;


namespace BookingApp.Application.UseCases
{
    public class LocationService
    {
        private ILocationRepository locationRepository;

        public LocationService()
        {
            locationRepository = Injector.Injector.CreateInstance<ILocationRepository>();
        }

        public Location Save(Location location)
        {
            return locationRepository.Save(location);
        }

        public int GetIdByCountryAndCity(string city, string country)
        {
            return locationRepository.GetIdByCountyAndCity(city, country);
        }

        public List<Location> GetAll()
        {
            return locationRepository.GetAll();
        }



    }
}
