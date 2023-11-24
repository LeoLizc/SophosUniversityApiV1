using System.ComponentModel.DataAnnotations;

namespace SophosUniversityApi.DataContracts
{
	public record CreateCursoDTO(
		[Required] int IdProfesor,
		[Required] int IdAsignatura,
		string? Periodo,
		bool? Activo,
		int Cupos = 0
	);

	public record UpdateCursoDTO(
		string? Periodo,
		int? IdProfesor,
		int? IdAsignatura,
		bool? Activo,
		int? Cupos
	);

	public record CursoDTO(
		int IdCurso,
		string Nombre,
		string PreRequisito,
		int Creditos,
		int CuposDisponibles
	);

	public record CursoDetailDTO(
		int IdCurso,
		string Periodo,
		bool Activo,
		string Asignatura,
		int NumeroEstudiantes,
		string Profesor,
		int Creditos,
		List<string> Alumnos //TODO: Not a string, but a list of EstudianteDTO
	);

}
