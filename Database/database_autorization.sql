-- Crear el inicio de sesión (Login)
CREATE LOGIN SophosApi WITH PASSWORD = 'FFkRlYH57Y7WFYsg1HgF';

-- Utilizar la base de datos Sophos_university
USE Sophos_university;

-- Crear el usuario en la base de datos y asignarle el inicio de sesión
CREATE USER SophosApi FOR LOGIN SophosApi;

-- Asignar permisos de propietario al usuario sobre la base de datos
EXEC sp_addrolemember 'db_owner', 'SophosApi';

GO