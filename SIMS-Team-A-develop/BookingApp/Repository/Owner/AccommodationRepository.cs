using BookingApp.Domain.Models.Owner;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using BookingApp.Domain.RepositoryInterfaces.Owner;


namespace BookingApp.Repository.Owner
{
    public class AccommodationRepository:IAccommodationRepository
    {
        public const string filePath = "../../../Resources/Data/accommodations.csv";

        private readonly Serializer<Accommodation> serializer;

        private List<Accommodation> accommodations;

        public AccommodationRepository()
        {
            serializer = new Serializer<Accommodation>();
            accommodations = serializer.FromCSV(filePath);
            foreach (Accommodation a in accommodations)
            {
                Console.WriteLine(a.id);
            }
        }

        public Accommodation GetByName(string name)
        {
            accommodations = serializer.FromCSV(filePath);
            return accommodations.FirstOrDefault(a => a.name == name);
        }

        public Accommodation Save(Accommodation accomodation)
        {
            accomodation.id = NextId();
            accommodations = serializer.FromCSV(filePath);
            accommodations.Add(accomodation);
            serializer.ToCSV(filePath, accommodations);
            return accomodation;
        }

        private int NextId()
        {
            accommodations = serializer.FromCSV(filePath);
            if (accommodations.Count < 1)
            {
                return 1;
            }
            return accommodations.Max(a => a.id) + 1;
        }

        public List<Accommodation> GetAll()
        {
            accommodations = serializer.FromCSV(filePath);
            return accommodations;
        }

        

    }
}
