using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Movies.Context;
using API_Movies.Models;
using MySqlConnector;

namespace API_Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RealisateursController : ControllerBase
    {
        private readonly ApiMovieContext _context;

        public RealisateursController(ApiMovieContext context)
        {
            _context = context;
        }

        // GET: api/Realisateurs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Realisateur>>> GetRealisateurs()
        {
          if (_context.Realisateurs == null)
          {
              return NotFound();
          }
            return await _context.Realisateurs.Include(rea => rea.Personne).ToListAsync();
        }

        // GET: api/Realisateurs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Realisateur>> GetRealisateur(int id)
        {
          if (_context.Realisateurs == null)
          {
              return NotFound();
          }
            var realisateur = await _context.Realisateurs.Include(rea => rea.Personne).SingleOrDefaultAsync(x => x.RealisateurId == id);

            if (realisateur == null)
            {
                return NotFound();
            }

            return realisateur;
        }

        // PUT: api/Realisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRealisateur(int id, Realisateur realisateur)
        {
            if (id != realisateur.RealisateurId)
            {
                return BadRequest();
            }

            AttributePersonne(realisateur);

            _context.Entry(realisateur).State = EntityState.Modified;
            _context.Entry(realisateur.Personne).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RealisateurExists(id))
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

        // POST: api/Realisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Realisateur>> PostRealisateur(Realisateur realisateur)
        {
            if (_context.Realisateurs == null)
            {
                return Problem("Entity set 'ApiMovieContext.Realisateurs'  is null.");
            }

            // Verify and attribute a person if he exists and if the director object have null value on Personne Object
            AttributePersonne(realisateur);

            _context.Realisateurs.Add(realisateur);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRealisateur", new { id = realisateur.RealisateurId }, realisateur);
        }

        // DELETE: api/Realisateurs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRealisateur(int id)
        {
            try
            {
                if (_context.Realisateurs == null)
                {
                    return NotFound();
                }
                var realisateur = await _context.Realisateurs.FindAsync(id);
                if (realisateur == null)
                {
                    return NotFound();
                }

                _context.Realisateurs.Remove(realisateur);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return UnprocessableEntity(ex.InnerException.Message);
            }

            return NoContent();
        }

        private bool RealisateurExists(int id)
        {
            return (_context.Realisateurs?.Any(e => e.RealisateurId == id)).GetValueOrDefault();
        }

        /// <summary>
        /// If Personne object is null, we will check the personneID if it exist on the list of Personnes
        /// </summary>
        /// <param name="realisateur"></param>
        /// <returns></returns>
        private void AttributePersonne(Realisateur realisateur)
        {
            if (realisateur.Personne == null)
            {
                realisateur.Personne = _context.Personnes.FirstOrDefault(personne => personne.PersonneId == realisateur.PersonneId);
            }
        }
    }
}
