using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using BookingApp.Application.UseCases.Guest1;
using BookingApp.Domain.Models.Guest1;
using BookingApp.WPF.Views.Guest1;

namespace BookingApp.WPF.ViewModels.Guest1
{
    public class ReservationsViewModel : INotifyPropertyChanged
    {

        private ReservationService reservationService;
        public List<AccommodationReservation> reservations { get; set; }

        public AccommodationReservation selectedReservation { get; set; }

        private int userId;

        public ReservationsViewModel(int userId,Frame frame)
        {

            this.userId = userId;

            InitializeRepositoriesAndLists();

            frame.ContentRendered += (s, e) =>
            {
                reservations = reservationService.GetByUserId(userId);
                OnPropertyChanged();
            };

        }

        private void InitializeRepositoriesAndLists()
        {
            reservationService = new ReservationService();
            reservations = reservationService.GetByUserId(userId);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void CancelReservation()
        {
            if (selectedReservation == null) return;

            var result = MessageBox.Show("Confirm reservation cancellation", "Cancel reservation!", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    reservationService.Remove(selectedReservation);
                    reservations = reservationService.GetByUserId(userId);
                    OnPropertyChanged();
                    MessageBox.Show("Reservation canceled!");
                    break;
                case MessageBoxResult.No:
                    break;

            }
        }

        public void MoveReservation()
        {
            if(selectedReservation == null) return;

            var moveReservationWin = new MoveReservationWin(selectedReservation);
            moveReservationWin.ShowDialog();

        }



    }



    

}
