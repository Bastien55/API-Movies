using System;
using System.Collections.Generic;

namespace API_Movies.Models;

public partial class FilmPersonne
{
    public int PersonneId { get; set; }

    public int FilmId { get; set; }

    public string? Role { get; set; }

    public virtual Film Film { get; set; } = null!;

    public virtual Personne Personne { get; set; } = null!;
}
