using BookingApp.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BookingApp.Repository
{
    public class AccomodationRepository
    {
        public const string filePath = "../../../Resources/Data/accommodations.csv";

        private readonly Serializer<Accommodation> serializer;

        private List<Accommodation> accomodations;

        public AccomodationRepository()
        {
            serializer = new Serializer<Accommodation>();
            accomodations = serializer.FromCSV(filePath);
            foreach (Accommodation a in accomodations)
            {
                Console.WriteLine(a.id);
            }
        }

        public Accommodation GetByName(string name)
        {
            accomodations = serializer.FromCSV(filePath);
            return accomodations.FirstOrDefault(a => a.name == name);
        }

        public Accommodation Save(Accommodation accomodation)
        {
            accomodation.id = NextId();
            accomodations = serializer.FromCSV(filePath);
            accomodations.Add(accomodation);
            serializer.ToCSV(filePath, accomodations);
            return accomodation;
        }

        public int NextId()
        {
            accomodations = serializer.FromCSV(filePath);
            if (accomodations.Count < 1)
            {
                return 1;
            }
            return accomodations.Max(a => a.id) + 1;
        }

        public List<Accommodation> GetAll()
        {
            accomodations = serializer.FromCSV(filePath);

            UpdateAccodomationLocation();
            UpdateAccomodationImages();

            return accomodations;
        }

        private void UpdateAccodomationLocation()  //za objekat Location location u Accomodation.cs
        {
            LocationRepository locationRepository = new LocationRepository();
            List<Location> locations = locationRepository.GetAll();

            foreach (Accommodation accomodation in accomodations)
            {
                foreach (Location location in locations) 
                {
                
                    if(location.idLocation == accomodation.idLocation) 
                        accomodation.location = location;
                    
                }
            }
        }

        private void UpdateAccomodationImages()
        { 
            ImageRepository imageRepository = new ImageRepository();
            List<Image> images = imageRepository.getAccomodationImg();
 

            foreach (Accommodation accomodation in accomodations)
            {
                foreach(Image image in images)
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
