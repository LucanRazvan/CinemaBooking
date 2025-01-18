using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinemaBooking.Models.DBObjects;

public partial class ReservedSeat
{
    public Guid ReservedSeatId { get; set; }

    public Guid ReservationId { get; set; }
    [Required(ErrorMessage ="Please provide the number of the reserved seat!")]
    [Range(1,40,ErrorMessage ="The seat number must be a number between 1 and 40!")]
    public int SeatNumber { get; set; }

    public virtual Reservation? Reservation { get; set; }
}
