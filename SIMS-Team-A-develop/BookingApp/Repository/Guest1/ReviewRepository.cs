﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Models.Guest1;
using BookingApp.Domain.RepositoryInterfaces.Guest1;
using InitialProject.Serializer;

namespace BookingApp.Repository.Guest1
{
    class ReviewRepository:IReviewRepository
    {
        private const string filePath = "../../../Resources/Data/accommodationReviews.csv";

        private readonly Serializer<AccommodationReview> serializer;

        private List<AccommodationReview> reviews;

        public ReviewRepository()
        {
            serializer = new Serializer<AccommodationReview>();
            reviews = serializer.FromCSV(filePath);
        }

        public AccommodationReview Save(AccommodationReview review)
        {
            review.id = NextId();
            reviews = serializer.FromCSV(filePath);
            reviews.Add(review);
            serializer.ToCSV(filePath,reviews);

            return review;
        }

        public List<AccommodationReview> GetByAccommodationId(int id)
        {
            reviews = serializer.FromCSV(filePath);
            var filteredReviews = reviews.Where(x => x.accommodationId == id);

            return filteredReviews.ToList();
        }

        public List<AccommodationReview> GetByUserId(int id)
        {
            reviews = serializer.FromCSV(filePath);
            var filteredReviews = reviews.Where(x => x.userId == id);

            return filteredReviews.ToList();
        }

        private int NextId()
        {
            reviews = serializer.FromCSV(filePath);
            if (reviews.Count < 1)
                return 1;

            return reviews.Max(x => x.id) + 1;
        }


    }
}
