using BookingApp.Domain.Models.Guest1;
using InitialProject.Serializer;
using System.Collections.Generic;
using System.Linq;
using BookingApp.Domain.RepositoryInterfaces.Guest1;

namespace BookingApp.Repository.Guest1
{
    public class ReservationRepository:IReservationRepository
    {
        private const string filePath = "../../../Resources/Data/accommodationReservations.csv";

        private readonly Serializer<AccommodationReservation> serializer;

        private List<AccommodationReservation> reservations;


        public ReservationRepository() 
        {
            serializer = new Serializer<AccommodationReservation>();
            reservations = serializer.FromCSV(filePath);
        }

        public AccommodationReservation Save(AccommodationReservation reservation)
        {
            reservation.id = NextId();
            reservations = serializer.FromCSV(filePath);
            reservations.Add(reservation);
            serializer.ToCSV(filePath, reservations);
            
            return reservation; 
        }

        public void Remove(AccommodationReservation reservation)
        {
            reservations = serializer.FromCSV(filePath);
            var deletionReservation = reservations.Find(x=>x.id==reservation.id);
            reservations.Remove(deletionReservation);
            serializer.ToCSV(filePath,reservations);
        }

        public List<AccommodationReservation> GetAll()
        {
            reservations = serializer.FromCSV(filePath);

            return reservations;
        }
        
        public List<AccommodationReservation> GetByAccommodationId(int id) 
        {
            reservations = serializer.FromCSV(filePath);
            var filteredReservations = reservations.Where(x => x.accomodation.id == id);

            return filteredReservations.ToList();
        }

        public List<AccommodationReservation> GetByUserId(int id)
        {
            reservations = serializer.FromCSV(filePath);
            var filteredReservations = reservations.Where(x => x.idGuest == id);

            return filteredReservations.ToList();
        }

        private int NextId()
        {
            reservations = serializer.FromCSV(filePath);
            if (reservations.Count < 1)
                return 1;

            return reservations.Max(x => x.id) + 1;
        }

        

    }
}
