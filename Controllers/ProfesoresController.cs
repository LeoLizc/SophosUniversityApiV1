﻿using System;
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
		public async Task<ActionResult<IEnumerable<ProfesorDTO>>> GetProfesores(
			[FromQuery(Name = "nombre")] string? nombre
		)
		{
			if (_context.Profesores == null)
			{
				return NotFound();
			}

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
					prof.Cursos.Select(cu => cu.Asignatura.Nombre).ToList()
				)
			);
			return Ok(result);
		}

		// PUT: api/Profesores/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
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
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/Profesores
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
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
					"GetProfesor",
					newProfesor
				);
			} catch (DbUpdateConcurrencyException)
			{
				return Problem("Error al crear el profesor.");
			}
		}

		// DELETE: api/Profesores/5
		[HttpDelete("{id}")]
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
