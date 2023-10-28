namespace API_Movies.Models
{
    /// <summary>
    /// Class that represent an Hyperlink for the ressources in API
    /// </summary>
    public class Hyperlink
    {
        /// <summary>
        /// Name of the ressources
        /// </summary>
        public string Relation { get; set; }

        /// <summary>
        /// The link of the ressource
        /// </summary>
        public string Href { get; set; }
    }
}
