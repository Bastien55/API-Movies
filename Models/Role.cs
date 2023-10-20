using System;
using System.Collections.Generic;

namespace API_Movies.Models;

public partial class Role
{
    public int PersonneId { get; set; }

    public int FilmId { get; set; }

    public string? Role1 { get; set; }

    public virtual Film Film { get; set; } = null!;

    public virtual Personne Personne { get; set; } = null!;
}
