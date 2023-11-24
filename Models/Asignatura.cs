using System;
using System.Collections.Generic;

namespace SophosUniversityApi.Models;

public partial class Asignatura
{
    public int IdAsignatura { get; set; }

    public string Nombre { get; set; } = null!;

    public int Creditos { get; set; }

    public int IdFacultad { get; set; }

    public int? IdPreRequisito { get; set; }

    public virtual ICollection<Curso> Cursos { get; set; } = new List<Curso>();

    public virtual Facultad Facultad { get; set; } = null!;

    public virtual Asignatura? PreRequisito { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<Asignatura> AsignaturasDependientes { get; set; } = new List<Asignatura>();
}
