using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Models.Guest1;
using BookingApp.Domain.RepositoryInterfaces.Guest1;

namespace BookingApp.Application.UseCases.Guest1
{
    public class ReservationRequestService
    {
        private readonly IRequestRepository requestRepository;
        private readonly ReservationService reservationService;


        public ReservationRequestService()
        {
            requestRepository = Injector.Injector.CreateInstance<IRequestRepository>();
            reservationService = new ReservationService();
        }

        public List<MoveReservationRequest> GetByUserId(int id)
        {
            List<MoveReservationRequest> requests = requestRepository.GetByUserId(id);

            UpdateReservationsInRequests(requests);

            return requests;
        }

        public void Save(MoveReservationRequest request)
        {
            requestRepository.Save(request);
        }

        private void UpdateReservationsInRequests(List<MoveReservationRequest> requests)
        {
            List<AccommodationReservation> reservations = reservationService.GetAll();

            foreach (MoveReservationRequest request in requests)
            {
                foreach (AccommodationReservation reservation in reservations)
                {
                    if (request.oldReservation.id == reservation.id)
                        request.oldReservation = reservation;
                }
            }

        }

    }
}
