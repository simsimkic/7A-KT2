using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Application.UseCases.Guest1;
using BookingApp.Domain.Models.Guest1;

namespace BookingApp.WPF.ViewModels.Guest1
{
    public class ReservationRequestsViewModel
    {
        private ReservationRequestService requestService;
        public List<MoveReservationRequest> requests { get; set; }
        public MoveReservationRequest selectedRequest { get; set; }

        private readonly int userId;

        public ReservationRequestsViewModel(int userId)
        {   
            this.userId = userId;

            requestService = new ReservationRequestService();
            requests = requestService.GetByUserId(userId);
            selectedRequest = new MoveReservationRequest();

            
        }


    }
}
