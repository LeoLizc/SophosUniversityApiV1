using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SophosUniversityApi.DBContext;
using SophosUniversityApi.Models;

namespace SophosUniversityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultadesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FacultadesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Facultades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Facultad>>> GetFacultads()
        {
          if (_context.Facultades == null)
          {
              return NotFound();
          }
            return await _context.Facultades.ToListAsync();
        }

        // GET: api/Facultades/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Facultad>> GetFacultad(int id)
        {
          if (_context.Facultades == null)
          {
              return NotFound();
          }
            var facultad = await _context.Facultades.FindAsync(id);

            if (facultad == null)
            {
                return NotFound();
            }

            return facultad;
        }

        // PUT: api/Facultades/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFacultad(int id, Facultad facultad)
        {
            if (id != facultad.IdFacultad)
            {
                return BadRequest();
            }

            _context.Entry(facultad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacultadExists(id))
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

        // POST: api/Facultades
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Facultad>> PostFacultad(Facultad facultad)
        {
          if (_context.Facultades == null)
          {
              return Problem("Entity set 'AppDbContext.Facultads'  is null.");
          }
            _context.Facultades.Add(facultad);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFacultad", new { id = facultad.IdFacultad }, facultad);
        }

        // DELETE: api/Facultades/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFacultad(int id)
        {
            if (_context.Facultades == null)
            {
                return NotFound();
            }
            var facultad = await _context.Facultades.FindAsync(id);
            if (facultad == null)
            {
                return NotFound();
            }

            _context.Facultades.Remove(facultad);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FacultadExists(int id)
        {
            return (_context.Facultades?.Any(e => e.IdFacultad == id)).GetValueOrDefault();
        }
    }
}
