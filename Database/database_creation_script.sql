-- SQL SERVER DATABASE CREATION SCRIPT

DROP DATABASE IF EXISTS Sophos_university;
CREATE DATABASE Sophos_university;
GO
USE Sophos_university;

CREATE TABLE Profesor(
    id_profesor INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    titulo_maximo VARCHAR(50) NOT NULL,
    anios_experiencia INT NOT NULL
);

CREATE TABLE Facultad(
    id_facultad INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL
);

CREATE TABLE Estudiante(
    id_estudiante INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    edad INT NOT NULL,
    semestre_actual INT NOT NULL,
    id_facultad INT NOT NULL,
    FOREIGN KEY (id_facultad) REFERENCES Facultad(id_facultad)
);

CREATE TABLE Asignatura(
    id_asignatura INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    creditos INT NOT NULL,
    id_facultad INT NOT NULL,
    id_pre_requisito INT,
    FOREIGN KEY (id_facultad) REFERENCES Facultad(id_facultad)
    ON DELETE CASCADE,
    FOREIGN KEY (id_pre_requisito) REFERENCES Asignatura(id_asignatura)
    ON DELETE NO ACTION
);

CREATE TABLE Curso(
    id_curso INT IDENTITY(1,1) PRIMARY KEY,
    periodo VARCHAR(50) NOT NULL,
    cupos INT NOT NULL,
    id_asignatura INT NOT NULL,
    id_profesor INT NOT NULL,
    activo BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (id_asignatura) REFERENCES Asignatura(id_asignatura)
    ON DELETE CASCADE,
    FOREIGN KEY (id_profesor) REFERENCES Profesor(id_profesor)
    ON DELETE CASCADE,
);

CREATE TABLE Inscripcion(
    id_inscripcion INT IDENTITY(1,1) PRIMARY KEY,
    id_estudiante INT NOT NULL,
    id_curso INT NOT NULL,
    FOREIGN KEY (id_estudiante) REFERENCES Estudiante(id_estudiante)
    ON DELETE CASCADE,
    FOREIGN KEY (id_curso) REFERENCES Curso(id_curso)
    ON DELETE CASCADE
);

GO