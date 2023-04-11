using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using BookingApp.Application.UseCases.Owner;
using BookingApp.Domain.Models.Guest1;
using BookingApp.Domain.Models.Owner;
using BookingApp.Domain.RepositoryInterfaces.Guest1;
using BookingApp.Domain.RepositoryInterfaces.Owner;
using BookingApp.Repository.Guest1;
using BookingApp.Repository.Owner;
using BookingApp.Injector;
using BookingApp.WPF.Views.Guest1;


namespace BookingApp.Application.UseCases.Guest1
{
    
    public class ReservationService
    {

        private readonly IReservationRepository reservationRepository;
        private readonly AccommodationService accommodationService;



        public ReservationService()
        {
            reservationRepository = Injector.Injector.CreateInstance<IReservationRepository>();
            accommodationService = new AccommodationService();

        }

        public List<AccommodationReservation> GetAll()
        {
            List<AccommodationReservation> reservations = reservationRepository.GetAll();

            UpdateAccommodationsInReservations(reservations);


            return reservations;
        }

        public List<AccommodationReservation> GetByUserId(int id)
        {
            List<AccommodationReservation> reservations = reservationRepository.GetByUserId(id);

            UpdateAccommodationsInReservations(reservations);

            return reservations;
        }

        public List<AccommodationReservation> GetByAccommodationId(int id)
        {
            List<AccommodationReservation> reservations = reservationRepository.GetByAccommodationId(id);

            UpdateAccommodationsInReservations(reservations);


            return reservations;
        }

        public void Save(AccommodationReservation reservation)
        {
            reservationRepository.Save(reservation);
        }

        public void Remove(AccommodationReservation reservation)
        {
            reservationRepository.Remove(reservation);
        }


        private void UpdateAccommodationsInReservations(List<AccommodationReservation> reservations)
        {
           
            List<Accommodation> accomodations = accommodationService.GetAll();

            foreach (AccommodationReservation reservation in reservations)
            {
                foreach (Accommodation accomodation in accomodations)
                {
                    if (reservation.accomodation.id == accomodation.id)
                        reservation.accomodation = accomodation;
                }
            }



        }







    }
}
