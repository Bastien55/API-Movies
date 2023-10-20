using System;
using System.Collections.Generic;

namespace API_Movies.Models;

public partial class Film
{
    public int FilmId { get; set; }

    public string? Nom { get; set; }

    public string Description { get; set; } = null!;

    public DateOnly DateDeParution { get; set; }
}
