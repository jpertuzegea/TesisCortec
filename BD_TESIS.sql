-- DROP DATABASE TESIS;
-- CREATE DATABASE TESIS;

USE TESIS;
 
CREATE TABLE Personas(
PersonaId INT NOT NULL IDENTITY(1,1),
Imagen VARBINARY(MAX),
ContetType VARCHAR(50), 
TipoDocumento TINYINT NOT NULL,
NumDocumento VARCHAR(50),
NombreCompleto VARCHAR(70),
CodigoInstitucional VARCHAR(50), -- Auto incremental  
Email VARCHAR(50),
Ciudad VARCHAR(50),
Departamento VARCHAR(50),
Direccion VARCHAR(150),
Telefono VARCHAR(50),
Clave VARCHAR(254),
RolAcademico TINYINT NOT NULL,
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

CREATE TABLE EvidenciaCorreo(
EvidenciaCorreoId INT NOT NULL IDENTITY(1,1), 
EmailDestino VARCHAR(70),
EmailRemitente VARCHAR(70),
Asunto VARCHAR(250),
Mensaje VARCHAR(2000), 
Fecha DATETIME,
EstadoEnvio VARCHAR(20),
PRIMARY KEY (EvidenciaCorreoId)
);
DBCC CHECKIDENT (EvidenciaCorreo, RESEED,1); -- obliga a que el contador de llave primaria empiece en 1 

CREATE TABLE Cursos(
CursoId INT NOT NULL IDENTITY(1,1), 
Nombre VARCHAR(70),
Descripcion VARCHAR(255),
Imagen VARBINARY(MAX),
ContetType VARCHAR(50), 
TituloOtorgado VARCHAR(100),
ValorCurso INT,
DuracionHoras INT,
Docente INT,
Estado TINYINT NOT NULL,
PRIMARY KEY (CursoId),
FOREIGN KEY (Docente) REFERENCES Personas (PersonaId)
);
DBCC CHECKIDENT (Cursos, RESEED,1); -- obliga a que el contador de llave primaria empiece en 1 

CREATE TABLE IngresosAlSistema(
IngresoAlSistemaId INT NOT NULL IDENTITY(1,1),
Usuario VARCHAR(70),
FechaIntento DATETIME NOT null,
IP_Origen VARCHAR(16),
EstadoAcceso TINYINT NOT NULL,
PRIMARY KEY (IngresoAlSistemaId)
);
DBCC CHECKIDENT (IngresosAlSistema, RESEED,1); -- obliga a que el contador de llave primaria empiece en 1 


-- Borrado de tablas --
Drop table IngresosAlSistema;
Drop table Cursos;
Drop table EvidenciaCorreo;
Drop table NotasRapidas;
Drop table Codigos;
Drop table Personas;