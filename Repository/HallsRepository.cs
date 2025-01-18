using CinemaBooking.Data;
using CinemaBooking.Models.DBObjects;
using CinemaBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaBooking.Repository
{
    public class HallsRepository
    {
        private ApplicationDbContext dbContext;

        public HallsRepository()
        {
            this.dbContext = new ApplicationDbContext();
        }
        public HallsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public Hall GetHallsByID(Guid ID)
        {
            return dbContext.Halls.FirstOrDefault(x => x.HallId == ID);
        }
        public void InsertHall(Guid id,Hall hall)
        {
            hall.HallId = Guid.NewGuid();
            hall.CinemaId = id;
            dbContext.Halls.Add(hall);
            dbContext.SaveChanges();
        }
        public List<Hall> GetHallsByCinemaID(Guid cinemaId)
        {
            List<Hall> hallList = new List<Hall>();
            foreach (var dbHall in dbContext.Halls.Where(x => x.CinemaId == cinemaId))
            {
                hallList.Add(dbHall);
            }
            return hallList;
        }
        public void UpdateHall(Hall hall)
        {
            Hall existingHall = dbContext.Halls.FirstOrDefault(x => x.HallId == hall.HallId);
            if (existingHall != null)
            {
                existingHall.HallNumber = hall.HallNumber;
                existingHall.Capacity = hall.Capacity;
                dbContext.SaveChanges();
            }
        }
        public void DeleteHall(Guid id)
        {

            Hall existingHall = dbContext.Halls.FirstOrDefault(x => x.HallId == id);
            if (existingHall != null)
            {
                dbContext.Halls.Remove(existingHall);
                dbContext.SaveChanges();
            }
        }
    }
}

