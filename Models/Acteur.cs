using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API_Movies.Models;

public partial class Acteur
{
    public int ActeurId { get; set; }

    public int PersonneId { get; set; }

    [JsonIgnore]
    public virtual ICollection<Cast> Casts { get; set; } = new List<Cast>();

    public virtual Personne? Personne { get; set; }
}
