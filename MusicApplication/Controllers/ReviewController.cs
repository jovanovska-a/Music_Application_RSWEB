using Microsoft.AspNetCore.Mvc;
using MusicApplication.Data.Repositories;
using MusicApplication.Models;
using MusicApplication.ViewModels;

namespace MusicApplication.Controllers
{
    public class ReviewController : Controller
    { 
        private readonly IReviewService _service;
        private readonly ISongService _songService;
        private readonly HttpContextAccessor _httpContextAccessor;

        public ReviewController(IReviewService service, ISongService songService)
        {
            _service = service;
            _songService = songService;
        }

        public async Task<IActionResult> Index(int id)
        {
            var allReviews = await _service.GetAllById(id);
            ReviewViewModel newRVM= new ReviewViewModel()
            {
                SongId = id,
                Reviews = allReviews
            };
            return View(newRVM);
        }

        public IActionResult Create() 
        {
            var currentUser = _service.GetCurrentUserId();
            var newReview = new Review()
            {
                AppUser = currentUser,
            };
            var newVM = new CreateReviewViewModel()
            {
                Review = newReview,
                UserName = _service.GetCurrentUserUsername(currentUser)
            };
            return View(newVM);
        }

        [HttpPost]

        public IActionResult Create(int id, CreateReviewViewModel reviewVM) 
        { 
            if(!ModelState.IsValid)
            {
                return View(reviewVM);
            }
            var newReview = new Review()
            {
                SongId = id,
                AppUser = reviewVM.Review.AppUser,
                Rating = reviewVM.Review.Rating,
                Comment = reviewVM.Review.Comment
            };
            _service.Add(newReview);
            return Redirect("/Review/Index/" + id);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var reviewDetails = await _service.GetByIdAsync(id);
            if (reviewDetails == null) return View("NotFound");
            await _service.DeleteAsync(id);
            return Redirect("/Review/Index/" + reviewDetails.SongId);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var reviewDetails = await _service.GetByIdAsync(id);
            if (reviewDetails == null) return View("NotFound");
            return View(reviewDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Review review)
        {
            if (!ModelState.IsValid)
                return View(review);
            await _service.UpdateAsync(review);
            return Redirect("/Review/Index/"+review.SongId);
        }
    }
}
