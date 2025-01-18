using CinemaBooking.Data;
using CinemaBooking.Models;
using CinemaBooking.Models.DBObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CinemaBooking.Controllers
{
    public class HallsController : Controller
    {
        private Repository.HallsRepository _repository;
        public HallsController(ApplicationDbContext dbContext)
        {
            _repository = new Repository.HallsRepository(dbContext);
        }
        [Authorize(Roles = "Admin")]
        // GET: HallsController
        public ActionResult Index(Guid cinemaId)
        {
            var halls = _repository.GetHallsByCinemaID(cinemaId);
            ViewBag.CinemaID = cinemaId;
            return View("Index", halls);
        }
        // GET: HallsController/Create
        public ActionResult InsertHall(Guid cinemaId)
        {
            ViewBag.CinemaID = cinemaId; 
            return View("InsertHall");
        }

        // POST: HallsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult InsertHall(Guid cinemaId,IFormCollection collection)
        {
            try
            {
                Hall model = new Hall();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _repository.InsertHall(cinemaId,model);
                }
                return RedirectToAction("Index", new {cinemaId});
            }
            catch
            {
                return View("InsertHall");
            }
            return RedirectToAction(nameof(Index));
        }
        // GET: HallsController/Edit/5
        public ActionResult Edit(Guid id,Guid cinemaId)
        {
            var model = _repository.GetHallsByID(id);
            ViewBag.CinemaID=cinemaId;
            return View("Edit", model);
        }

        // POST: HallsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Guid id,Guid cinemaId, IFormCollection collection)
        {
            try
            {
                Hall model = new Hall();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _repository.UpdateHall(model);
                    return RedirectToAction("Index",new { cinemaId });
                }
                else
                {
                    return RedirectToAction("Index", new {cinemaId});
                }
            }
            catch
            {
                return RedirectToAction("Index", new {cinemaId});
            }
        }
        // GET: HallsController/Delete/5
        public ActionResult Delete(Guid id, Guid cinemaId)
        {
            var model = _repository.GetHallsByID(id);
            ViewBag.CinemaID = model.CinemaId;
            return View("Delete", model);
        }

        // POST: HallsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(Guid id, Guid cinemaId, IFormCollection collection)
        {
            var hall = _repository.GetHallsByID(id);
            try
            {
                    _repository.DeleteHall(id);
                if (ModelState.IsValid)
                {
                    ViewBag.CinemaID = hall.CinemaId;
                }
                return RedirectToAction("Index", new { hall.CinemaId });
            }
            catch
            {
                return RedirectToAction("Index", new { cinemaId });
            }
        }
    }
}
