using System;
using System.Collections.Generic;

namespace CinemaManager_zaineb.Models.Cinema;

public partial class Table
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Nationality { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
