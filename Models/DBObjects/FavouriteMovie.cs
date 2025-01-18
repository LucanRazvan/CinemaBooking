using System;
using System.Collections.Generic;

namespace CinemaBooking.Models.DBObjects;

public partial class FavouriteMovie
{
    public Guid FavouriteId { get; set; }

    public Guid MovieId { get; set; }

    public Guid UserId { get; set; }

    public virtual Movie? Movie { get; set; }
}
