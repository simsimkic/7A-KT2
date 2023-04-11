using InitialProject.Model;
using InitialProject.Serializer;

namespace BookingApp.Model
{
    public class RateReservatiounGuest : ISerializable
    {
        public int id { get; set; }
        public int tidiness { get; set; }
        public int rulesFollowed { get; set; }
        public string comment { get; set; }
        public AccommodationReservation AccommodationReservation { get; set; }

        public RateReservatiounGuest()
        { 
           AccommodationReservation = new AccommodationReservation();
        }
        
        public RateReservatiounGuest( AccommodationReservation AccommodationReservation, int tidiness, int rulesFollowed, string comment)
        {
            this.AccommodationReservation = AccommodationReservation;  
            this.tidiness = tidiness;
            this.rulesFollowed = rulesFollowed;
            this.comment = comment;
        }

        public string[] ToCSV()
        {
            string[] csValues =
            {
                id.ToString(),
                tidiness.ToString(),
                rulesFollowed.ToString(),
                comment,
                AccommodationReservation.id.ToString()
            };
            return csValues;
        }

        public void FromCSV(string[] values)
        {
            id = int.Parse(values[0]);
            tidiness= int.Parse(values[1]);
            rulesFollowed= int.Parse(values[2]);    
            comment = values[3];
            AccommodationReservation.id = int.Parse(values[4]);
        }
    }
}
