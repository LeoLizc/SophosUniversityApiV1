using System.ComponentModel.DataAnnotations;

namespace SophosUniversityApi.DataContracts
{
	public record CreateInscripcionDTO(
		[Required] int IdEstudiante,
		[Required] int IdCurso
	);//? Cómo saber su id para retirar?
}
