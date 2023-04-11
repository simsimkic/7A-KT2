using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BookingApp.Application.UseCases.Owner;
using BookingApp.Domain.Models.Owner;
using BookingApp.Application.UseCases;


namespace BookingApp.WPF.Views.Guest1.MainWinPages
{
    /// <summary>
    /// Interaction logic for AccommodationsPage.xaml
    /// </summary>
    public partial class AccommodationsPage : Page, INotifyPropertyChanged
    {
        public ObservableCollection<Accommodation> accommodations { get; set; }


        public Accommodation selectedAccommodation { get; set; }

        private AccommodationService accommodationService;


        public string searchName { get; set; }
        public string searchType { get; set; }
        public int searchGuestNum { get; set; }
        public int searchDays { get; set; }

        private string searchCountry;
        private string searchCity;

        public string SearchCountry
        {
            get { return searchCountry; }
            set
            {
                searchCountry = value;

                if (value != "All")
                {
                    CityComboBox.IsEnabled = true;
                    CityComboBox.ItemsSource = locations[searchCountry];
                }
                else
                {
                    CityComboBox.IsEnabled = false;
                    CityComboBox.SelectedItem = "All";
                }

                OnPropertyChanged();
            }
        }

        public string SearchCity
        {
            get { return searchCity; }
            set
            {

                searchCity = value;
                OnPropertyChanged();

            }
        }

        private int userId;

        public List<string> accomodationTypes { get; set; }
        public Dictionary<string, List<string>> locations { get; set; }

        public AccommodationsPage(int userId)
        {
            InitializeComponent();
            InitializeRepositoriesAndLists();

            this.DataContext = this;

            this.userId = userId;
        }

        private void InitializeRepositoriesAndLists()
        {
            

            accommodationService = new AccommodationService();

            accommodations = new ObservableCollection<Accommodation>(accommodationService.GetAll());


            accomodationTypes = new List<string>();
            AddAccommodationTypesToList();

            locations = new Dictionary<string, List<string>>();
            UpdateLocationMap();

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AddAccommodationTypesToList()
        {
            accomodationTypes.Add("All");
            accomodationTypes.Add("Apartment");
            accomodationTypes.Add("House");
            accomodationTypes.Add("Cottage");
        }

        private void UpdateLocationMap()
        {

            LocationService locationService = new LocationService();
            
            var locationsFromFile = locationService.GetAll();
            locations["All"] = new List<string>();

            foreach (var location in locationsFromFile)
            {
                string country = location.country;

                AddNewCountryToMap(country);

                locations[country].Add(location.city);

            }

        }

        private void AddNewCountryToMap(string country)
        {
            if (!locations.ContainsKey(country))
            {
                locations[country] = new List<string>();
                locations[country].Add("All");
            }

        }

        private void ApplyFilter_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<Accommodation> query = accommodations;

            if (!string.IsNullOrEmpty(searchName))
            {
                query = query.Where(x => x.name.ToLower().Contains(searchName.ToLower()));
            }

            if (searchType != "All" && !string.IsNullOrEmpty(searchType))
            {
                query = query.Where(x => x.type.ToString() == searchType);
            }

            if (searchCountry != "All" && !string.IsNullOrEmpty(searchCountry))
            {
                query = query.Where(x => x.location.country == searchCountry);
            }

            if (searchCity != "All" && !string.IsNullOrEmpty(searchCity))
            {
                query = query.Where(x => x.location.city == searchCity);
            }

            if (searchGuestNum > 0)
            {
                query = query.Where(x => x.maxGuests >= searchGuestNum);
            }

            if (searchDays > 0)
            {
                query = query.Where(x => x.minDaysForStay <= searchDays);
            }

            listView.ItemsSource = query;
            OnPropertyChanged();

        }

        private void Reserve_Click(object sender, RoutedEventArgs e)
        {
            if (selectedAccommodation != null)
            {
                var reservationWin = new ReservationWin(selectedAccommodation, userId);


                //// update reservation list when a reservation window has been closed
                //reservationWin.Closed += (sender, e) =>
                //{
                //    reservations = reservationRepository.GetByUserId(userId);
                //    OnPropertyChanged(nameof(reservations));
                //};

                reservationWin.ShowDialog();



            }

        }




    }
}
