USE Sophos_university;
GO

-- Populate Profesor table
INSERT INTO Profesor (nombre, titulo_maximo, anios_experiencia) VALUES
    ('Dana Neal', 'Doctorado en Matemáticas', 10),
    ('Josue Sarmiento', 'Doctorado en Física', 8),
    ('Andrés Molina', 'Doctorado en Historia', 12);

-- Populate Facultad table
INSERT INTO Facultad (nombre) VALUES
    ('Facultad de Ciencias'),
    ('Facultad de Humanidades'),
    ('Facultad de Ingeniería');

-- Populate Estudiante table
INSERT INTO Estudiante (nombre, edad, creditos_inscritos, semestre_actual, id_facultad) VALUES
    ('Luis Miguel Lopez', 20, 30, 3, 1),
    ('Juan Montero', 22, 45, 4, 2),
    ('Miguel Dubán', 21, 35, 3, 3);

-- Populate Asignatura table
INSERT INTO Asignatura (nombre, creditos, id_facultad, id_pre_requisito) VALUES
    ('Matemáticas I', 4, 1, NULL),
    ('Historia Antigua', 3, 2, NULL),
    ('Programación Avanzada', 5, 3, 1);


-- Populate Curso table
INSERT INTO Curso (periodo, cupos, id_asignatura, id_profesor, activo) VALUES
    ('2023-1', 5, 1, 1, 0),
    ('2023-2', 30, 1, 1, DEFAULT),
    ('2023-2', 25, 2, 2, DEFAULT),
    ('2023-2', 20, 3, 3, DEFAULT);

-- Populate Inscripcion table
INSERT INTO Inscripcion (id_estudiante, id_curso) VALUES
    (1, 1),
    (2, 2),
    (3, 1),
    (3, 3);

GO