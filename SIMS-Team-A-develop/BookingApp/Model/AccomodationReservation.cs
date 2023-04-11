using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Globalization;

namespace BookingApp.Model
{
    public class AccommodationReservation : ISerializable
    {
        public int idGuest { get; set; }
        public User guest { get; set; } 
        public int id { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }  
        public Accommodation accommodation { get; set; }
        public int guestNumber { get; set; }
        public bool isRated { get; set; }

        public AccommodationReservation() 
        {
            accommodation = new Accommodation();
        }

        public AccommodationReservation(DateTime startDate, DateTime endDate, Accommodation accomodation, int idGuest,int guestNumber, bool isRated) 
        {
            this.accommodation = accomodation;
            this.startDate = startDate;
            this.endDate = endDate;
            this.idGuest = idGuest;
            this.guestNumber = guestNumber;
            this.isRated = isRated;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
            id.ToString(),
            startDate.ToString("dd'/'MM'/'yyyy"),
            endDate.ToString("dd'/'MM'/'yyyy"),
            accommodation.id.ToString(),
            idGuest.ToString(),
            guestNumber.ToString(),
            isRated.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            id = int.Parse(values[0]);
            startDate = DateTime.ParseExact(values[1], "dd'/'MM'/'yyyy", CultureInfo.InvariantCulture);
            endDate = DateTime.ParseExact(values[2], "dd'/'MM'/'yyyy", CultureInfo.InvariantCulture);
            accommodation.id = int.Parse(values[3]);
            idGuest = int.Parse(values[4]);
            guestNumber = int.Parse(values[5]);
            isRated = bool.Parse(values[6]);
        }

    }
}
