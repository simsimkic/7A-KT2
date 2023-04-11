using BookingApp.Controller;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace BookingApp.View
{
    public partial class AddTourWin : Window, INotifyPropertyChanged
    {
        private readonly TourController _tourcontroller;
        public AddTourWin(TourController tourcontroller, int guideId)
        {
            InitializeComponent();

            this.DataContext = this;
            this.GuideId = guideId;
            _tourcontroller = tourcontroller;
        }
        public int GuideId { get; set; }

        private string _name = string.Empty;
        public string NameBind
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _language = string.Empty;
        public string LanguageBind
        {
            get => _language;
            set
            {
                if (value != _language)
                {
                    _language = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _description = string.Empty;
        public string Description
        {
            get => _description;
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _maxguests;
        public int MaxGuests
        {
            get => _maxguests;
            set
            {
                if (value != _maxguests)
                {
                    _maxguests = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _starttime;
        public DateTime StartTime
        {
            get => _starttime;
            set
            {
                if (value != _starttime)
                {
                    _starttime = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _duration;
        public int Duration
        {
            get => _duration;
            set
            {
                if (value != _duration)
                {
                    _duration = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _city = string.Empty;
        public string City
        {
            get => _city;
            set
            {
                if (value != _city)
                {
                    _city = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _country = string.Empty;
        public string Country
        {
            get => _country;
            set
            {
                if (value != _country)
                {
                    _country = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _startpointname = string.Empty;
        public string StartPointName
        {
            get => _startpointname;
            set
            {
                if (value != _startpointname)
                {
                    _startpointname = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _midpointname = string.Empty;
        public string MidPointName
        {
            get => _midpointname;
            set
            {
                if (value != _midpointname)
                {
                    _midpointname = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _endpointname = string.Empty;
        public string EndPointName
        {
            get => _endpointname;
            set
            {
                if (value != _endpointname)
                {
                    _endpointname = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _imageUrl = string.Empty;
        public string ImageUrl
        {
            get => _imageUrl;
            set
            {
                if (value != _imageUrl)
                {
                    _imageUrl = value;
                    OnPropertyChanged();
                }
            }
        }
        private List<DateTime> _dates = new List<DateTime>();
        public List<DateTime> Dates
        {
            get => _dates;
            set
            {
                if (value != _dates)
                {
                    _dates = value;
                    OnPropertyChanged();
                }
            }
        }
        private List<String> _midPointNames = new List<String>();
        public List<String> MidPointNames
        {
            get => _midPointNames;
            set
            {
                if (value != _midPointNames)
                {
                    _midPointNames = value;
                    OnPropertyChanged();
                }
            }
        }
        private List<String> _images = new List<String>();
        public List<String> Images
        {
            get => _images;
            set
            {
                if (value != _images)
                {
                    _images = value;
                    OnPropertyChanged();
                }
            }
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            _tourcontroller.Create(GuideId, NameBind, Description, LanguageBind, StartTime, Duration, City, Country, MaxGuests, StartPointName, MidPointNames, EndPointName, Dates, Images);
            Close();
        }
        private void AddDate_Click(object sender, RoutedEventArgs e)
        {
            Dates.Add(StartTime);
        }
        private void AddMidPoint_Click(object sender, RoutedEventArgs e)
        {
            MidPointNames.Add(MidPointName);
        }
        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            Images.Add(ImageUrl);
        }
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        
    }
}
