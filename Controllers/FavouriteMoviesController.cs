using CinemaBooking.Data;
using CinemaBooking.Models.DBObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CinemaBooking.Controllers
{
    public class FavouriteMoviesController : Controller
    {
        private Repository.FavouriteMoviesRepository _repository;
        private UserManager<IdentityUser> _userManager;
        public FavouriteMoviesController(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _repository = new Repository.FavouriteMoviesRepository(dbContext);
            _userManager = userManager;
        }
        [Authorize(Roles ="Admin, User")]
        // GET: FavouriteMoviesController
        public ActionResult Index(Guid userId)
        {
            List<FavouriteMovie> model=_repository.GetFavouriteMoviesByUserID(userId);
            return View("Index",model);
        }
        // GET: FavouriteMoviesController/Create
        public ActionResult AddToWatchList(Guid movieId)
        {
            ViewBag.MovieID=movieId;
            Guid userId = new Guid(_userManager.GetUserId(HttpContext.User));
            ViewBag.UserID=userId;

            Movie model=_repository.GetMovieDetailsByMovieID(movieId);
            return View("AddToWatchList",model);
        }

        // POST: FavouriteMoviesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, User")]
        public ActionResult AddToWatchList(IFormCollection collection)
        {
            Guid userId = new Guid(_userManager.GetUserId(HttpContext.User));
            Guid movieId = new Guid(collection["MovieID"]);
            try
            {
                FavouriteMovie model = new FavouriteMovie();
                    _repository.InsertFavouriteMovie(movieId, userId, model);
                return RedirectToAction("Index",new { userId });
            }
            catch
            {
                return View("AddToWatchList");
            }
        }
        // GET: FavouriteMoviesController/Delete/5
        public ActionResult Delete(Guid favouriteId)
        {
            FavouriteMovie model=_repository.GetFavouriteMoviesByID(favouriteId);
            return View("Delete",model);
        }

        // POST: FavouriteMoviesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, User")]
        public ActionResult Delete(Guid favouriteId, IFormCollection collection)
        {
            Guid userId = new Guid(_userManager.GetUserId(HttpContext.User));
            try
            {
                _repository.DeleteFavouriteMovieByID(favouriteId);
                return RedirectToAction("Index", new {userId});
            }
            catch
            {
                return View();
            }
        }
    }
}
