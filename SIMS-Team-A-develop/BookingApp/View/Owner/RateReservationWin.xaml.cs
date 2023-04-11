using BookingApp.Model;
using BookingApp.Repository;
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
using System.Xml.Linq;

namespace BookingApp.View.Owner
{
    public partial class RateReservationWin : Window, INotifyPropertyChanged
    {
        private readonly RateReservationGuestRepository rateReservationGuestRepository;
        public AccommodationReservation selectedReservation { get; set; }
        public RateReservationWin(AccommodationReservation selected)
        {
            InitializeComponent();
            DataContext = this;
            rateReservationGuestRepository = new RateReservationGuestRepository();
            selectedReservation = selected;
        }

        private int _tidiness;
        public int Tidiness
        {
            get => _tidiness;
            set
            {
                if (_tidiness != value)
                {
                    _tidiness = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _rules;
        public int RulesFollowed
        {
            get => _rules;
            set
            {
                if (_rules != value)
                {
                    _rules = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _comment;
        public string Comment
        {
            get => _comment;
            set
            {
                if (_comment != value)
                {
                    _comment = value;
                    OnPropertyChanged();
                }
            }
        }


       

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        public AccommodationReservation SelectedReservation { get; set; }
        public RateReservatiounGuest SelectedReview { get; set; }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            ////ovde treba da preko selected item nabavim koja je reervacija u pitanju i onda nju prosledim u ovaj konstrktor
            RateReservatiounGuest rateReservatiounGuest= new RateReservatiounGuest(selectedReservation, Tidiness, RulesFollowed, Comment);
            RateReservatiounGuest rateReservatiounGuest1 = rateReservationGuestRepository.Save(rateReservatiounGuest);



            this.Close();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
