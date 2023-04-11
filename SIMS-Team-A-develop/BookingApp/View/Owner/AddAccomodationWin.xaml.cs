using BookingApp.Model;
using BookingApp.Repository;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using USER_TYPE = BookingApp.Model.USER_TYPE;

namespace BookingApp.View
{
    public partial class AddAccomodationWin : Window, INotifyPropertyChanged
    {
        private string _name;
        public string AName
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _type;
        public string AType
        {
            get => _type;
            set
            {
                if (_type != value)
                {
                    _type = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _maxGuests;
        public string MaxGuests
        {
            get => _maxGuests;
            set
            {
                if (_maxGuests != value)
                {
                    _maxGuests = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _minDaysForReservation;
        public string MinDaysForReservation
        {
            get => _minDaysForReservation;
            set
            {
                if (_minDaysForReservation != value)
                {
                    _minDaysForReservation = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _minDaysForCancelation;
        public string MinDaysForCancelation
        {
            get => _minDaysForCancelation;
            set
            {
                if (_minDaysForCancelation != value)
                {
                    _minDaysForCancelation = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _imageUrl;
        public string ImageURL
        {
            get => _imageUrl;
            set
            {
                if (_imageUrl != value)
                {
                    _imageUrl = value;
                    OnPropertyChanged();
                }
            }

        }

        private string searchCountry;
        public string SearchCountry
        {
            get { return searchCountry; }
            set
            {
                searchCountry = value;

                if (value != null)
                {
                    CityComboBox.IsEnabled = true;
                    CityComboBox.ItemsSource = locations[searchCountry];
                }

                OnPropertyChanged();
            }
        }

        private string searchCity;
        public string SearchCity
        {
            get { return searchCity; }
            set
            {
                searchCity = value;
                OnPropertyChanged();
            }
        }


        public static User LoggedUser { get; set; }
        public Dictionary<string, List<string>> locations { get; set; }

        private readonly AccomodationRepository accomodationRepository;
        private readonly LocationRepository locationRepository;
        private readonly ImageRepository imageRepository;

        public AddAccomodationWin(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedUser = user;

            accomodationRepository = new AccomodationRepository();
            locationRepository = new LocationRepository();
            imageRepository = new ImageRepository();

            locations = new Dictionary<string, List<string>>();
            UpdateLocationMap();
        }

        private void UpdateLocationMap()
        {
            LocationRepository locationRepository = new LocationRepository();
            var locationsFromFile = locationRepository.GetAll();

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
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void Create_New_Accomodation(object sender, RoutedEventArgs e)
        {
            int id = locationRepository.GetIdByCountyAndCity(SearchCity, SearchCountry);
            Location location = new Location(SearchCity, SearchCountry);

            Accommodation accomodation = new Accommodation(LoggedUser.id, AName, id, location, (ACCOMMODATION_TYPE)Enum.Parse(typeof(ACCOMMODATION_TYPE), AType), int.Parse(MaxGuests), int.Parse(MinDaysForReservation), int.Parse(MinDaysForCancelation));
            
            Accommodation accomodation1 = accomodationRepository.Save(accomodation);
            OwnerWin.accommodations.Add(accomodation1);

            string[] listaUrlova = ImageURL.Split(",");

            foreach(string s in listaUrlova)
            {

                Image image = new Image();  //refaktorisi
                image.resourceId = accomodation1.id;
                image.url = s;
                image.imageUserType = USER_TYPE.Accomodation;

                imageRepository.Save(image);
            }


            this.Close();
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
