using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Movies.Context;
using API_Movies.Models;

namespace API_Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActeursController : ControllerBase
    {
        private readonly ApiMovieContext _context;

        public ActeursController(ApiMovieContext context)
        {
            _context = context;
        }

        // GET: api/Acteurs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Acteur>>> GetActeurs()
        {
            if (_context.Acteurs == null)
            {
                return NotFound();
            }

            return await _context.Acteurs.Include(actor => actor.Personne).ToListAsync();
        }

        // GET: api/Acteurs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Acteur>> GetActeur(int id)
        {
            if (_context.Acteurs == null)
            {
                return NotFound();
            }

            var actor = await _context.Acteurs.Include(actor => actor.Personne).SingleOrDefaultAsync(a => a.ActeurId == id);

            if(actor == null)
            {
                return NotFound();
            }

            return actor;
        }

        // PUT: api/Acteurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActeur(int id, Acteur acteur)
        {
            if (id != acteur.ActeurId)
            {
                return BadRequest();
            }

            AttributePersonne(acteur);

            _context.Entry(acteur).State = EntityState.Modified; // Set its state to modified
            _context.Entry(acteur.Personne).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActeurExists(id))
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

        // POST: api/Acteurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Acteur>> PostActeur(Acteur acteur)
        {
            if (_context.Acteurs == null)
            {
                return Problem("Entity set 'ApiMovieContext.Acteurs'  is null.");
            }

            AttributePersonne(acteur);
            _context.Acteurs.Add(acteur);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActeur", new { id = acteur.ActeurId }, acteur);
        }

        // DELETE: api/Acteurs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActeur(int id)
        {
            try
            {
                if (_context.Acteurs == null)
                {
                    return NotFound();
                }
                var acteur = await _context.Acteurs.FindAsync(id);
                if (acteur == null)
                {
                    return NotFound();
                }

                _context.Acteurs.Remove(acteur);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException ex)
            {
                return UnprocessableEntity(ex.InnerException.Message);
            }

            return NoContent();
        }

        private bool ActeurExists(int id)
        {
            return (_context.Acteurs?.Any(e => e.ActeurId == id)).GetValueOrDefault();
        }

        /// <summary>
        /// If Personne object is null, we will check the personneID if it exist on the list of Personnes
        /// </summary>
        /// <param name="realisateur"></param>
        /// <returns></returns>
        private void AttributePersonne(Acteur actor)
        {
            if (actor.Personne == null)
            {
                actor.Personne = _context.Personnes.FirstOrDefault(personne => personne.PersonneId == actor.PersonneId);
            }
        }
    }
}
