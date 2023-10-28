using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Movies.Context;
using API_Movies.Models;
using System.Collections;
using Microsoft.EntityFrameworkCore.Query;
using MySqlConnector;
using API_Movies.Models.DTO;

namespace API_Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmsController : ControllerBase
    {

        private readonly ApiMovieContext _context;

        public FilmsController(ApiMovieContext context)
        {
            _context = context;
        }

        // GET: api/Films
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Film>>> GetFilms()
        {
            if (_context.Films == null)
            {
                return NotFound();
            }

            var listOfFilm = await LoadedEntireData();
            return Ok(listOfFilm);
        }

        /// <summary>
        /// A version of the film controller with Hyperlinks ressource
        /// </summary>
        /// <returns></returns>
        [HttpGet("hyperlinks")]
        public async Task<ActionResult<IEnumerable<FilmDTO>>> GetFilmsHyperlinked()
        {
            if(_context.Films == null)
                return NotFound();

            var listOfFilm = await LoadedEntireData();
            var listOfDto = new List<FilmDTO>();

            foreach (var film in listOfFilm)
            {
                var dto = new FilmDTO()
                {
                    Nom = film.Nom,
                    Description = film.Description,
                    DateDeParution = film.DateDeParution
                };

                AddLinks(dto, film);

                listOfDto.Add(dto);
            }

            return Ok(listOfDto);
        }

        // GET: api/Films/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Film>> GetFilm(int id)
        {
            if (_context.Films == null)
            {
                return NotFound();
            }
            var film = LoadedEntireData().Result.FirstOrDefault(f => f.FilmId == id);

            if (film == null)
            {
                return NotFound();
            }

            return film;
        }

        // GET: api/Films/filter
        /// <summary>
        /// Method that allow to split films depending of actors id and directors id
        /// </summary>
        /// <param name="actorIds">List that contains several ID of actors</param>
        /// <param name="directorIds">List that contains several ID of directors</param>
        /// <returns>A splitted film list</returns>
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Film>>> GetFilmsFiltered([FromQuery] List<int> actorIds, [FromQuery] List<int> directorIds)
        {
            //We load the entire data of films list
            var query = await LoadedEntireData();

            if (actorIds != null && actorIds.Any())
            {
                // We split the list where we have actors containing in the film list
                query = query.Where(film => film.Cast.Acteur != null && actorIds.Contains(film.Cast.Acteur.ActeurId));
            }

            if (directorIds != null && directorIds.Any())
            {
                // Same for directors
                query = query.Where(film => film.Cast.Realisateur != null && directorIds.Contains(film.Cast.Realisateur.RealisateurId));
            }

            var films = query.ToList();
            return Ok(films);
        }

        // PUT: api/Films/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilm(int id, Film film)
        {
            if (id != film.FilmId)
            {
                return BadRequest();
            }

            var oldFilm = _context.Films.FirstOrDefault(f => f.FilmId == id);
            if (oldFilm == null)
                return BadRequest("Film non trouvé");

            _context.Entry(oldFilm).State = EntityState.Detached;

            film.Nom = oldFilm.Nom;
            film.Description = oldFilm.Description;
            film.DateDeParution = oldFilm.DateDeParution;
            film.Cast = oldFilm.Cast ?? _context.Casts.Find(film.CastId);
            
            //Allow to do an update command for the object
            _context.Entry(film).State = EntityState.Modified;
            _context.Entry(film.Cast).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Films
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Film>> PostFilm(Film film)
        {
          if (_context.Films == null)
          {
              return Problem("Entity set 'ApiMovieContext.Films'  is null.");
          }
            _context.Films.Add(film);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFilm", new { id = film.FilmId }, film);
        }

        // DELETE: api/Films/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilm(int id)
        {

            try
            {
                if (_context.Films == null)
                {
                    return NotFound();
                }
                var film = await _context.Films.FindAsync(id);
                if (film == null)
                {
                    return NotFound();
                }

                _context.Films.Remove(film);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return UnprocessableEntity(ex.InnerException.Message);
            }

            return NoContent();
        }

        private bool FilmExists(int id)
        {
            return (_context.Films?.Any(e => e.FilmId == id)).GetValueOrDefault();
        }

        /// <summary>
        /// Method that allow to load "Cast" Object in Film and to load Actors / Directors in the cast object
        /// </summary>
        /// <returns>A list of film with actors and directors</returns>
        private async Task<IEnumerable<Film>> LoadedEntireData()
        {
            return await _context.Films
                                 .Include(film => film.Cast) //Include Allow to load data of the foreign key from the data model film
                                    .ThenInclude(cast => cast.Acteur) //Then Include enter in the object we loaded to load another object in the loaded object
                                        .ThenInclude(actor => actor.Personne)
                                 .Include(film => film.Cast)
                                    .ThenInclude(cast => cast.Realisateur)
                                        .ThenInclude(rea => rea.Personne)
                                 .ToListAsync();
        }

        /// <summary>
        /// Allow to add hyperlinks for the acteurs and directors ressources
        /// </summary>
        /// <param name="film"></param>
        private void AddLinks(FilmDTO dto, Film film)
        {
            if(film != null)
            {
                dto.Hyperlinks.Add(new Hyperlink()
                {
                    Relation = "Acteurs",
                    Href = $"http://localhost:5206/api/Acteurs/{film.Cast.ActeurId}"
                });

                dto.Hyperlinks.Add(new Hyperlink()
                {
                    Relation = "Realisateurs",
                    Href = $"http://localhost:5206/api/Realisateurs/{film.Cast.RealisateurId}"
                });
            }
        }
    }
}
