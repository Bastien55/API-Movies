using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API_Movies.Models;

public partial class Cast
{
    public int CastId { get; set; }
    
    public int? ActeurId { get; set; }

    public int? RealisateurId { get; set; }

    public virtual Acteur? Acteur { get; set; }

    [JsonIgnore]
    public virtual ICollection<Film> Films { get; set; } = new List<Film>();

    public virtual Realisateur? Realisateur { get; set; }
}
