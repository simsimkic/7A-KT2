using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using BookingApp.Application.UseCases.Guest1;
using BookingApp.Domain.Models.Guest1;

namespace BookingApp.WPF.ViewModels.Guest1
{
    public class MoveReservationViewModel: INotifyPropertyChanged
    {

        private ReservationRequestService requestService;
        private ValidDatesService datesService;
        public AccommodationReservation reservation { get; set; }


        public DateTime? newStartDate { get; set; }
        public DateTime? newEndDate { get; set; }

        public DateTime? NewStartDate
        {
            get
            {
                return newStartDate;
            }

            set
            {
                newStartDate = value;
                newEndDate = null;
                OnPropertyChanged();
            }

        }


        public MoveReservationViewModel(AccommodationReservation reservation)
        {

            requestService = new ReservationRequestService();
            datesService = new ValidDatesService();
            this.reservation = reservation;


            

        }

        public void GetValidDates(DatePicker datePicker)
        {
             

            datesService.BlackOutDates(datePicker, reservation.accomodation.id);
        }

        public void GetValidEndDates(DatePicker datePicker, DateTime startDate)
        {
            datePicker.BlackoutDates.Clear();
            GetValidDates(datePicker);
            datesService.BlackOutForbiddenEndDates(datePicker, startDate, reservation);
        }

        public void MoveReservation(Window window)
        {
            if (newStartDate == null || newEndDate == null) return;

            var request = new MoveReservationRequest(reservation.idGuest,reservation, newStartDate.Value, newEndDate.Value);
            requestService.Save(request);
            
            window.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
