using CinemaBooking.Data;
using CinemaBooking.Models.DBObjects;
using CinemaBooking.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CinemaBooking.Repository
{
    public class ScreeningsRepository
    {
        private ApplicationDbContext dbContext;

        public ScreeningsRepository()
        {
            this.dbContext = new ApplicationDbContext();
        }
        public ScreeningsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public Screening GetScreeningsByID(Guid ID)
        {
            return dbContext.Screenings.FirstOrDefault(x => x.ScreeningId == ID);
        }
        public List<Screening> GetScreeningsByMovieID(Guid movieId)
        {
            List<Screening> screeningList = new List<Screening>();
            foreach (var dbScreening in dbContext.Screenings.Where(x => x.MovieId == movieId).Include(h=>h.Hall).ThenInclude(c=>c.Cinema).Include(m=>m.Movie))
            {
                screeningList.Add(dbScreening);
            }
            return screeningList;
        }
        
        public void InsertScreening(Guid movieId,Guid hallId,Screening screening)
        {
            screening.ScreeningId = Guid.NewGuid();
            screening.MovieId = movieId;
            screening.HallId = hallId;
            dbContext.Screenings.Add(screening);
            dbContext.SaveChanges();
        }
        public Guid GetHallIdByCinemaAndNumber(string cinemaName, int hallNumber)
        {
            Hall hall = dbContext.Halls
                .Include(h => h.Cinema)
                .FirstOrDefault(h => h.Cinema.Name == cinemaName && h.HallNumber == hallNumber);
            return hall.HallId;
        }
        public void UpdateScreening(Screening Screening,string cinemaName,int hallNumber)
        {
            Screening existingScreening = dbContext.Screenings.FirstOrDefault(x => x.ScreeningId == Screening.ScreeningId);
            if (existingScreening != null)
            {
                existingScreening.HallId = GetHallIdByCinemaAndNumber(cinemaName,hallNumber);
                existingScreening.ScreeningTime = Screening.ScreeningTime;
                existingScreening.Price = Screening.Price;
                dbContext.SaveChanges();
            }
        }
        public void DeleteScreening(Guid id)
        {

            Screening existingScreening = dbContext.Screenings.FirstOrDefault(x => x.ScreeningId == id);
            if (existingScreening != null)
            {
                dbContext.Screenings.Remove(existingScreening);
                dbContext.SaveChanges();
            }
        }
    }
}

