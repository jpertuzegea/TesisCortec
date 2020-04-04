-- DROP DATABASE TESIS;
-- CREATE DATABASE TESIS;

USE TESIS;
 
CREATE TABLE Personas(
PersonaId INT NOT NULL IDENTITY(1,1),
Imagen VARBINARY(MAX),
ContetType VARCHAR(50), 
TipoDocumento VARCHAR(50),
NumDocumento VARCHAR(50),
NombreCompleto VARCHAR(70),
CodigoInstitucional VARCHAR(50), -- Auto incremental  
Email VARCHAR(50),
Ciudad VARCHAR(50),
Departamento VARCHAR(50),
Direccion VARCHAR(150),
Telefono VARCHAR(50),
Clave VARCHAR(254),
-- Rol TINYINT NOT NULL, -- debe haber una tabla que relacione rol y usuario 
FechaIngreso DATETIME NOT NULL,
Estado TINYINT NOT NULL,
PRIMARY KEY (PersonaId)
);
DBCC CHECKIDENT (Personas, RESEED,1); -- obliga a que el contador de llave primaria empiece en 1  


Create table Codigos(
CodigoInstitucionalId INT NOT NULL IDENTITY(1,1),
Contador int not null,
PRIMARY KEY (CodigoInstitucionalId)
);
DBCC CHECKIDENT (Codigos, RESEED,1); -- obliga a que el contador de llave primaria empiece en 1  
insert into Codigos values (0);

CREATE TABLE NotasRapidas(
NotaRapidaId INT NOT NULL IDENTITY(1,1),
FechaPublicacion DATETIME NOT NULL,
FechaFinalizacion DATETIME NOT NULL,
Titulo VARCHAR(50),
Mensaje VARCHAR(250),                       
UsuarioPublica INT,             
Estado TINYINT NOT NULL,
PRIMARY KEY (NotaRapidaId),                                      
FOREIGN KEY (UsuarioPublica) REFERENCES Personas (PersonaId)
);
DBCC CHECKIDENT (NotasRapidas, RESEED,1); -- obliga a que el contador de llave primaria empiece en 1 


-- Borrado de tablas --
Drop table NotasRapidas;
Drop table Codigos;
Drop table Personas;