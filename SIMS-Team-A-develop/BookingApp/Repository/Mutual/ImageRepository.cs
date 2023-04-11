﻿using BookingApp.Domain.Models;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.RepositoryInterfaces;

namespace BookingApp.Repository.Mutual
{
    public class ImageRepository:IImageRepository
    {

        private const string filePath = "../../../Resources/Data/images.csv";

        private readonly Serializer<Image> serializer;

        private List<Image> images;

        public ImageRepository()
        {
            serializer = new Serializer<Image>();
            images = serializer.FromCSV(filePath);
        }

        public Image Save(Image image) 
        {
            image.id = NextId();
            images = serializer.FromCSV(filePath);
            images.Add(image);
            serializer.ToCSV(filePath,images);
            return image;
        
        }

        public List<Image> getAccomodationImg()
        {
            images = serializer.FromCSV(filePath);

            return images.Where(x => x.imageUserType == IMG_USER_TYPE.Accomodation).ToList();
        }
     

        private int NextId()
        {
            images = serializer.FromCSV(filePath);
            if (images.Count < 1)
                return 1;

            return images.Max(x => x.id) + 1;
        }

    }
}
