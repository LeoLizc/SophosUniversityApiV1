using System;
using System.Collections.Generic;

namespace SophosUniversityApi.Models;

public partial class Curso
{
    public int IdCurso { get; set; }

    public string Periodo { get; set; } = null!;

    public int Cupos { get; set; }

    public int IdAsignatura { get; set; }

    public int IdProfesor { get; set; }

    public bool? Activo { get; set; }

    public virtual Asignatura IdAsignaturaNavigation { get; set; } = null!;

    public virtual Profesor IdProfesorNavigation { get; set; } = null!;

    public virtual ICollection<Inscripcion> Inscripcions { get; set; } = new List<Inscripcion>();
}
