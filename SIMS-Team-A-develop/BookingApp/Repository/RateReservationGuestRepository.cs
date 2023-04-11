using BookingApp.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.Repository
{
    public class RateReservationGuestRepository 
    {
        public const string filePath = "../../../Resources/Data/ratesReservationsGuests.csv";

        private readonly Serializer<RateReservatiounGuest> serializer;

        private List<RateReservatiounGuest> rates;

        
        public RateReservationGuestRepository()
        {
            serializer = new Serializer<RateReservatiounGuest>();
            rates = serializer.FromCSV(filePath);
        }

        
 
        public RateReservatiounGuest Save(RateReservatiounGuest rate)
        {
            rate.id = NextId();
            rates = serializer.FromCSV(filePath);
            rates.Add(rate);
            serializer.ToCSV(filePath, rates);

            return rate;
        }
        private int NextId()
        {
            rates = serializer.FromCSV(filePath);
            if (rates.Count < 1)
            {
                return 1;
            }
            return rates.Max(c => c.id) + 1;
        }

        //public List<RateReservatiounGuest> GetAll()
        //{
        //    return serializer.FromCSV(filePath);
        //}

        //public void Delete(RateReservatiounGuest rate)
        //{
        //    rates = serializer.FromCSV(filePath);
        //    RateReservatiounGuest founded = rates.Find(t => t.id == rate.id);
        //    rates.Remove(rate);
        //    serializer.ToCSV(filePath, rates);
        //}

        //public RateReservatiounGuest Update(RateReservatiounGuest rate)
        //{
        //    rates = serializer.FromCSV(filePath);
        //    RateReservatiounGuest current = rates.Find(r => r.id == rate.id);
        //    int index = rates.IndexOf(current);
        //    rates.Remove(current);
        //    rates.Insert(index, rate);
        //    serializer.ToCSV(filePath, rates);
        //    return rate;
        //}
    }
}
