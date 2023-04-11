using BookingApp.Model;
using BookingApp.Repository;
using InitialProject.Model;
using InitialProject.Repository;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    internal class TourAttendenceService
    {
        private readonly TourAttendenceRepository _tourAttendenceRepository;
        private readonly TourReservationRepository _tourReservationRepository;
        private readonly UserRepository _userRepository;
        private readonly VoucherRepository _voucherRepository;


        public TourAttendenceService()
        {
            _tourAttendenceRepository = new TourAttendenceRepository();
            _tourReservationRepository = new TourReservationRepository();
            _userRepository = new UserRepository();
            _voucherRepository = new VoucherRepository();
        }
        public List<TourAttendence> GetAll(TourAppointment tourAppointment)
        {
            List<TourAttendence> tourAttendences= new List<TourAttendence>();
            foreach(TourAttendence tourAttendence in _tourAttendenceRepository.GetAll())
            {
                if(tourAttendence.appointmentId == tourAppointment.id)
                {
                    BindUserToAttendence(tourAttendence);
                    tourAttendences.Add(tourAttendence);
                }
            }
            return tourAttendences;
        }
        public void CreateAttendencesForTour(TourAppointment tourAppointment)
        {
            List<TourReservation> reservations = new List<TourReservation>(_tourReservationRepository.GetAll());
            foreach(TourReservation reservation in reservations)
            {
                if(reservation.appointmentId == tourAppointment.id)
                {
                    Create(reservation.guestId, tourAppointment.id);
                }
            }
        }

        private void BindUserToAttendence(TourAttendence tourAttendence)
        {
            foreach (User user in _userRepository.GetAll())
            {
                if(user.id == tourAttendence.guestId)
                {
                    tourAttendence.guest = user;
                }
            }
        }

        private TourAttendence Create(int guestId, int appointmentId)
        {
            return _tourAttendenceRepository.Save(new TourAttendence(guestId, appointmentId));
        }

        public void SendNotification(TourAttendence tourAttendence, TourPoint currentPoint) //TODO: Current Checkpoint
        {
            tourAttendence.requestSent = true;
            tourAttendence.currentPoint = currentPoint;
            _tourAttendenceRepository.Update(tourAttendence);
        }

        public void CreateVouchers(TourAppointment tourAppointment)
        {
            foreach(TourReservation tourReservation in _tourReservationRepository.GetAll())
            {
                if(tourReservation.appointmentId == tourAppointment.id)
                {
                    DateTime expireDate = DateTime.Now.AddYears(1);
                    _voucherRepository.Save(new Voucher(tourReservation.guestId, expireDate));
                }
            }
        }
    }
}
