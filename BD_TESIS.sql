-- DROP DATABASE TESIS;
-- CREATE DATABASE TESIS;

USE TESIS;

CREATE TABLE Personas(
PersonaId INT NOT NULL IDENTITY(1,1),
Imagen VARBINARY(MAX),
ContetType VARCHAR(50), 
TipoDocumento VARCHAR(50),
NumDocumento VARCHAR(50),
NombreComplpeto VARCHAR(70),
CodigoInstitucional VARCHAR(50), -- Auto incremental 
FechaNacimiento DATETIME NOT NULL,
Email VARCHAR(50),
Ciudad VARCHAR(50),
Direccion VARCHAR(150),
Telefono VARCHAR(50),
Clave VARCHAR(254),
-- Rol TINYINT NOT NULL, -- debe haber una tabla que relacione rol y usuario 
FechaIngreso DATETIME NOT NULL,
Estado TINYINT NOT NULL,
PRIMARY KEY (PersonaId)
);
DBCC CHECKIDENT (Personas, RESEED,1); -- obliga a que el contador de llave primaria empiece en 1  
	 