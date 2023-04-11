using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;

namespace BookingApp.Application.UseCases
{
    public class ImageService
    {
        private IImageRepository imageRepository;

        public ImageService()
        {
            imageRepository = Injector.Injector.CreateInstance<IImageRepository>();
        }

        public Image Save(Image image)
        {
            return imageRepository.Save(image);
        }

        public List<Image> GetAccommodationImgImages()
        {
            return imageRepository.getAccomodationImg();
        }

    }
}
