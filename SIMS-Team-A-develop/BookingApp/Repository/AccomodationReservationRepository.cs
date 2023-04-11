using BookingApp.Model;
using InitialProject.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repository
{
    public class ReservationRepository
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

        public List<AccommodationReservation> GetAll()
        {
            reservations = serializer.FromCSV(filePath);
            UpdateAccomodationsInReservations();

            return reservations;
        }

        public List<AccommodationReservation> GetByAccomodationId(int id) 
        {
            reservations = serializer.FromCSV(filePath);
            UpdateAccomodationsInReservations();
            var filteredReservations = reservations.Where(x => x.accommodation.id == id);

            return filteredReservations.ToList();
        }

        public List<AccommodationReservation> GetByUserId(int id)
        {
            reservations = serializer.FromCSV(filePath);
            UpdateAccomodationsInReservations();
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

        private void UpdateAccomodationsInReservations() 
        {
            AccomodationRepository accomodationRepository = new AccomodationRepository();
            List<Accommodation> accomodations = accomodationRepository.GetAll();

            foreach(AccommodationReservation reservation in reservations)
            {
                foreach(Accommodation accomodation in accomodations)
                {
                    if (reservation.accommodation.id == accomodation.id)
                        reservation.accommodation = accomodation;
                }
            }
        }

    }
}
