using API_Movies.Context;

namespace API_Movies.Repository
{
    /// <summary>
    /// Class that represent the base repository with the database context
    /// </summary>
    public class BaseRepository
    {
        protected ApiMovieContext _dbContext;

        public BaseRepository(ApiMovieContext context) { _dbContext = context; }
    }
}
