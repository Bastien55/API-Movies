using API_Movies.Context;
using API_Movies.Models;
using API_Movies.Repository.Interface;

namespace API_Movies.Repository
{
    /// <summary>
    /// Class that represent the repository to modify the table film_personne in the database
    /// </summary>
    public class FilmPersonneRepository : BaseRepository, IRepositoryService<FilmPersonne>
    {
        public FilmPersonneRepository(ApiMovieContext context) : base(context) { }

        public void Delete(FilmPersonne entity)
        {
            _dbContext.FilmPersonnes.Remove(GetElement(entity));
            _dbContext.SaveChanges();
        }

        public IEnumerable<FilmPersonne> GetAll()
        {
            return _dbContext.FilmPersonnes.ToList();
        }

        public FilmPersonne GetByID(int id)
        {
            return null;
        }

        public void Save(FilmPersonne entity)
        {
            if (_dbContext.FilmPersonnes.Contains(GetElement(entity)))
                _dbContext.FilmPersonnes.Update(entity);
            else
                _dbContext.FilmPersonnes.Add(entity);

            _dbContext.SaveChanges();
        }

        private FilmPersonne GetElement(FilmPersonne entity)
        {
            return _dbContext.FilmPersonnes.First(fp => (fp.FilmId == entity.FilmId) && fp.PersonneId == entity.PersonneId);
        }
    }
}
