using CinemaBooking.Data;
using CinemaBooking.Models;
using CinemaBooking.Models.DBObjects;
using Microsoft.EntityFrameworkCore;

namespace CinemaBooking.Repository
{
    public class CinemasRepository
    {
        private ApplicationDbContext dbContext;

        public CinemasRepository()
        {
            this.dbContext = new ApplicationDbContext();
        }
        public CinemasRepository(ApplicationDbContext dbContext) { 
        this.dbContext=dbContext;
        }
        public List<Cinema> GetAllCinemasWithHalls()
        {
            List<Cinema> cinemaList = new List<Cinema>();
            foreach(Cinema cinema in dbContext.Cinemas.Include(h => h.Halls))
            {
                cinemaList.Add(cinema);
            }
            return cinemaList;
        }
        public Cinema GetCinemaByID(Guid ID)
        {
            return dbContext.Cinemas.FirstOrDefault(x=>x.CinemaId==ID);
        }
        public void InsertCinema(Cinema cinema)
        {
            cinema.CinemaId = Guid.NewGuid();
            dbContext.Cinemas.Add(cinema);
            dbContext.SaveChanges();
        }
        public void UpdateCinema(Cinema cinema)
        {
            Cinema existingCinema = dbContext.Cinemas.FirstOrDefault(x => x.CinemaId == cinema.CinemaId);
            if (existingCinema != null)
            {
                existingCinema.Name = cinema.Name;
                existingCinema.Address = cinema.Address;
                existingCinema.City = cinema.City;
                dbContext.SaveChanges();
            }
        }
    }
}
