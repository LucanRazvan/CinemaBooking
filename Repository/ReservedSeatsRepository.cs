using CinemaBooking.Data;
using CinemaBooking.Models.DBObjects;
using CinemaBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaBooking.Repository
{
    public class ReservedSeatsRepository
    {
        private ApplicationDbContext dbContext;

        public ReservedSeatsRepository()
        {
            this.dbContext = new ApplicationDbContext();
        }
        public ReservedSeatsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<Reservation> GetAllReservationsFromHallByHallID(Guid hallId)
        {
            List<Reservation> reservations = new List<Reservation>();
            foreach(Reservation dbreservation in dbContext.Screenings.Where(h => h.HallId == hallId).SelectMany(r => r.Reservations))
            {
                reservations.Add(dbreservation);
            }
            return reservations;
        }

        public List<ReservedSeat> GetAllReservedSeatsByScreeningID(Guid screeningId)
        {
            List<ReservedSeat> reservedseatsList = new List<ReservedSeat>();
            foreach (ReservedSeat dbreservedseat in dbContext.ReservedSeats.Include(r=>r.Reservation).ThenInclude(s=>s.Screening).Where(rs => rs.Reservation.Screening.ScreeningId==screeningId))
            {
                reservedseatsList.Add(dbreservedseat);
            }
            return reservedseatsList;
        }
        public void InsertReservedSeat(Guid reservationId,ReservedSeat reservedSeat,int seatNr)
        {
            reservedSeat.ReservedSeatId = Guid.NewGuid();
            reservedSeat.ReservationId = reservationId;
            reservedSeat.SeatNumber= seatNr;
            dbContext.ReservedSeats.Add(reservedSeat);
            dbContext.SaveChanges();
        }
        public int GetHallCapacityByReservationID(Guid ID)
        {
            Reservation reservation=dbContext.Reservations.Include(s=>s.Screening).ThenInclude(h=>h.Hall).FirstOrDefault(rs=>rs.ReservationId==ID);
            int capacity = reservation.Screening.Hall.Capacity;
            return capacity;
        }
        public Guid GetHallIDByReservationID(Guid ID)
        {
			Reservation reservation = dbContext.Reservations.Include(s => s.Screening).ThenInclude(h => h.Hall).FirstOrDefault(rs => rs.ReservationId == ID);
			Guid hallId = reservation.Screening.Hall.HallId;
			return hallId;
		}
    }
}
