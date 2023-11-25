using SophosUniversityApi.Models;
using System.ComponentModel.DataAnnotations;

namespace SophosUniversityApi.DataContracts
{
	public record CreateAsignaturaDTO(
		[Required] string Nombre,
		[Required] int Creditos,
		[Required] int IdFacultad,
		int? IdPreRequisito
	);

	public record UpdateAsignaturaDTO(
		string? Nombre,
		int? Creditos,
		int? IdFacultad,
		int? IdPreRequisito
	);

	public record AsignaturaDTO(
		int IdAsignatura,
		string Nombre,
		int Creditos
	);

	public record AsignaturaDetailDTO(
		int IdAsignatura,
		string Nombre,
		int Creditos,
		string Facultad,
		string? PreRequisito,
		List<Curso> Cursos
	);
}
