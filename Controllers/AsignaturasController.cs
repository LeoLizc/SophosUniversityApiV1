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
	public class AsignaturasController : ControllerBase
	{
		private readonly AppDbContext _context;

		public AsignaturasController(AppDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Get a list of alumnos with optional filtering by name and facultad.
		/// </summary>
		/// <param name="name">The name of the alumno to filter by.</param>
		/// <param name="facultad">The facultad to filter by.</param>
		/// <returns>A list of alumnos.</returns>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<AsignaturaDTO>>> GetAsignaturas(
			[FromQuery(Name = "nombre")] string? name
		)
		{
			if (_context.Asignaturas == null)
			{
				return NotFound();
			}
			var asignaturas = await _context.Asignaturas.ToListAsync();

			if (name != null)
			{
				asignaturas = asignaturas.Where(a => a.Nombre.Contains(name)).ToList();
			}

			var result = asignaturas.Select(
				a => new AsignaturaDTO(
					a.IdAsignatura, 
					a.Nombre, 
					a.Creditos
				)
			);
			return Ok( result );
		}

		// GET: api/Asignaturas/5
		[HttpGet("{id}")]
		public async Task<ActionResult<AsignaturaDetailDTO>> GetAsignatura(int id)
		{
			if (_context.Asignaturas == null)
			{
				return NotFound();
			}
			var asignatura = await _context.Asignaturas.FindAsync(id);

			if (asignatura == null)
			{
				return NotFound();
			}

			return new AsignaturaDetailDTO(
				asignatura.IdAsignatura,
				asignatura.Nombre,
				asignatura.Creditos,
				asignatura.Facultad.Nombre,
				asignatura.PreRequisito?.Nombre,
				asignatura.Cursos.Select(
					c => new CursoDTO(
						c.IdCurso,
						c.Asignatura.Nombre,
						c.Asignatura.PreRequisito?.Nombre ?? "",
						c.Asignatura.Creditos,
						c.Cupos - c.Inscripciones.Count()
					)
				).ToList()
			);
		}

		// PUT: api/Asignaturas/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutAsignatura(int id, UpdateAsignaturaDTO asignatura)
		{

			if (_context.Asignaturas == null)
			{
				return NotFound();
			}

			var asignaturaToUpdate = await _context.Asignaturas.FindAsync(id);

			if (asignaturaToUpdate == null)
			{
				return NotFound();
			}

			asignaturaToUpdate.Nombre = asignatura.Nombre ?? asignaturaToUpdate.Nombre;
			asignaturaToUpdate.Creditos = asignatura.Creditos ?? asignaturaToUpdate.Creditos;
			asignaturaToUpdate.IdFacultad = asignatura.IdFacultad ?? asignaturaToUpdate.IdFacultad;
			asignaturaToUpdate.IdPreRequisito = asignatura.IdPreRequisito ?? asignaturaToUpdate.IdPreRequisito;

			_context.Entry(asignaturaToUpdate).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!AsignaturaExists(id))
				{
					return NotFound();
				}
				else
				{
					return Problem("Error updating entity.");
				}
			}

			return NoContent();
		}

		// POST: api/Asignaturas
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Asignatura>> PostAsignatura(CreateAsignaturaDTO asignatura)
		{
			if (_context.Asignaturas == null)
			{
				return Problem("Entity set 'AppDbContext.Asignaturas'  is null.");
			}

			var nuevaAsignatura = new Asignatura
			{
				Nombre = asignatura.Nombre,
				Creditos = asignatura.Creditos,
				IdFacultad = asignatura.IdFacultad,
				IdPreRequisito = asignatura.IdPreRequisito
			};

			try
			{
				_context.Asignaturas.Add(nuevaAsignatura);
				await _context.SaveChangesAsync();
			}
			catch (Exception e)
			{
				return Problem(e.Message);
			}

			return CreatedAtAction("GetAsignatura", new { id = nuevaAsignatura.IdAsignatura }, nuevaAsignatura);
		}

		// DELETE: api/Asignaturas/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAsignatura(int id)
		{
			if (_context.Asignaturas == null)
			{
				return NotFound();
			}
			var asignatura = await _context.Asignaturas.FindAsync(id);
			if (asignatura == null)
			{
				return NotFound();
			}

			_context.Asignaturas.Remove(asignatura);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool AsignaturaExists(int id)
		{
			return (_context.Asignaturas?.Any(e => e.IdAsignatura == id)).GetValueOrDefault();
		}
	}
}
