using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API_Movies.Models;

public partial class Personne
{
    public int PersonneId { get; set; }

    public string? Nom { get; set; }

    public string? Prenom { get; set; }

    public DateOnly? DateNaissance { get; set; }

    [JsonIgnore]
    public virtual ICollection<Acteur> Acteurs { get; set; } = new List<Acteur>();

    [JsonIgnore]
    public virtual ICollection<Realisateur> Realisateurs { get; set; } = new List<Realisateur>();
}
