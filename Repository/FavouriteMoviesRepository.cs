using CinemaBooking.Data;
using CinemaBooking.Models.DBObjects;
using CinemaBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaBooking.Repository
{
    public class FavouriteMoviesRepository
    {
        private ApplicationDbContext dbContext;

        public FavouriteMoviesRepository()
        {
            this.dbContext = new ApplicationDbContext();
        }
        public FavouriteMoviesRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public FavouriteMovie GetFavouriteMoviesByID(Guid ID)
        {
            return dbContext.FavouriteMovies.Include(m=>m.Movie).FirstOrDefault(x => x.FavouriteId == ID);
        }
        
        public List<FavouriteMovie> GetFavouriteMoviesByUserID(Guid userId)
        {
            List<FavouriteMovie> favouriteMovieList = new List<FavouriteMovie>();
            foreach (var dbfavouriteMovie in dbContext.FavouriteMovies.Where(x => x.UserId == userId).Include(m=>m.Movie))
            {
                favouriteMovieList.Add(dbfavouriteMovie);
            }
            return favouriteMovieList;
        }
        public void InsertFavouriteMovie(Guid movieId,Guid userId, FavouriteMovie favouriteMovie)
        {
            favouriteMovie.FavouriteId = Guid.NewGuid();
            favouriteMovie.MovieId = movieId;
            favouriteMovie.UserId = userId;
            dbContext.FavouriteMovies.Add(favouriteMovie);
            dbContext.SaveChanges();
        }
        public Movie GetMovieDetailsByMovieID(Guid movieId)
        {
            return dbContext.Movies.FirstOrDefault(fm=>fm.MovieId==movieId);
        }
        public void DeleteFavouriteMovieByID(Guid ID)
        {
            FavouriteMovie existingFavouriteMovie = dbContext.FavouriteMovies.FirstOrDefault(x => x.FavouriteId == ID);
            if (existingFavouriteMovie != null)
            {
                dbContext.FavouriteMovies.Remove(existingFavouriteMovie);
                dbContext.SaveChanges();
            }
        }
    }
}
