using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace BookingApp.View.CustomerTour
{
    /// <summary>
    /// Interaction logic for CustomerTourFeedbackWin.xaml
    /// </summary>
    public partial class CustomerTourFeedbackWin : Window
    {
        private int idUser;

        public CustomerTourFeedbackWin()
        {
            InitializeComponent();
        }

        private void Submit(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(url_Input.Text) && num1_Input.Value != null && num1_Input.Value != null && num3_Input.Value != null)
            {
                string filePath = "C:\\Users\\filip\\Desktop\\SIMS\\BookingApp\\BookingApp\\Resources\\Data\\CustomerTours\\TourFeedback.csv";

                string lines =  url_Input.Text + "|" + num1_Input.Value.ToString() + "|" + num2_Input.Value.ToString() + "|" + num3_Input.Value.ToString();
                File.AppendAllText(filePath, lines + Environment.NewLine);

                MessageBox.Show("Your form is submited. Thank you!");
            }
            else
            {
                MessageBox.Show("Please fill in the form completely!");
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            CustomerToursWin main = new CustomerToursWin(idUser);
            main.Show();

        }
    }
}
