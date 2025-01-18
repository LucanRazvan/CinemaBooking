using CinemaBooking.Data;
using CinemaBooking.Models.DBObjects;
using CinemaBooking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace CinemaBooking.Controllers
{
    public class ScreeningsController : Controller
    {
        private Repository.ScreeningsRepository _repository;
        public ScreeningsController(ApplicationDbContext dbContext)
        {
            _repository = new Repository.ScreeningsRepository(dbContext);
        }
        [AllowAnonymous]
        // GET: ScreeningsController
        public ActionResult Index(Guid movieId)
        {
            List<Screening>? screenings =_repository.GetScreeningsByMovieID(movieId);
            ViewBag.MovieID=movieId;
            return View("Index",screenings);
        }
        // GET: ScreeningsController/Create
        public ActionResult InsertScreening(Guid movieId)
        {
            ViewBag.MovieID=movieId;
            return View("InsertScreening");
        }

        // POST: ScreeningsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, User")]
        public ActionResult InsertScreening(Guid movieId,IFormCollection collection)
        {
            try
            {
                string cinemaName = collection["CinemaName"];
                int hallNumber = int.Parse(collection["HallNumber"]);
                Guid hallId = _repository.GetHallIdByCinemaAndNumber(cinemaName, hallNumber);

                Screening model = new Screening();
                var task=TryUpdateModelAsync(model);
                
                task.Wait();
                if (task.Result)
                {
                    _repository.InsertScreening(movieId, hallId, model);
                }
                return RedirectToAction("Index", new { movieId });
            }
            catch
            {
                return View("InsertScreening");
            }
            return RedirectToAction(nameof(Index));
        }
        // GET: ScreeningsController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var model = _repository.GetScreeningsByID(id);
            ViewBag.MovieID = model.MovieId;
            return View("Edit",model);
        }
        // POST: ScreeningsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                string cinemaName = collection["CinemaName"];
                int hallNumber = int.Parse(collection["HallNumber"]);
                Screening model = new Screening();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _repository.UpdateScreening(model,cinemaName,hallNumber);
                    var movieId = model.MovieId;
                    return RedirectToAction("Index",new { movieId });
                }
                else
                {
                    return RedirectToAction("Index", id);
                }
            }
            catch
            {
                return RedirectToAction("Index", id);
            }
        }
        // GET: ScreeningsController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var screening=_repository.GetScreeningsByID(id);
            ViewBag.MovieID = screening.MovieId;
            return View("Delete",screening);
        }

        // POST: ScreeningsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            var screening = _repository.GetScreeningsByID(id);
            try
            {
                _repository.DeleteScreening(id);
                if (ModelState.IsValid)
                {
                    ViewBag.MovieID = screening.MovieId;
                }
                return View("Delete");
            }
            catch
            {
                return View("Delete", id);
            }
        }
    }
}
