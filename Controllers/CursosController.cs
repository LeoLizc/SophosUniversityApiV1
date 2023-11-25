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
	public class CursosController : ControllerBase
	{
		private readonly AppDbContext _context;

		public CursosController(AppDbContext context)
		{
			_context = context;
		}

		// GET: api/Cursos
		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<CursoDTO>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<IEnumerable<CursoDTO>>> GetCursos(
			[FromQuery(Name = "nombre")] string? nombre,//* Filtra por el nombre del estudiante
			[FromQuery(Name = "cupos")] bool? cupos,//* Filtra los cursos con cupos disponibles
			[FromQuery(Name = "activo")] bool? activo//* Filtra los cursos activos
		)
		{
			if (_context.Cursos == null)
			{
				return NotFound();
			}
			var cursos = await _context.Cursos.ToListAsync();

			if (nombre != null)
			{
				cursos = cursos.Where(c => _context.Asignaturas.Find(c.IdAsignatura)!.Nombre.Contains(nombre)).ToList();
			}

			if (cupos != null)
			{
				cursos = cursos.Where(
					c => (c.Cupos - _context.Inscripciones.Where(ins => ins.IdCurso == c.IdCurso).Count() > 0) == (bool)cupos
				).ToList();
			}

			if (activo != null)
			{
				cursos = cursos.Where(c => c.Activo == activo).ToList();
			}

			var result = cursos.Select(
				c => {
					var asignatura  = _context.Asignaturas.Find(c.IdAsignatura);
					return new CursoDTO(
						c.IdCurso,
						asignatura!.Nombre,
						asignatura.PreRequisito?.Nombre ?? "",
						asignatura.Creditos,
						c.Cupos - _context.Inscripciones.Where(ins => ins.IdCurso == c.IdCurso).Count(),
						c.Activo ?? false
					);
				}
			);

			return Ok(result);
		}

		// GET: api/Cursos/5
		[HttpGet("{id}")]
		[ProducesResponseType(typeof(CursoDetailDTO), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<CursoDetailDTO>> GetCurso(int id)
		{
			if (_context.Cursos == null)
			{
				return NotFound();
			}
			var curso = await _context.Cursos.FindAsync(id);

			if (curso == null)
			{
				return NotFound();
			}

			var asignatura = _context.Asignaturas.Find(curso.IdAsignatura);

			var result = new CursoDetailDTO(
				curso.IdCurso,
				curso.Periodo,
				curso.Activo ?? false,
				asignatura!.Nombre,
				_context.Inscripciones.Where(ins => ins.IdCurso == curso.IdCurso).Count(),
				_context.Profesores.Find(curso.IdProfesor)!.Nombre,
				asignatura.Creditos,
				_context.Inscripciones.Where(ins => ins.IdCurso == curso.IdCurso).Select(ins => ins.Estudiante.Nombre).ToList()
			);

			return Ok(result);
		}

		// PUT: api/Cursos/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> PutCurso(int id, UpdateCursoDTO curso)
		{

			if (_context.Cursos == null)
			{
				return NotFound();
			}

			var cursoToUpdate = await _context.Cursos.FindAsync(id);

			if (cursoToUpdate == null)
			{
				return NotFound();
			}

			cursoToUpdate.Periodo = curso.Periodo ?? cursoToUpdate.Periodo;
			cursoToUpdate.Activo = curso.Activo ?? cursoToUpdate.Activo;
			cursoToUpdate.Cupos = curso.Cupos ?? cursoToUpdate.Cupos;
			cursoToUpdate.IdAsignatura = curso.IdAsignatura ?? cursoToUpdate.IdAsignatura;
			cursoToUpdate.IdProfesor = curso.IdProfesor ?? cursoToUpdate.IdProfesor;

			_context.Entry(cursoToUpdate).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!CursoExists(id))
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

		// POST: api/Cursos
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		[ProducesResponseType(typeof(Curso), StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<Curso>> PostCurso(CreateCursoDTO curso)
		{
			if (_context.Cursos == null)
			{
				return Problem("Entity set 'AppDbContext.Cursos'  is null.");
			}

			var nuevoCurso = new Curso()
			{
				Periodo = curso.Periodo ?? "Actual...",
				Activo = curso.Activo ?? true,
				Cupos = curso.Cupos,
				IdAsignatura = curso.IdAsignatura,
				IdProfesor = curso.IdProfesor
			};

			_context.Cursos.Add(nuevoCurso);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(PostCurso), new { id = nuevoCurso.IdCurso }, nuevoCurso);
		}

		// DELETE: api/Cursos/5
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> DeleteCurso(int id)
		{
			if (_context.Cursos == null)
			{
				return NotFound();
			}
			var curso = await _context.Cursos.FindAsync(id);
			if (curso == null)
			{
				return NotFound();
			}

			_context.Cursos.Remove(curso);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool CursoExists(int id)
		{
			return (_context.Cursos?.Any(e => e.IdCurso == id)).GetValueOrDefault();
		}
	}
}
