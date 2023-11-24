using System.ComponentModel.DataAnnotations;

namespace SophosUniversityApi.DataContracts
{
	public record CreateEstudianteDTO(
		[Required] string Nombre,
		[Required] int IdFacultad,
		int Semestre = 1,
		int Edad = 18
	);

	public record UpdateEstudianteDTO(
		string? Nombre,
		int? IdFacultad,
		int? Semestre,
		int? Edad
	);

	public record EstudianteDTO(
		int IdEstudiante,
		string Nombre,
		int Facultad,
		int CreditosInscritos,
		int Edad
	);

	public record EstudianteDetailDTO(
		int IdEstudiante,
		string Nombre,
		int Facultad,
		int CreditosInscritos,
		int Edad,
		int Semestre,
		List<string> CursosMatriculados,//TODO: Cambiar a CursoDTO
		List<string> CursosCursados
	);
}