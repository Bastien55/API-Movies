using API_Movies.Context;
using API_Movies.Models;
using API_Movies.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace API_Movies.Repository
{
    /// <summary>
    /// Class that represent the film table in database
    /// </summary>
    public class FilmRepository : BaseRepository, IRepositoryService<Film>
    {
        public FilmRepository(ApiMovieContext context) : base(context) { }

        public void Delete(Film entity)
        {
            _dbContext.Films.Remove(GetByID(entity.FilmId));
            _dbContext.SaveChanges();
        }

        public IEnumerable<Film> GetAll()
        {
            return _dbContext.Films.ToList();
        }

        public Film GetByID(int id)
        {
            var result = _dbContext.Films.First(film => film.FilmId == id);
            return result;
        }

        public void Save(Film entity)
        {
            if(_dbContext.Films.Any(film => film.FilmId == entity.FilmId))
                _dbContext.Films.Update(entity);
            else
                _dbContext.Films.Add(entity);

            _dbContext.SaveChanges();
        }
    }
}
