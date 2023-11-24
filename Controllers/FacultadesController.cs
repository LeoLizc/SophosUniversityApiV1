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
	public class FacultadesController : ControllerBase
	{
		private readonly AppDbContext _context;

		public FacultadesController(AppDbContext context)
		{
			_context = context;
		}

		// GET: api/Facultades
		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<Facultad>),StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<IEnumerable<Facultad>>> GetFacultades(
			[FromQuery(Name = "nombre")] string? nombre
		)
		{
			if (_context.Facultades == null)
			{
				return NotFound();
			}
			var facultades = await _context.Facultades.ToListAsync();

			if (nombre != null)
			{
				facultades = facultades.Where(p => p.Nombre.Contains(nombre)).ToList();
			}

			return facultades;
		}

		// GET: api/Facultades/5
		[HttpGet("{id}")]
		[ProducesResponseType(typeof(Facultad),StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> PutFacultad(int id, UpdateFacultadDTO facultad)
		{
			if (_context.Facultades == null)
			{
				return NotFound();
			}

			var facultadToUpdate = await _context.Facultades.FindAsync(id);
			if (facultadToUpdate == null)
			{
				return NotFound();
			}

			facultadToUpdate.Nombre = facultad.Nombre ?? facultadToUpdate.Nombre;

			_context.Entry(facultadToUpdate).State = EntityState.Modified;

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
					return Problem("Erro al actualizar Facultad");
				}
			}

			return NoContent();
		}

		// POST: api/Facultades
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		[ProducesResponseType(typeof(Facultad),StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<Facultad>> PostFacultad(CreateFacultadDTO facultad)
		{
			if (_context.Facultades == null)
			{
				return Problem("Entity set 'AppDbContext.Facultads'  is null.");
			}

			var nuevaFacultad = new Facultad
			{
				Nombre = facultad.Nombre
			};

			_context.Facultades.Add(nuevaFacultad);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetFacultad", new { id = nuevaFacultad.IdFacultad }, nuevaFacultad);
		}

		// DELETE: api/Facultades/5
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
