using Microsoft.AspNetCore.Mvc;
using Capstone.DAO.Interfaces;
using Capstone.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class AdminController : ControllerBase
    {
        private readonly IUserDao userDao;
        private readonly IReviewDao reviewDao;

        public AdminController(IUserDao userDao, IReviewDao reviewDao)
        {
            this.userDao = userDao;
            this.reviewDao = reviewDao;
        }

        [HttpGet("users")]
        public ActionResult<IList<User>> GetUsers()
        {
            return Ok(userDao.GetUsers());
        }

        [HttpGet("users/{id}/reviews")]
        public ActionResult<IList<Review>> GetUserReviews(int id)
        {
            return Ok(reviewDao.GetReviewsByUser(id));
        }

        [HttpGet("reviews")]
        public ActionResult<IList<Review>> GetAllReviews()
        {
            return Ok(reviewDao.GetAllReviews());
        }

        [HttpPut("reviews/{id}")]
        public ActionResult<Review> UpdateReview(int id, Review review)
        {
            if (reviewDao.GetReviewByID(id) == null)
            {
                return NotFound();
            }
            return Ok(reviewDao.UpdateReview(id, review));
        }

        [HttpDelete("reviews/{id}")]
        public ActionResult DeleteReview(int id)
        {
            if (!reviewDao.DeleteReview(id))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
