using BookingApp.Functionality;
using BookingApp.Model;
using BookingApp.View;
using BookingApp.View.CustomerTour;
using BookingApp.View.Guide;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Image = BookingApp.Model.Image;

namespace BookingApp
{
    /// <summary>
    /// Interaction logic for CustomerToursWin.xaml
    /// </summary>
    public partial class CustomerToursWin : Window
    {
        public int idUser;
        TourFunctionality tourFunctionality;
        public static int quideId;
      
        public CustomerToursWin(int idUser)
        {
            InitializeComponent();
           
            tourFunctionality = new TourFunctionality(idUser);
            this.DataContext = tourFunctionality;
            this.idUser = idUser;
          
        }
        private void Tour_Booking(object sender, RoutedEventArgs e)
        {
           // Voucher selectedVoucher = (Voucher)dataGrid.SelectedItem;
           // bool used = selectedVoucher.used;
            bool used = false;

            if (dataGrid.SelectedItem != null)
            {
                used = true;
                MessageBox.Show("Along with the selected tour, we inform you that you have also selected a voucher.");
            }
            else
            {
                used = false;
            }


            if (listView.SelectedItem != null)
            {

                TourAppointment selectedTour = (TourAppointment)listView.SelectedItem;

                int idAppointment = selectedTour.id;
                string name = selectedTour.tour.name;
                string locationCity = selectedTour.tour.Location.city;
                string locationState = selectedTour.tour.Location.country;
                DateTime startTime = selectedTour.startTime;
                string description = selectedTour.tour.description;
                string language = selectedTour.tour.language;
                int maxGuests = selectedTour.tour.maxGuests;
                double duration = selectedTour.tour.duration;
                
                CustomerToursMakeReservation makeReservation = new CustomerToursMakeReservation(idUser, idAppointment, name, locationCity, locationState, startTime, description, language, maxGuests, duration, selectedTour, used);
                makeReservation.Show();
                this.Close();
            }

            else
            {
                MessageBox.Show("Please select the tour from the ListView.");

            }
        }

        private void Live_Tour(object sender, RoutedEventArgs e)
        {
            LiveTourWin liveTourWin = new LiveTourWin(quideId);
            var tourPoints = liveTourWin.TourPoints;
            CustomerTourLiveWin customerTourLiveWin = new CustomerTourLiveWin(tourPoints);
            customerTourLiveWin.Show();
        }

        private void Feedback(object sender, RoutedEventArgs e)
        {
            CustomerTourFeedbackWin feedbackWin = new CustomerTourFeedbackWin();
            feedbackWin.Show();
        }        
    }
}
