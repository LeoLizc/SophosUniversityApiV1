using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SophosUniversityApi.DataContracts;
using SophosUniversityApi.DBContext;
using SophosUniversityApi.Models;

namespace SophosUniversityApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class InscripcionesController : ControllerBase
	{
		private readonly AppDbContext _context;

		public InscripcionesController(AppDbContext context)
		{
			_context = context;
		}

		// POST: api/Inscripciones
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Inscripcion>> PostInscripcion(CreateInscripcionDTO inscripcion)
		{
			if (_context.Inscripciones == null)
			{
				return Problem("Entity set 'AppDbContext.Inscripcions'  is null.");
			}

			var newInscripcion = new Inscripcion
			{
				IdCurso = inscripcion.IdCurso,
				IdEstudiante = inscripcion.IdEstudiante
			};

			_context.Inscripciones.Add(newInscripcion);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetInscripcion", new { id = newInscripcion.IdInscripcion }, newInscripcion);
		}

		// DELETE: api/Inscripciones/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteInscripcion(int id)
		{
			if (_context.Inscripciones == null)
			{
				return NotFound();
			}
			var inscripcion = await _context.Inscripciones.FindAsync(id);
			if (inscripcion == null)
			{
				return NotFound();
			}

			_context.Inscripciones.Remove(inscripcion);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool InscripcionExists(int id)
		{
			return (_context.Inscripciones?.Any(e => e.IdInscripcion == id)).GetValueOrDefault();
		}
	}
}
