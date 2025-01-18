using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinemaBooking.Models.DBObjects;

public partial class Reservation
{
    public Guid ReservationId { get; set; }

    public Guid ScreeningId { get; set; }

    public Guid UserId { get; set; }
    [Required(ErrorMessage ="Please provide the number of seats you wish to reserve!")]
    public int NumberOfSeats { get; set; }

    public virtual ICollection<ReservedSeat>? ReservedSeats { get; set; }

    public virtual Screening? Screening { get; set; }
}
