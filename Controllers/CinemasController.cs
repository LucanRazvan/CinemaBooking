using CinemaBooking.Data;
using CinemaBooking.Models;
using CinemaBooking.Models.DBObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CinemaBooking.Controllers
{
    public class CinemasController : Controller
    {
        private Repository.CinemasRepository _repository;
        public CinemasController(ApplicationDbContext dbContext)
        {
            _repository = new Repository.CinemasRepository(dbContext);
        }
        [Authorize(Roles ="Admin")]
        // GET: CinemasController
        public ActionResult Index()
        {
            var cinemas = _repository.GetAllCinemasWithHalls();
            return View("Index", cinemas);
        }
        // GET: CinemasController/Create
        public ActionResult InsertCinema()
        {
            return View("InsertCinema");
        }

        // POST: CinemasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult InsertCinema(IFormCollection collection)
        {
            try
            {
                Cinema model = new Cinema();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _repository.InsertCinema(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View("InsertCinema");
            }
            return RedirectToAction(nameof(Index));
        }
        // GET: CinemasController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var model = _repository.GetCinemaByID(id);
            return View("Edit", model);
        }

        // POST: CinemasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                Cinema model = new Cinema();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _repository.UpdateCinema(model);
                    return RedirectToAction("Index");
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
    }
}
