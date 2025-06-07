using Capstone.Models;
using System.Collections;
using System.Collections.Generic;

namespace Capstone.DAO.Interfaces
{
    public interface IReviewDao
    {
        IList<Review> GetRestaurantReviews(int restID, int userID = 0);
        IList<Review> GetReviewsByUser(int userID);
        IList<Review> GetDrinkReviews(int drinkID);
        Review AddRestaurantReview(Review review);
        Review GetReviewByID(int id);
        bool DeleteRestaurantReview(int reviewID);
        IList<Review> GetAllReviews();
        Review UpdateReview(int reviewID, Review review);
        bool DeleteReview(int reviewID);
    }
}
