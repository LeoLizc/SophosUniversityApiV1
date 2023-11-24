using System.ComponentModel.DataAnnotations;

namespace SophosUniversityApi.DataContracts
{
	public record CreateFacultadDTO(
		[Required] string Nombre
	);

	public record UpdateFacultadDTO(
		string? Nombre
	);

}
