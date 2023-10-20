using API_Movies.Context;
using API_Movies.Models;
using API_Movies.Repository.Interface;

namespace API_Movies.Repository
{
    /// <summary>
    /// Class that represent the modification of "personnes" table
    /// </summary>
    public class PersonneRepository : BaseRepository, IRepositoryService<Personne>
    {
        public PersonneRepository(ApiMovieContext context) : base(context) { }

        public void Delete(Personne entity)
        {
            _dbContext.Personnes.Remove(GetByID(entity.PersonneId));
            _dbContext.SaveChanges();
        }

        public IEnumerable<Personne> GetAll()
        {
            return _dbContext.Personnes.ToList();
        }

        public Personne GetByID(int id)
        {
            return _dbContext.Personnes.First(personne => personne.PersonneId == id);
        }

        public void Save(Personne entity)
        {
            if(_dbContext.Personnes.Any(personne => personne.PersonneId == entity.PersonneId))
                _dbContext.Personnes.Update(entity);
            else
                _dbContext.Personnes.Add(entity);

            _dbContext.SaveChanges();
        }
    }
}
