using CinemaBooking.Data;
using CinemaBooking.Models.DBObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using Microsoft.AspNetCore.Identity;

namespace CinemaBooking.Controllers
{
    public class ReservationController : Controller
    {
        private Repository.ReservationsRepository _repository;
        private UserManager<IdentityUser> _userManager;
        public ReservationController(ApplicationDbContext dbContext,UserManager<IdentityUser> userManager)
        {
            _repository = new Repository.ReservationsRepository(dbContext);
            _userManager = userManager;
        }
        [Authorize(Roles ="User, Admin")]
        // GET: ReservationController
        public ActionResult Index(Guid userId)
        {
            List<Reservation> reservations;
            if (User.IsInRole("Admin"))
            {
                reservations = _repository.GetAllReservations();
            }
            else
            {
                reservations = _repository.GetReservationsByUserID(userId);
            }
            return View("Index",reservations);
        }

        // GET: ReservationController/Create
        public ActionResult InsertReservation(Guid screeningId)
        {
            ViewBag.ScreeningID = screeningId;
            return View("InsertReservation");
        }

        // POST: ReservationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="User, Admin")]
        public ActionResult InsertReservation(Guid screeningId,IFormCollection collection)
        {
            Guid userId=new Guid(_userManager.GetUserId(HttpContext.User));
            try
            {
                Reservation model = new Reservation();
                if (ModelState.IsValid)
                {
                    var task = TryUpdateModelAsync(model);
                    task.Wait();
                    if (task.Result)
                    {
                        _repository.InsertReservation(screeningId, userId, model);
                    }
                    ViewBag.ReservationID = model.ReservationId;
                    ViewBag.ScreeningIDcopy = screeningId;
                    ViewBag.ShowButtonReservedSeats = true;
                    ViewBag.NumberOfTickets = model.NumberOfSeats;
                    return View("InsertReservation");
                }
                ViewBag.ShowButton = false;
                
                return View("InsertReservation");
            }
            catch
            {
                return View("Index", new { userId });
            }
            return RedirectToAction(nameof(Index));
        }
        // GET: ReservationController/Delete/5
        public ActionResult Delete(Guid reservationId)
        {
            Reservation reservation=_repository.GetReservationByID(reservationId);
            return View("Delete",reservation);
        }

        // POST: ReservationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User, Admin")]
        public ActionResult Delete(Guid reservationId, IFormCollection collection)
        {
            Guid userId = new Guid(_userManager.GetUserId(HttpContext.User));
            try
            {
                _repository.DeleteReservedSeatsByReservationID(reservationId);
                _repository.DeleteReservation(reservationId);
                return RedirectToAction("Index", new {userId});
            }
            catch
            {
                return View("Delete", reservationId);
            }
        }
    }
}
