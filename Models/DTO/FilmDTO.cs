using System.ComponentModel.DataAnnotations.Schema;

namespace API_Movies.Models.DTO
{
    /// <summary>
    /// A version of Film with Hyperlinks
    /// </summary>
    public class FilmDTO
    {

        public int FilmId { get; set; }

        public string? Nom { get; set; }

        public string? Description { get; set; } = null!;

        public DateOnly? DateDeParution { get; set; }

        public virtual List<Hyperlink> Hyperlinks { get; set; } = new List<Hyperlink>();
    }
}
