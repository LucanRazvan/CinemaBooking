using CinemaBooking.Data;
using CinemaBooking.Models.DBObjects;
using CinemaBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaBooking.Repository
{
    public class ReservationsRepository
    {
        private ApplicationDbContext dbContext;

        public ReservationsRepository()
        {
            this.dbContext = new ApplicationDbContext();
        }
        public ReservationsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<Reservation> GetAllReservations()
        {
            List<Reservation> reservationsList = new List<Reservation>();
            foreach (var dbReservation in dbContext.Reservations.Include(s => s.ReservedSeats).Include(s => s.Screening).Include(m => m.Screening.Movie).Include(h => h.Screening.Hall).Include(c => c.Screening.Hall.Cinema))
            {
                reservationsList.Add(dbReservation);
            }
            return reservationsList;
        }
        public List<Reservation> GetReservationsByUserID(Guid ID)
        {
            List<Reservation> reservationsList = new List<Reservation>();
            foreach (var dbReservation in dbContext.Reservations.Include(s => s.ReservedSeats).Where(x => x.UserId == ID).Include(s=>s.Screening).Include(m=>m.Screening.Movie).Include(h=>h.Screening.Hall).Include(c=>c.Screening.Hall.Cinema))
            {
                reservationsList.Add(dbReservation);
            }
            return reservationsList;
        }
        public Reservation GetReservationByID(Guid ID)
        {
            Reservation reservation = dbContext.Reservations
                .Include(s => s.Screening)
                .ThenInclude(m=>m.Movie)
                .FirstOrDefault(r => r.ReservationId == ID);
            return reservation;
        }

        public void InsertReservation(Guid screeningId,Guid userId,Reservation reservation)
        {
            reservation.ReservationId = Guid.NewGuid();
            reservation.ScreeningId = screeningId;
            reservation.UserId = userId;
            dbContext.Reservations.Add(reservation);
            dbContext.SaveChanges();
        }
        public void DeleteReservation(Guid ID)
        {
            Reservation existingReservation = dbContext.Reservations.FirstOrDefault(x => x.ReservationId == ID);
            if (existingReservation != null)
            {
                dbContext.Reservations.Remove(existingReservation);
                dbContext.SaveChanges();
            }
        }
        public void DeleteReservedSeatsByReservationID(Guid ID)
        {
            foreach (ReservedSeat reservedSeat in dbContext.ReservedSeats.Where(r => r.ReservationId == ID))
            {
                dbContext.ReservedSeats.Remove(reservedSeat);
                dbContext.SaveChanges();
            }
        }

    }
}
