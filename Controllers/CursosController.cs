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
	public class CursosController : ControllerBase
	{
		private readonly AppDbContext _context;

		public CursosController(AppDbContext context)
		{
			_context = context;
		}

		// GET: api/Cursos
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Curso>>> GetCursos(
			[FromQuery(Name = "nombre")] string? nombre,//* Filtra por el nombre del estudiante
			[FromQuery(Name = "cupos")] bool? cupos//* Filtra los cursos con cupos disponibles
		)
		{
			if (_context.Cursos == null)
			{
				return NotFound();
			}
			var cursos = _context.Cursos.AsQueryable();

			if (nombre != null)
			{
				cursos = cursos.Where(c => c.Asignatura.Nombre.Contains(nombre));
			}

			if (cupos != null)
			{
				cursos = cursos.Where(c => c.Cupos - _context.Inscripciones );
			}
		}

		// GET: api/Cursos/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Curso>> GetCurso(int id)
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

			return curso;
		}

		// PUT: api/Cursos/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutCurso(int id, Curso curso)
		{
			if (id != curso.IdCurso)
			{
				return BadRequest();
			}

			_context.Entry(curso).State = EntityState.Modified;

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
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/Cursos
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Curso>> PostCurso(Curso curso)
		{
			if (_context.Cursos == null)
			{
				return Problem("Entity set 'AppDbContext.Cursos'  is null.");
			}
			_context.Cursos.Add(curso);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetCurso", new { id = curso.IdCurso }, curso);
		}

		// DELETE: api/Cursos/5
		[HttpDelete("{id}")]
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
