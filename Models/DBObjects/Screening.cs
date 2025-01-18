using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinemaBooking.Models.DBObjects;

public partial class Screening
{
    public Guid ScreeningId { get; set; }

    public Guid MovieId { get; set; }

    public Guid HallId { get; set; }
    [Required(ErrorMessage ="Please provide the screening time!")]
    [DataType(DataType.DateTime)]
    public DateTime ScreeningTime { get; set; }
    [Required(ErrorMessage ="Please provide the price of the ticket!")]
    public decimal Price { get; set; }

    public virtual Hall? Hall { get; set; }

    public virtual Movie? Movie { get; set; }

    public virtual ICollection<Reservation>? Reservations { get; set; }
}
