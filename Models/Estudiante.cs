using System;
using System.Collections.Generic;

namespace SophosUniversityApi.Models;

public partial class Estudiante
{
    public int IdEstudiante { get; set; }

    public string Nombre { get; set; } = null!;

    public int Edad { get; set; }

    public int CreditosInscritos { get; set; }

    public int SemestreActual { get; set; }

    public int IdFacultad { get; set; }

    public virtual Facultad IdFacultadNavigation { get; set; } = null!;

    public virtual ICollection<Inscripcion> Inscripcions { get; set; } = new List<Inscripcion>();
}
