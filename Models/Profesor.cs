using System;
using System.Collections.Generic;

namespace SophosUniversityApi.Models;

public partial class Profesor
{
    public int IdProfesor { get; set; }

    public string Nombre { get; set; } = null!;

    public string TituloMaximo { get; set; } = null!;

    public int AniosExperiencia { get; set; }

    public virtual ICollection<Curso> Cursos { get; set; } = new List<Curso>();
}
