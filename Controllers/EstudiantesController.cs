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
	public class EstudiantesController : ControllerBase
	{
		private readonly AppDbContext _context;

		public EstudiantesController(AppDbContext context)
		{
			_context = context;
		}

		// GET: api/Estudiantes
		[HttpGet]
		public async Task<ActionResult<IEnumerable<EstudianteDTO>>> GetEstudiantes(
			[FromQuery(Name = "nombre")] string? nombre,
			[FromQuery(Name = "facultad")] string? facultad
		)
		{
			if (_context.Estudiantes == null)
			{
				return NotFound();
			}
			
			var estudiantes = await _context.Estudiantes.ToListAsync();
			
			if (nombre != null)
			{
				estudiantes = estudiantes.Where(e => e.Nombre.Contains(nombre)).ToList();
			}
			if (facultad != null)
			{
				estudiantes = estudiantes.Where(e => e.Facultad.Nombre.Contains(facultad)).ToList();
			}

			return Ok(
				estudiantes.Select(
					est => new EstudianteDTO(
						est.IdEstudiante,
						est.Nombre,
						est.Facultad.Nombre,
						_context.Inscripciones.Where(
							ins => ins.IdEstudiante == est.IdEstudiante
									&& ins.Curso.Activo == true
						).Sum(ins => ins.Curso.Asignatura.Creditos),
						est.Edad
					)
				)
			);
		}

		// GET: api/Estudiantes/5
		[HttpGet("{id}")]
		public async Task<ActionResult<EstudianteDetailDTO>> GetEstudiante(int id)
		{
			if (_context.Estudiantes == null)
			{
				return NotFound();
			}
			var estudiante = await _context.Estudiantes.FindAsync(id);

			if (estudiante == null)
			{
				return NotFound();
			}

			var result = new EstudianteDetailDTO(
				estudiante.IdEstudiante,
				estudiante.Nombre,
				estudiante.Facultad.IdFacultad,
				_context.Inscripciones.Where(
					ins => ins.IdEstudiante == estudiante.IdEstudiante
							&& ins.Curso.Activo == true
				).Sum(ins => ins.Curso.Asignatura.Creditos),
				estudiante.Edad,
				estudiante.SemestreActual,
				_context.Inscripciones.Where(
					ins => ins.IdEstudiante == estudiante.IdEstudiante
							&& ins.Curso.Activo == true
				).Select(ins => ins.Curso.Asignatura.Nombre).ToList(),
				_context.Inscripciones.Where(
					ins => ins.IdEstudiante == estudiante.IdEstudiante
							&& ins.Curso.Activo == false
				).Select(ins => ins.Curso.Asignatura.Nombre).ToList()
			);

			return Ok(result);
		}

		// PUT: api/Estudiantes/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutEstudiante(int id, UpdateEstudianteDTO estudiante)
		{

			var estudiantePorActualizar = await _context.Estudiantes.FindAsync(id);

			if (estudiantePorActualizar == null)
			{
				return NotFound();
			}

			_context.Entry(estudiantePorActualizar).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!EstudianteExists(id))
				{
					return NotFound();
				}
				else
				{
					return Problem("Problema al actualizar el estudiante.");
				}
			}

			return NoContent();
		}

		// POST: api/Estudiantes
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Estudiante>> PostEstudiante(CreateEstudianteDTO estudiante)
		{
			if (_context.Estudiantes == null)
			{
				return Problem("Entity set 'AppDbContext.Estudiantes'  is null.");
			}

			var nuevoEstudiante = new Estudiante
			{
				Nombre = estudiante.Nombre,
				IdFacultad = estudiante.IdFacultad,
				SemestreActual = estudiante.Semestre,
				Edad = estudiante.Edad
			};

			try
			{
				_context.Estudiantes.Add(nuevoEstudiante);
				await _context.SaveChangesAsync();
			} catch (System.Exception)
			{
				return Problem("Problema al crear el estudiante.");
			}
			return CreatedAtAction("GetEstudiante", new { id = nuevoEstudiante.IdEstudiante }, nuevoEstudiante);
		}

		// DELETE: api/Estudiantes/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteEstudiante(int id)
		{
			if (_context.Estudiantes == null)
			{
				return NotFound();
			}
			var estudiante = await _context.Estudiantes.FindAsync(id);
			if (estudiante == null)
			{
				return NotFound();
			}

			_context.Estudiantes.Remove(estudiante);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool EstudianteExists(int id)
		{
			return (_context.Estudiantes?.Any(e => e.IdEstudiante == id)).GetValueOrDefault();
		}
	}
}
