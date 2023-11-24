using System.ComponentModel.DataAnnotations;

namespace SophosUniversityApi.DataContracts
{
	public record CreateProfesorDTO(
		[Required] string Nombre,
		[Required] string TituloMaximo,
		[Required] int AniosExperiencia
	);

	public record UpdateProfesorDTO(
		string? Nombre,
		string? TituloMaximo,
		int? AniosExperiencia
	);

	public record ProfesorDTO(
		int IdProfesor,
		string Nombre,
		string TituloMaximo,
		int AniosExperiencia,
		List<string> cursos //TODO: Cambiar por lista de cursos
	);
}
