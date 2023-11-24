using System.ComponentModel.DataAnnotations;

namespace SophosUniversityApi.DataContracts
{
	public record CreateInscripcionDTO(
		[Required] string IdEstudiante,
		[Required] string IdCurso
	);//? Cómo saber su id para retirar?
}
