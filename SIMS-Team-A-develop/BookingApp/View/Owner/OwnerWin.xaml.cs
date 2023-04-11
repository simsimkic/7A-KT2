using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.View.Owner;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace BookingApp.View
{
    public partial class OwnerWin : Window, INotifyPropertyChanged
    {

        public static ObservableCollection<Accommodation> accommodations { get; set; }
        public static ObservableCollection<AccommodationReservation> finishedreservations { get; set; }
        public static List<AccommodationReservation> reservations { get; set; }

        public AccomodationRepository accomodationRepository;
        public ReservationRepository ReservationRepository { get; set; }
        public AccommodationReservation selectedReservation { get; set; }
        public Dictionary<string, List<string>> locations { get; set; }  //za Lokacije

        public static User LoggedUser { get; set; }

        public OwnerWin(User user)
        {
            InitializeComponent();
            this.DataContext = this;
            LoggedUser = user;

            accomodationRepository = new AccomodationRepository();
            ReservationRepository = new ReservationRepository();

            accommodations = new ObservableCollection<Accommodation>(accomodationRepository.GetAll());
            reservations = new List<AccommodationReservation>(ReservationRepository.GetAll());
            finishedreservations = new ObservableCollection<AccommodationReservation>(ZavrseneRez());

        
            locations = new Dictionary<string, List<string>>();
            UpdateLocationMap();
        }

        private List<AccommodationReservation> ZavrseneRez() {

            List<AccommodationReservation> zavrseneLista= new List<AccommodationReservation>();
            foreach (AccommodationReservation a in reservations)
            {
                if (DateTime.Now >= a.endDate && (DateTime.Now - a.endDate.Date).TotalDays <= 5)
                    zavrseneLista.Add(a);
            }
            //ovde metoda a.guest = getGuestById koja vraca celog usera a prosledim mu samo gyestId

            


            return zavrseneLista;
        }
       

       
        private void UpdateLocationMap()  //puni mapu 
        {

            LocationRepository locationRepository = new LocationRepository();

            var locationsFromFile = locationRepository.GetAll();

            
            foreach (var location in locationsFromFile)
            {
                String country = location.country;

                if (!locations.ContainsKey(country))
                {
                    locations[country] = new List<string>();
                   
                }

                locations[country].Add(location.city);


            }


        }

        private void Add_Accomodation_Click(object sender, RoutedEventArgs e)    //DUGMAD
        {
            AddAccomodationWin addAccomodationWin = new AddAccomodationWin(LoggedUser);
            addAccomodationWin.Show();
        }

        private void Rate_A_Guest_Click(object sender, RoutedEventArgs e)
        {
            RateReservationWin rateReservationWin= new RateReservationWin(selectedReservation);
            rateReservationWin.Show();
        }

        private void Log_Out_Click(object sender, RoutedEventArgs e)
        {
            LoginWin loginWin = new LoginWin(); 
            loginWin.Show();
            this.Close();
        }


        public event PropertyChangedEventHandler PropertyChanged;    //za INotyfyPropertyChanged
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

