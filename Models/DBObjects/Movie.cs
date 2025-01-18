using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinemaBooking.Models.DBObjects;

public partial class Movie
{
    public Guid MovieId { get; set; }
    [Required(ErrorMessage ="Please provide the title of the movie!")]
    [MaxLength(100)]
    public string? Title { get; set; }
    [Required(ErrorMessage ="Please provide the image adress for the movie!")]
    public string? ImgURL { get; set; }
    [Required(ErrorMessage ="Please provide the description of the movie!")]
    [MaxLength(500)]
    public string? Description { get; set; }

    [Required(ErrorMessage ="Please provide the movie length in minutes!")]
    [Range(30, 240,ErrorMessage ="The movie length must be a number between 30 and 240!")]
    public int DurationMinutes { get; set; }

    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }

    public virtual ICollection<FavouriteMovie>? FavouriteMovies { get; set; }

    public virtual ICollection<Screening> Screenings { get; set; } = new List<Screening>();
}
