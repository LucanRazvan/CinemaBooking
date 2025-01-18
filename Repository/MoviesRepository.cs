using CinemaBooking.Data;
using CinemaBooking.Models.DBObjects;
using CinemaBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaBooking.Repository
{
    public class MoviesRepository
    {
        private ApplicationDbContext dbContext;

        public MoviesRepository()
        {
            this.dbContext = new ApplicationDbContext();
        }
        public MoviesRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<Movie> GetAllMovies()
        {
            List<Movie> moviesList = new List<Movie>();
            foreach (var dbmovie in dbContext.Movies)
            {
                moviesList.Add(dbmovie);
            }
            return moviesList;
        }
        public Movie GetMoviesByID(Guid ID)
        {
            return dbContext.Movies.FirstOrDefault(x => x.MovieId == ID);
        }
        
        public void InsertMovie(Movie movie)
        {
            movie.MovieId = Guid.NewGuid();
            dbContext.Movies.Add(movie);
            dbContext.SaveChanges();
        }
        
        public void UpdateMovie(Movie movie)
        {
            Movie existingMovie = dbContext.Movies.FirstOrDefault(x => x.MovieId == movie.MovieId);
            if (existingMovie != null)
            {
                existingMovie.Title = movie.Title;
                existingMovie.Description = movie.Description;
                existingMovie.ImgURL=movie.ImgURL;
                existingMovie.DurationMinutes = movie.DurationMinutes;
                existingMovie.ReleaseDate = movie.ReleaseDate;

                dbContext.SaveChanges();
            }
        }
        public void DeleteMovie(Guid id)
        {

            Movie existingMovie = dbContext.Movies.FirstOrDefault(x => x.MovieId == id);
            if (existingMovie != null)
            {
                dbContext.Movies.Remove(existingMovie);
                dbContext.SaveChanges();
            }
        }
    }
}
