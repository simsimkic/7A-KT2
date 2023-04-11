using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using BookingApp.Application.UseCases.Guest1;
using BookingApp.Domain.Models.Guest1;
using BookingApp.WPF.ViewModels.Guest1;

namespace BookingApp.WPF.Views.Guest1.MainWinPages
{
    /// <summary>
    /// Interaction logic for ReservationsPage.xaml
    /// </summary>
    public partial class ReservationsPage : Page
    {
        private readonly ReservationsViewModel reservationsViewModel;

        public ReservationsPage(int userId,Frame frame)
        {
            InitializeComponent();
           

            reservationsViewModel = new ReservationsViewModel(userId, frame);
            this.DataContext = reservationsViewModel;
            
           

        }


        private void CancelReservation_Click(object sender, RoutedEventArgs e)
        {
            reservationsViewModel.CancelReservation();
        }


        private void MoveReservation_OnClick(object sender, RoutedEventArgs e)
        {
            reservationsViewModel.MoveReservation();
        }
    }
}
