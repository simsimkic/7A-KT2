using BookingApp.Controller;
using BookingApp.Model;
using BookingApp.View.Guide;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace BookingApp.View.CustomerTour
{
    /// <summary>
    /// Interaction logic for CustomerTourLiveWin.xaml
    /// </summary>
    public partial class CustomerTourLiveWin : Window, INotifyPropertyChanged
    {
        private int quideId;
        public ObservableCollection<TourPoint> TourPoints { get; set; }
        public TourAppointment LiveTourAppointment { get; set; }

        private readonly TourAppointmentController _tourAppointmentController;

        public CustomerTourLiveWin(ObservableCollection<TourPoint> tourPoints)
        {
            InitializeComponent();

            this.DataContext = this;
            TourPoints = tourPoints;

            _tourAppointmentController = new TourAppointmentController();
            LiveTourAppointment = _tourAppointmentController.GetLiveTour(quideId);

            this.TourName = LiveTourAppointment.tour.name;
        }

        private string _tourName = "";
       

        public string TourName
        {
            get { return _tourName; }
            set
            {
                if (value != _tourName)
                {
                    _tourName = value;
                    OnPropertyChanged(nameof(TourName));
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;




    }
}
