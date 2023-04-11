using System.ComponentModel;
using System.Windows;
using BookingApp.WPF.Views.Guest1.MainWinPages;

namespace BookingApp.WPF.Views.Guest1
{
    /// <summary>
    /// Interaction logic for MainBookingWin.xaml
    /// </summary>
    public partial class MainBookingWin : Window, INotifyPropertyChanged
    {
        
        

        private AccommodationsPage accomodationPage;
        private ReservationsPage reservationsPage;
        private RequestsPage requestsPage;

        private int userId;

        public MainBookingWin(int userId)
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            this.DataContext = this;
            this.userId = userId;

            InitializePages();

            AccommodationsButton.IsEnabled = true;
            MainFrame.Content = accomodationPage;

            

        }

        private void InitializePages()
        {
            accomodationPage = new AccommodationsPage(userId);
            reservationsPage = new ReservationsPage(userId,MainFrame);
            requestsPage = new RequestsPage(userId);
        }
       

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
      
        private void AccommodationsButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = accomodationPage;
            OnPropertyChanged();
        }

        private void ReservationsButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = reservationsPage;
            OnPropertyChanged();
        }

        private void Requests_OnClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = requestsPage;
            OnPropertyChanged();
        }


        private void WinClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void WinMaximize_Click(object sender, RoutedEventArgs e)
        {
            

            if (this.WindowState != WindowState.Maximized)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }

        private void WindMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void TitleBar_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                DragMove();
            }

        }

        
    }
}
