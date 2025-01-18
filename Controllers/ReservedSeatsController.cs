using CinemaBooking.Data;
using CinemaBooking.Models.DBObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CinemaBooking.Controllers
{
    public class ReservedSeatsController : Controller
    {
        private Repository.ReservedSeatsRepository _repository;
        public ReservedSeatsController(ApplicationDbContext dbContext)
        {
            _repository = new Repository.ReservedSeatsRepository(dbContext);
        }
        [Authorize(Roles = "Admin,User")]
        // GET: ReservedSeatsController
        public ActionResult Index(Guid reservationId,Guid screeningId,int numberOfTickets)
        {
            Guid hallId=_repository.GetHallIDByReservationID(reservationId);

            var reservedSeats=_repository.GetAllReservedSeatsByScreeningID(screeningId);
            ViewBag.NumberOfSeats =_repository.GetHallCapacityByReservationID(reservationId);
            ViewBag.NumberOfTickets = numberOfTickets;
            ViewBag.ReservationID =reservationId;
            return View("Index",reservedSeats);
        }
        // GET: ReservedSeatsController/Create
        public ActionResult InsertReservedSeat(Guid reservationId,string selectedSeats)
        {
            ViewBag.ReservationID = reservationId;

            ViewBag.SelectedSeats = selectedSeats;

            return View("InsertReservedSeat");
        }

        // POST: ReservedSeatsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User, Admin")]
        public ActionResult InsertReservedSeat(Guid reservationId,IFormCollection collection)
        {
            string selectedSeats = collection["Selected_seats"];
            List<int> seatsArray = selectedSeats.Split(",").Select(int.Parse).ToList();

            try
            {
                ReservedSeat model=new ReservedSeat();
                    foreach (var seat in seatsArray)
                    {
                        _repository.InsertReservedSeat(reservationId, model, seat);
                    }
                return View("Succes");
            }
            catch
            {
                return View();
            }
        }
    }
}
