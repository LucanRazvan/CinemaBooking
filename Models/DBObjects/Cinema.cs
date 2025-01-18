using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinemaBooking.Models.DBObjects;

public partial class Cinema
{
    public Guid CinemaId { get; set; }
    [Required(ErrorMessage ="The name of the cinema is required!")]
    [MaxLength(100)]
    public string Name { get; set; } = null!;
    [Required(ErrorMessage ="The adress of the cinema is required!")]
    [MaxLength(255)]
    public string Address { get; set; } = null!;
    [Required(ErrorMessage ="The city of the cinema is required!")]
    [MaxLength(100)]
    public string City { get; set; } = null!;

    public virtual ICollection<Hall>? Halls { get; set; }
}
