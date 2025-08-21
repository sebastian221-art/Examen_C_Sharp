-- =========================
-- CREACIÓN DE LA BASE DE DATOS
-- =========================
DROP DATABASE IF EXISTS TorneoDB;
CREATE DATABASE TorneoDB;
USE TorneoDB;

-- =========================
-- TABLA DE EQUIPOS
-- =========================
CREATE TABLE Equipo (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    GolesContra INT DEFAULT 0
);

-- =========================
-- TABLA DE JUGADORES
-- =========================
CREATE TABLE Jugador (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Edad INT NOT NULL,
    Posicion VARCHAR(50),
    Dorsal INT,
    Asistencias INT DEFAULT 0,
    ValorMercado DECIMAL(18,2) DEFAULT 0,
    EquipoId INT,
    FOREIGN KEY (EquipoId) REFERENCES Equipo(Id) ON DELETE SET NULL
);

-- =========================
-- TABLA DE TORNEOS
-- =========================
CREATE TABLE Torneo (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    FechaInicio DATE,
    FechaFin DATE
);

-- =========================
-- RELACIÓN EQUIPO - TORNEO
-- =========================
CREATE TABLE Equipo_Torneo (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    EquipoId INT NOT NULL,
    TorneoId INT NOT NULL,
    FOREIGN KEY (EquipoId) REFERENCES Equipo(Id) ON DELETE CASCADE,
    FOREIGN KEY (TorneoId) REFERENCES Torneo(Id) ON DELETE CASCADE
);

-- =========================
-- TABLA CUERPO TÉCNICO
-- =========================
CREATE TABLE CuerpoTecnico (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Email VARCHAR(100),
    Descripcion TEXT,
    Rol VARCHAR(50),
    EquipoId INT,
    FOREIGN KEY (EquipoId) REFERENCES Equipo(Id) ON DELETE CASCADE
);

-- =========================
-- TABLA CUERPO MÉDICO
-- =========================
CREATE TABLE CuerpoMedico (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Email VARCHAR(100),
    Descripcion TEXT,
    Especialidad VARCHAR(50),
    EquipoId INT,
    FOREIGN KEY (EquipoId) REFERENCES Equipo(Id) ON DELETE CASCADE
);

-- =========================
-- TABLA HISTORIAL DE TRANSFERENCIAS
-- =========================
CREATE TABLE Transferencia (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    JugadorId INT NOT NULL,
    EquipoOrigenId INT,
    EquipoDestinoId INT,
    FechaTransferencia DATE NOT NULL DEFAULT (CURRENT_DATE),
    Precio DECIMAL(18,2) DEFAULT 0,
    FOREIGN KEY (JugadorId) REFERENCES Jugador(Id) ON DELETE CASCADE,
    FOREIGN KEY (EquipoOrigenId) REFERENCES Equipo(Id) ON DELETE SET NULL,
    FOREIGN KEY (EquipoDestinoId) REFERENCES Equipo(Id) ON DELETE SET NULL
);

-- =========================
-- INSERTS DE EJEMPLO
-- =========================

--  Equipos
INSERT INTO Equipo (Nombre, GolesContra)
VALUES 
('Barcelona', 10),
('Real Madrid', 8),
('Manchester City', 5),
('Boca Juniors', 12),
('Al Nassr', 15);

--  Jugadores
INSERT INTO Jugador (Nombre, Edad, Posicion, Dorsal, Asistencias, ValorMercado, EquipoId)
VALUES
('Lionel Messi', 36, 'Delantero', 10, 15, 50000000, 1),
('Karim Benzema', 35, 'Delantero', 9, 10, 25000000, 2),
('Kevin De Bruyne', 32, 'Mediocampista', 17, 20, 70000000, 3),
('Juan Román Riquelme', 28, 'Mediocampista', 10, 12, 15000000, 4),
('Pedri González', 21, 'Mediocampista', 8, 8, 60000000, 1),
('Vinícius Jr', 23, 'Extremo Izquierdo', 7, 18, 120000000, 2),
('Cristiano Ronaldo', 39, 'Delantero', 7, 12, 20000000, 5);

--  Torneos
INSERT INTO Torneo (Nombre, FechaInicio, FechaFin)
VALUES
('Champions League', '2025-02-15', '2025-06-01'),
('Copa Libertadores', '2025-03-10', '2025-07-30');

--  Relación equipo-torneo
INSERT INTO Equipo_Torneo (EquipoId, TorneoId)
VALUES
(1, 1), -- Barcelona en Champions
(2, 1), -- Real Madrid en Champions
(3, 1), -- Man City en Champions
(4, 2), -- Boca en Libertadores
(5, 1); -- Al Nassr en Champions

--  Cuerpo Técnico
INSERT INTO CuerpoTecnico (Nombre, Email, Descripcion, Rol, EquipoId)
VALUES
('Pep Guardiola', 'pep@city.com', 'Estratega de posesión', 'Director Técnico', 3),
('Carlo Ancelotti', 'carlo@realmadrid.com', 'Manejador de vestuario', 'Director Técnico', 2);

--  Cuerpo Médico
INSERT INTO CuerpoMedico (Nombre, Email, Descripcion, Especialidad, EquipoId)
VALUES
('Dr. Smith', 'drsmith@barcelona.com', 'Experto en lesiones musculares', 'Fisioterapeuta', 1),
('Dr. Pérez', 'drperez@boca.com', 'Especialista en rodilla', 'Traumatólogo', 4);

paquetes--
dotnet add package Microsoft.EntityFrameworkCore

dotnet add package Pomelo.EntityFrameworkCore.MySql --version 9.0.0-rc.1.efcore.9.0.0

dotnet add package Microsoft.EntityFrameworkCore.Design

dotnet add package Microsoft.Extensions.Configuration

dotnet add package Microsoft.Extensions.Configuration.Json

dotnet add package Microsoft.Extensions.Configuration.EnvironmentVariables

dotnet add package MySql.Data



