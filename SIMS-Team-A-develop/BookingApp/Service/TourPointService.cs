using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    internal class TourPointService
    {
        private readonly TourAppointmentPointRepository _tourAppointmentPointRepository;

        public TourPointService()
        {
            _tourAppointmentPointRepository= new TourAppointmentPointRepository();
        }

        public void SetCheckPoint(TourPoint tourPoint)
        {
            tourPoint.status = true;
            _tourAppointmentPointRepository.Update(tourPoint);
        }

        public TourPoint FindCurrentPoint(TourAppointment tourAppointment)
        {
            TourPoint currentPoint = new TourPoint();
            foreach(TourPoint tourPoint in tourAppointment.tourPoints)
            {
                if(tourPoint.status == true)
                {
                    currentPoint = tourPoint;
                }
            }

            return currentPoint;
        }

    }
}
