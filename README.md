# Reto Sophos University Api
Este repositorio contiene el proyecto desarrollado como solución a un reto propuesto por un curso de Sophos de desarrollo backend

## Enunciado
Usted ha sido contratado por la universidad Sophoscon el fin de desarrollar una aplicación web que permita gestionar la información de su institución, especialmente los cursos ofrecidos, estudiantes y maestros, el sistema debe permitir:
- listar los cursos ofrecidos mostrando su nombre,nombre delcurso prerrequisito, número de créditos y cupos disponibles.
- listar los alumnos matriculadosmostrando nombre, facultad a la que pertenece, cantidad de créditos inscritos.
- listar los profesores, mostrando nombre, máximo titulo académico, años de experiencia en docencia y nombre del curso o de los cursos que dicta.
- Agregar nuevos cursos, alumnos y/o profesores.
- Actualizar información de cursos, alumnos y/o profesores.
- Eliminar cursos, alumnos y/o profesores.
- Buscar curso, alumno y/o docente por nombre.
- Buscar curso por estado de cupos (si tiene o no cupos disponibles)
- Buscar alumnos por facultad a la que pertenece.
- Seleccionar un curso y mostrar la información de este (nombre, número de estudiantes inscritos, profesor que dicta el curso, cantidad de créditos, entre otro) además un listado de los alumnos que están cursando dicha asignatura.
- Seleccionar un Alumno y mostrar la información de este (nombre, número de créditos inscritos, semestre que cursa, entre otros datos que considere relevantes) además un listado de los cursos que tiene matriculados y un listado de las asignaturas que ya cursó

## Utilización
### Requerimientos
Para la correcta ejecución y prueba de este proyecto se requieren los siguientes softwares:
- Docker: 24.0.6 ⏫

### Ejecución
Para ejecutar este proyecto se debe ejecutar el siguiente comando en la raíz del proyecto:
```bash
docker-compose up --build
```

Este comando creará los contenedores necesarios para la ejecución del proyecto, y expondrá el puerto 5189 para la comunicación con la Api y el puerto 1433 para la comunicación con la base de datos.
Normalmente la base de datos se tarda unos 20 segundos en ejecutar el proceso de población, por lo que recomiendo esperar antes de empezar a usar la api.

### Endpoints
Puede encontrar la documentación e información sobre el uso de los endpoints accediendo a la ruta: ```/swagger/index.html```

## Autor
- **Leonardo David Lizcano Pinto**
  - [LinkedIn](https://www.linkedin.com/in/leonardo-lizcano-pinto0220/)
  - [Github](https://github.com/LeoLizc/)
