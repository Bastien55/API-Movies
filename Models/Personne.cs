using System;
using System.Collections.Generic;

namespace API_Movies.Models;

public partial class Personne
{
    public int PersonneId { get; set; }

    public string? Nom { get; set; }

    public string? Prenom { get; set; }

    public DateOnly? DateNaissance { get; set; }
}
