
# 🏆 LeagueMaster - Sistema de Gestión de Torneos

**LeagueMaster** es un sistema completo de gestión de torneos de fútbol.  
Está diseñado con **MySQL** como base de datos relacional y preparado para integrarse con **.NET + Entity Framework Core**.

---

## 📂 Estructura del Proyecto

El sistema maneja las siguientes entidades principales:

- **Equipo 🏟️**: Clubes participantes en los torneos.
- **Jugador 👟**: Información de jugadores (edad, posición, dorsal, asistencias, valor de mercado).
- **Torneo 🏆**: Torneos nacionales e internacionales con fechas de inicio y fin.
- **Equipo_Torneo 🔗**: Relación entre equipos y torneos.
- **Cuerpo Técnico 🎓**: Entrenadores, asistentes y staff.
- **Cuerpo Médico 🏥**: Especialistas médicos de los equipos.
- **Transferencia 💸**: Historial de transferencias de jugadores entre equipos.

---

## 🛠️ Creación de la Base de Datos

```sql
DROP DATABASE IF EXISTS LeagueMasterDB;
CREATE DATABASE LeagueMasterDB;
USE LeagueMasterDB;
```

---

## 📊 Definición de Tablas

### Tabla: Equipo
```sql
CREATE TABLE Equipo (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    GolesContra INT DEFAULT 0
);
```

### Tabla: Jugador
```sql
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
```

### Tabla: Torneo
```sql
CREATE TABLE Torneo (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    FechaInicio DATE,
    FechaFin DATE
);
```

### Tabla: Equipo_Torneo
```sql
CREATE TABLE Equipo_Torneo (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    EquipoId INT NOT NULL,
    TorneoId INT NOT NULL,
    FOREIGN KEY (EquipoId) REFERENCES Equipo(Id) ON DELETE CASCADE,
    FOREIGN KEY (TorneoId) REFERENCES Torneo(Id) ON DELETE CASCADE
);
```

### Tabla: Cuerpo Técnico
```sql
CREATE TABLE CuerpoTecnico (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Email VARCHAR(100),
    Descripcion TEXT,
    Rol VARCHAR(50),
    EquipoId INT,
    FOREIGN KEY (EquipoId) REFERENCES Equipo(Id) ON DELETE CASCADE
);
```

### Tabla: Cuerpo Médico
```sql
CREATE TABLE CuerpoMedico (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Email VARCHAR(100),
    Descripcion TEXT,
    Especialidad VARCHAR(50),
    EquipoId INT,
    FOREIGN KEY (EquipoId) REFERENCES Equipo(Id) ON DELETE CASCADE
);
```

### Tabla: Transferencia
```sql
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
```

---

## 📊 Estadísticas y Consultas

```sql
-- Máximos goleadores (ordenar por asistencias como ejemplo)
SELECT Nombre, Asistencias FROM Jugador ORDER BY Asistencias DESC LIMIT 5;

-- Jugadores más valiosos
SELECT Nombre, ValorMercado FROM Jugador ORDER BY ValorMercado DESC LIMIT 5;

-- Promedio de edad por equipo
SELECT e.Nombre, AVG(j.Edad) AS PromedioEdad
FROM Jugador j
JOIN Equipo e ON j.EquipoId = e.Id
GROUP BY e.Nombre;

-- Valor total de mercado por equipo
SELECT e.Nombre, SUM(j.ValorMercado) AS ValorEquipo
FROM Jugador j
JOIN Equipo e ON j.EquipoId = e.Id
GROUP BY e.Nombre;

-- Equipos con más jugadores
SELECT e.Nombre, COUNT(j.Id) AS CantidadJugadores
FROM Jugador j
JOIN Equipo e ON j.EquipoId = e.Id
GROUP BY e.Nombre
ORDER BY CantidadJugadores DESC;
```

---

## 📥 Datos de Ejemplo (Inserts)

```sql
-- Equipos
INSERT INTO Equipo (Nombre, GolesContra) VALUES
('Barcelona', 10),
('Real Madrid', 8),
('Manchester City', 5),
('Boca Juniors', 12),
('Al Nassr', 15);

-- Jugadores
INSERT INTO Jugador (Nombre, Edad, Posicion, Dorsal, Asistencias, ValorMercado, EquipoId) VALUES
('Lionel Messi', 36, 'Delantero', 10, 15, 50000000, 1),
('Karim Benzema', 35, 'Delantero', 9, 10, 25000000, 2),
('Kevin De Bruyne', 32, 'Mediocampista', 17, 20, 70000000, 3),
('Juan Román Riquelme', 28, 'Mediocampista', 10, 12, 15000000, 4),
('Pedri González', 21, 'Mediocampista', 8, 8, 60000000, 1),
('Vinícius Jr', 23, 'Extremo Izquierdo', 7, 18, 120000000, 2),
('Cristiano Ronaldo', 39, 'Delantero', 7, 12, 20000000, 5);

-- Torneos
INSERT INTO Torneo (Nombre, FechaInicio, FechaFin) VALUES
('Champions League', '2025-02-15', '2025-06-01'),
('Copa Libertadores', '2025-03-10', '2025-07-30');

-- Relación Equipo-Torneo
INSERT INTO Equipo_Torneo (EquipoId, TorneoId) VALUES
(1, 1), (2, 1), (3, 1), (4, 2), (5, 1);

-- Cuerpo Técnico
INSERT INTO CuerpoTecnico (Nombre, Email, Descripcion, Rol, EquipoId) VALUES
('Pep Guardiola', 'pep@city.com', 'Estratega de posesión', 'Director Técnico', 3),
('Carlo Ancelotti', 'carlo@realmadrid.com', 'Manejador de vestuario', 'Director Técnico', 2);

-- Cuerpo Médico
INSERT INTO CuerpoMedico (Nombre, Email, Descripcion, Especialidad, EquipoId) VALUES
('Dr. Smith', 'drsmith@barcelona.com', 'Experto en lesiones musculares', 'Fisioterapeuta', 1),
('Dr. Pérez', 'drperez@boca.com', 'Especialista en rodilla', 'Traumatólogo', 4);
```

---


## 🚀 Configuración con .NET y EF Core

Este proyecto está preparado para trabajar con Entity Framework Core y MySQL.  
Instala los siguientes paquetes NuGet:

```bash
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Pomelo.EntityFrameworkCore.MySql --version 9.0.0-rc.1.efcore.9.0.0
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.Extensions.Configuration
dotnet add package Microsoft.Extensions.Configuration.Json
dotnet add package Microsoft.Extensions.Configuration.EnvironmentVariables
dotnet add package MySql.Data
```

---

## 📌 Requisitos

- MySQL 8.0+
- .NET 7 o superior
- Entity Framework Core 9
