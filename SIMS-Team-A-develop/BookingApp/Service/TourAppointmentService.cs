using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    public class TourAppointmentService
    {
        private readonly TourAppointmentRepository _tourAppointmentRepository;
        private readonly TourService _tourService;
        private readonly TourAppointmentPointRepository _tourAppointmentPointRepository;
        private readonly ImageRepository imageRepository;
        private readonly TourAttendenceService _tourAttendenceService;
        public TourAppointmentService()
        {
            _tourAppointmentRepository = new TourAppointmentRepository();
            _tourService = new TourService();
            _tourAppointmentPointRepository = new TourAppointmentPointRepository();
            _tourAttendenceService = new TourAttendenceService();
            imageRepository = new ImageRepository();
        }

        public void Create(Tour tour, DateTime startTime, int guideId)
        {
            _tourAppointmentRepository.Save(new TourAppointment(tour, startTime, guideId));
        }

        public List<TourAppointment> GetAll()
        {
            List<TourAppointment> tourAppointments = new List<TourAppointment>();
            List<Tour> tours = new List<Tour>();
            tourAppointments.AddRange(_tourAppointmentRepository.GetAll());
            tours = _tourService.GetAll();

            foreach (TourAppointment tourAppointment in tourAppointments)
            {
                tourAppointment.tour = GetTourById(tourAppointment.tour.id, tours);
            }

            return tourAppointments;
        }

        private Tour GetTourById(int tourId, List<Tour> tours)
        {
            foreach(Tour tour in tours)
            {
                if(tour.id == tourId)
                {
                    BindImageToTour(tour);
                    return tour;
                }
            }
            return new Tour();
        }

        public List<TourAppointment> GetAllByGuide(int guideId)
        {
            List<TourAppointment> tourAppointments = new List<TourAppointment>();

            foreach(TourAppointment tourAppointment in GetAll())
            {
                if(tourAppointment.tour.guideId == guideId && tourAppointment.startTime.Date >= DateTime.Today.Date 
                    && !tourAppointment.started && !tourAppointment.cancelled)
                {
                    tourAppointments.Add(tourAppointment);
                }
            }

            return tourAppointments;
        }

        public List<TourAppointment> GetTodaysAppointmentsByGuide(int guideId)
        {
            List<TourAppointment> tourAppointments = new List<TourAppointment>();

            foreach(TourAppointment tourAppointment in GetAllByGuide(guideId))
            {
                if(tourAppointment.startTime.Date == DateTime.Today.Date && !tourAppointment.started)
                {
                    tourAppointments.Add(tourAppointment);
                }
            }

            return tourAppointments;
        }

        public Boolean HasGuideStartedTour(int guideId)
        {
            foreach(TourAppointment tourAppointment in GetAllByGuide(guideId))
            {
                if (tourAppointment.started)
                {
                    if (!tourAppointment.ended)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void StartTour(int tourAppointmentId)
        {
            foreach (TourAppointment tourAppointment in GetAll()) 
            {
                if(tourAppointment.id == tourAppointmentId)
                {
                    tourAppointment.started = true;
                    _tourAppointmentRepository.Update(tourAppointment);
                    CreateTourPointsForAppointment(tourAppointment);
                    _tourAttendenceService.CreateAttendencesForTour(tourAppointment);
                    return;
                }
            }
        }
        private void CreateTourPointsForAppointment(TourAppointment tourAppointment)
        {
            List<TourPoint> tourPoints = new List<TourPoint>();
            tourPoints = _tourService.GetTourPointsByTourId(tourAppointment.tour.id);

            foreach(TourPoint point in tourPoints)
            {
                point.tour.id = tourAppointment.id;
                if(point.order == 1)
                {
                    point.status = true;
                }
                _tourAppointmentPointRepository.Save(point);
            }
        }
        public Boolean CancelTour(TourAppointment tourAppointment)
        {
            if((tourAppointment.startTime - DateTime.Today).TotalHours <= 48)
            {
                return false;
            }

            tourAppointment.cancelled = true;
            _tourAttendenceService.CreateVouchers(tourAppointment);

            _tourAppointmentRepository.Update(tourAppointment);


            return true;
        }
        public TourAppointment GetLiveTour(int guideId)
        {
            foreach (TourAppointment tourAppointment in GetAllByGuide(guideId))
            {
                if (tourAppointment.started)
                {
                    if (!tourAppointment.ended)
                    {
                        BindTourPointsToAppointment(tourAppointment);
                        return tourAppointment;
                    }
                }
            }
            return new TourAppointment();
        }

        private void BindTourPointsToAppointment(TourAppointment tourAppointment)
        {
            List<TourPoint> tourPoints = new List<TourPoint>();
            tourAppointment.tourPoints = new List<TourPoint>();

            tourPoints = _tourAppointmentPointRepository.GetAll();
            foreach(TourPoint tourPoint in tourPoints)
            {
                if(tourPoint.tour.id == tourAppointment.id)
                {
                    tourAppointment.tourPoints.Add(tourPoint);
                }
            }
        }

        public void EndTour(TourAppointment tourAppointment)
        {
            foreach(TourPoint tourPoint in tourAppointment.tourPoints)
            {
                if(tourPoint.pointType == PointType.End)
                {
                    tourPoint.status = true;
                    _tourAppointmentPointRepository.Update(tourPoint);
                }
            }
            tourAppointment.ended = true;
            _tourAppointmentRepository.Update(tourAppointment);
        }

        private void BindImageToTour(Tour tour)
        {
            List<Image> images = new List<Image>();
            tour.tourImages = new List<Image>();

            images = imageRepository.getTourImg();
            foreach (Image image in images)
            {
                if (image.resourceId == tour.id)
                {
                    tour.tourImages.Add(image);
                }
            }
            if (tour.tourImages.Count > 0)
                tour.image = tour.tourImages.First();
        }
    }
}
