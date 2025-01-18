using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinemaBooking.Models.DBObjects;

public partial class Hall
{
    public Guid HallId { get; set; }

    public Guid CinemaId { get; set; }

    public int HallNumber { get; set; }

    [Required(ErrorMessage = "Please provide the capacity of the hall!")]
    [Range(20, 50, ErrorMessage = "The capacity must be a number between 20 and 50!")]
    public int Capacity { get; set; }

    public virtual Cinema? Cinema { get; set; }

    public virtual ICollection<Screening>? Screenings { get; set; }
}
