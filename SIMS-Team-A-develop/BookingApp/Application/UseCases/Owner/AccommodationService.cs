using System.Collections.Generic;
using System.Linq;
using BookingApp.Repository.Owner;
using BookingApp.Repository.Mutual;
using BookingApp.Domain.Models.Owner;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces.Owner;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Injector;
using BookingApp.WPF.Views.Guest1;


namespace BookingApp.Application.UseCases.Owner
{
    public class AccommodationService
    {

        private readonly IAccommodationRepository accommodationRepository;
        private readonly ILocationRepository locationRepository;
        private readonly IImageRepository imageRepository;




        public AccommodationService()
        {
            accommodationRepository = Injector.Injector.CreateInstance<IAccommodationRepository>();   
            locationRepository = Injector.Injector.CreateInstance<ILocationRepository>();  
            imageRepository = Injector.Injector.CreateInstance<IImageRepository>();    
        }

        public List<Accommodation> GetAll()
        {
            List<Accommodation> accommodations = accommodationRepository.GetAll();

            UpdateAccommodationLocation(accommodations);
            UpdateAccomodationImages(accommodations);

            return accommodations;
        }

        public void Save(Accommodation accommodation)
        {
            accommodationRepository.Save(accommodation);
        }

        
        private void UpdateAccommodationLocation(List<Accommodation> accomodations)  //za objekat Location location u Accomodation.cs
        {
            List<Location> locations = locationRepository.GetAll();

            foreach (Accommodation accomodation in accomodations)
            {
                foreach (Location location in locations)
                {

                    if (location.idLocation == accomodation.idLocation)
                        accomodation.location = location;

                }
            }
        }

        private void UpdateAccomodationImages(List<Accommodation> accomodations)
        {
            List<Image> images = imageRepository.getAccomodationImg();


            foreach (Accommodation accomodation in accomodations)
            {
                foreach (Image image in images)
                {
                    if (image.resourceId == accomodation.id)
                    {
                        accomodation.accommodationImages.Add(image);
                    }
                }
                if (accomodation.accommodationImages.Count > 0)
                    accomodation.firstImg = accomodation.accommodationImages.First();

            }
        }

    }
}
