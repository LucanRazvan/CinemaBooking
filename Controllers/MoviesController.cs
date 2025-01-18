using CinemaBooking.Data;
using CinemaBooking.Models;
using CinemaBooking.Models.DBObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CinemaBooking.Controllers
{
    public class MoviesController : Controller
    {
        private Repository.MoviesRepository _repository;
        public MoviesController(ApplicationDbContext dbContext)
        {
            _repository = new Repository.MoviesRepository(dbContext);
        }
        // GET: MoviesController
        public ActionResult Index()
        {
            var movies=_repository.GetAllMovies();
            return View("Index",movies);
        }
        // GET: MoviesController/Create
        public ActionResult InsertMovie()
        {
            return View("InsertMovie");
        }

        // POST: MoviesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult InsertMovie(IFormCollection collection)
        {
            try
            {
                Movie model = new Movie();
                var task=TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _repository.InsertMovie(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View("InsertMovie");
            }
            return RedirectToAction(nameof(Index));
        }
        // GET: MoviesController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var model =_repository.GetMoviesByID(id);
            return View("Edit",model);
        }

        // POST: MoviesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                Movie model = new Movie();
                var task= TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result) 
                { 
                    _repository.UpdateMovie(model);
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index",id);
                }
            }
            catch
            {
                return RedirectToAction("Index",id);
            }
        }

        // GET: MoviesController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var model= _repository.GetMoviesByID(id);
            return View("Delete",model);
        }

        // POST: MoviesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                _repository.DeleteMovie(id);
                return View("Delete");
            }
            catch
            {
                return View("Delete",id);
            }
        }
    }
}
