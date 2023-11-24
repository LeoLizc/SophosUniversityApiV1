using System;
using System.Collections.Generic;

namespace SophosUniversityApi.Models;

public partial class Facultad
{
    public int IdFacultad { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Asignatura> Asignaturas { get; set; } = new List<Asignatura>();

    public virtual ICollection<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();
}
