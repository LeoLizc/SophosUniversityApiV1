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
	public class ProfesoresController : ControllerBase
	{
		private readonly AppDbContext _context;

		public ProfesoresController(AppDbContext context)
		{
			_context = context;
		}

		// GET: api/Profesores
		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<ProfesorDTO>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<IEnumerable<ProfesorDTO>>> GetProfesores(
			[FromQuery(Name = "nombre")] string? nombre
		)
		{
			if (_context.Profesores == null)
			{
				return NotFound();
			}
			try
			{
				var profesores = await _context.Profesores.ToListAsync();

				if (nombre != null)
				{
					profesores = profesores.Where(p => p.Nombre.Contains(nombre)).ToList();
				}

				var result = profesores.Select(
					prof => new ProfesorDTO(
						prof.IdProfesor,
						prof.Nombre,
						prof.TituloMaximo,
						prof.AniosExperiencia,
						_context.Cursos.Where(c => c.IdProfesor == prof.IdProfesor).Select(cu => cu.Asignatura.Nombre).ToList()
					)
				);
				return Ok(result);
			}
			catch (Exception e)
			{
				return Problem("Error obteniendo profesores");
			}
		}

		// PUT: api/Profesores/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> PutProfesor(int id, UpdateProfesorDTO profesor)
		{

			var profesorToUpdate = await _context.Profesores.FindAsync(id);

			if (profesorToUpdate == null)
			{
				return NotFound();
			}

			profesorToUpdate.Nombre = profesor.Nombre ?? profesorToUpdate.Nombre;
			profesorToUpdate.TituloMaximo = profesor.TituloMaximo ?? profesorToUpdate.TituloMaximo;
			profesorToUpdate.AniosExperiencia = profesor.AniosExperiencia ?? profesorToUpdate.AniosExperiencia;

			_context.Entry(profesorToUpdate).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ProfesorExists(id))
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

		// POST: api/Profesores
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		[ProducesResponseType(typeof(Profesor), StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<Profesor>> PostProfesor(CreateProfesorDTO profesor)
		{
			if (_context.Profesores == null)
			{
				return Problem("Entity set 'AppDbContext.Profesors'  is null.");
			}

			var newProfesor = new Profesor
			{
				Nombre = profesor.Nombre,
				TituloMaximo = profesor.TituloMaximo,
				AniosExperiencia = profesor.AniosExperiencia
			};

			try
			{
				_context.Profesores.Add(newProfesor);
				await _context.SaveChangesAsync();

				return CreatedAtAction(
					nameof(GetProfesores),
					newProfesor
				);
			}
			catch (DbUpdateConcurrencyException)
			{
				return Problem("Error al crear el profesor.");
			}
		}

		// DELETE: api/Profesores/5
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> DeleteProfesor(int id)
		{
			if (_context.Profesores == null)
			{
				return NotFound();
			}
			var profesor = await _context.Profesores.FindAsync(id);
			if (profesor == null)
			{
				return NotFound();
			}

			_context.Profesores.Remove(profesor);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool ProfesorExists(int id)
		{
			return (_context.Profesores?.Any(e => e.IdProfesor == id)).GetValueOrDefault();
		}
	}
}
