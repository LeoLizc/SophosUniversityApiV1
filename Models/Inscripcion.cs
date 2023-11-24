using System;
using System.Collections.Generic;

namespace SophosUniversityApi.Models;

public partial class Inscripcion
{
    public int IdInscripcion { get; set; }

    public int IdEstudiante { get; set; }

    public int IdCurso { get; set; }

    public virtual Curso Curso { get; set; } = null!;

    public virtual Estudiante Estudiante { get; set; } = null!;
}
