using System;
using System.Collections.Generic;
using System.Windows;
using BookingApp.Domain.Models.Owner;
using BookingApp.Domain.Models.Guest1;
using BookingApp.Application.UseCases.Guest1;
using static BookingApp.WPF.Views.Guest1.ReservationWin;


namespace BookingApp.WPF.Views.Guest1
{
    /// <summary>
    /// Interaction logic for FreeDatesWin.xaml
    /// </summary>
    public partial class FreeDatesWin : Window
    {

        public List<DateSpan> freeDates { get; set; }
        public DateSpan selectedDate { get; set; }

        private Accommodation accommodation { get; set; }

        private ReservationService reservationService;
        private int userId;
        private int guestNumber;
        private Window reservationWin;

        public FreeDatesWin(List<DateSpan> freeDates, Accommodation accommodation, int userId, int guestNumber, Window reservationWin)
        {
            InitializeComponent();
            this.DataContext = this;


            this.freeDates = freeDates;

            selectedDate = new DateSpan();

            this.accommodation = accommodation;
            this.userId = userId;
            this.guestNumber = guestNumber;

            this.reservationWin = reservationWin;


            reservationService = new ReservationService();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Reserve_Click(object sender, RoutedEventArgs e)
        {
            if (selectedDate.Start != DateTime.MinValue)
            {
                var reservation = new AccommodationReservation(selectedDate.Start, selectedDate.End, accommodation, userId, guestNumber, false); 
                reservationService.Save(reservation);
                MessageBox.Show("Reservation made successfully!");
                this.Close();
                reservationWin.Close();
            }
        }
    }
}
