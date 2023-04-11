﻿using BookingApp.Functionality;
using BookingApp.Model;
using BookingApp.Repository;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using MessageBox = System.Windows.MessageBox;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for CustomerToursMakeReservation.xaml
    /// </summary>
    public partial class CustomerToursMakeReservation : Window
    {

        CustomerToursWin main;
        TourFunctionality tourFunctionality;

        private int idUser;
        private int idAppointment;
        private int maxGuests;
        private string name;
        private DateTime startTime;
        private bool used;
        public TourAppointment SelectedTour { get; set; }

        public CustomerToursMakeReservation(int idUser, int idAppointment, string name, string locationCity, string locationState, DateTime startTime, string description, string language, int maxGuests, double duration, TourAppointment selectedTour, bool used)
        {
            
            InitializeComponent();
            main = new CustomerToursWin(idUser);    
            tourFunctionality = new TourFunctionality(idUser);

            this.idUser = idUser;
            this.idAppointment = idAppointment;
            this.maxGuests = maxGuests;
            this.name = name;    
            this.startTime = startTime; 
            this.SelectedTour = selectedTour;
            this.used = used;
           
            BoxName.Text = name;
            BoxName.IsReadOnly = true;
            BoxLocationCity.Text = locationCity;
            BoxLocationCity.IsReadOnly = true;
            BoxLocationState.Text = locationState;
            BoxLocationState.IsReadOnly = true;
            BoxDate.Text = startTime.ToString();
            BoxDate.IsReadOnly = true;
            BoxDescription.Text = description;
            BoxDescription.IsReadOnly = true;
            BoxLanguage.Text = language;
            BoxLanguage.IsReadOnly = true;
            BoxMaxGuests.Text = maxGuests.ToString();
            BoxMaxGuests.IsReadOnly = true;
            BoxDuration.Text = duration.ToString();      
            BoxDuration.IsReadOnly = true;

            integerUpDown.Maximum = maxGuests;
        }

        private void Confirm_Reservation(object sender, RoutedEventArgs e)
        {
            var requestedGuests = integerUpDown.Value;

            if (requestedGuests == null)
            {
                MessageBox.Show("Please enter the number of people for the reservation.");
            }
            else if (requestedGuests < maxGuests)
            {
                string filePath = "C:\\Users\\filip\\Desktop\\SIMS\\BookingApp\\BookingApp\\Resources\\Data\\CustomerTours\\TourReservations.csv";
                string[] lines = File.ReadAllLines(filePath);

                bool foundInFile = false;

                foreach (string line in lines)
                {
                    if (line.StartsWith(name) && line.Contains(startTime.ToString()))
                    {
                        foundInFile = true;
                        string[] values = line.Split('|');
                        int alreadyBookedGuests = int.Parse(values[1]);

                        var restIfFound = maxGuests - (requestedGuests + alreadyBookedGuests);
                        var freePlaces = maxGuests - alreadyBookedGuests;

                        if (restIfFound > 0)
                        {
                            var newAlreadyBookedGuests = requestedGuests + alreadyBookedGuests;

                            string reservationInfo = name + "|" + newAlreadyBookedGuests + "|" + startTime + "|" + idAppointment + "|" + idUser + "|" + requestedGuests + "|" + used; 
                            File.WriteAllText(filePath, string.Join(Environment.NewLine, reservationInfo));

                            MessageBox.Show("Reservation successful! There are still " + restIfFound + " places!");
                            break;
                        }

                        else if (restIfFound == 0)
                        {
                            var newAlreadyBookedGuests = requestedGuests + alreadyBookedGuests;

                            string reservationInfo = name + "|" + newAlreadyBookedGuests + "|" + startTime + "|" + idAppointment + "|" + idUser + "|" + requestedGuests + "|" + used; 
                            File.WriteAllText(filePath, string.Join(Environment.NewLine, reservationInfo));

                            MessageBox.Show("Reservation successful! There are no more places, capacity is full!");
                            break;
                        }


                        else if (freePlaces == 0)
                        {

                            MessageBox.Show("There are no more places, capacity is full! Here are the alternatives!");

                            var alternatives = tourFunctionality.tours.Where(t => t.tour.Location.country == SelectedTour.tour.Location.country && t.tour.name != SelectedTour.tour.name);
                            main.listView.ItemsSource = alternatives;
                            tourFunctionality.SetAlternativeTours(alternatives);
                            main.Show();
                            this.Close();
                            break;
                        }

                        MessageBox.Show("There are no places for " + requestedGuests + " people. There is " + freePlaces + " places left!");
                    }
                }

                if (!foundInFile)
                {
                    string reservationInfo = name + "|" + requestedGuests + "|" + startTime + "|" + idAppointment + "|" + idUser + "|" + requestedGuests + "|" + used; 
                    var restIfNotFound = maxGuests - requestedGuests;

                    File.AppendAllText(filePath, reservationInfo + Environment.NewLine);
                    MessageBox.Show("Reservation successful! There are still " + restIfNotFound + " places!");
                }
            }

            else if(requestedGuests == maxGuests) 
           {
                string filePath = "C:\\Users\\filip\\Desktop\\SIMS\\BookingApp\\BookingApp\\Resources\\Data\\CustomerTours\\TourReservations.csv";
                string[] lines = File.ReadAllLines(filePath);

                bool foundInFile = false;

                foreach (string line in lines)
                {
                    if (line.StartsWith(name) && line.Contains(startTime.ToString()))
                    {
                        foundInFile = true;
                        string[] values = line.Split('|');
                        int alreadyBookedGuests = int.Parse(values[1]);

                        MessageBox.Show("Sorry, " + alreadyBookedGuests + " places are already reserved. Try again!");
                        break;
                    }
                }

                if (!foundInFile)
                {
                    string reservationInfo = name + "|" + integerUpDown.Value + "|" + startTime + "|" + idAppointment + "|" + idUser + "|" + requestedGuests + "|" + used; 

                    File.AppendAllText(filePath, reservationInfo + Environment.NewLine);
                    MessageBox.Show("Reservation successful! There are no more places, capacity is full!");
                }
            }      
        }

        private void Cancel_Reservation(object sender, RoutedEventArgs e)
        {
            GetWindow(this).Close();
        }      
    }
}
