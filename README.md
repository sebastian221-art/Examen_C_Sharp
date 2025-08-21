# 🏆 LeagueMaster - Sistema de Gestión de Torneos

**DataLeaguePro** es un sistema de base de datos relacional diseñado para gestionar torneos de fútbol, equipos, jugadores, transferencias, cuerpos técnicos y médicos.  
Está implementado en **MySQL** y preparado para ser utilizado con **.NET + Entity Framework Core**.

---

## 📂 Estructura del Proyecto

El sistema cuenta con las siguientes entidades principales:

- **Equipo** 🏟️: Representa a cada club participante (nombre, goles en contra).  
- **Jugador** 👟: Información detallada de los futbolistas (edad, posición, dorsal, asistencias, valor de mercado).  
- **Torneo** 🏆: Torneos nacionales e internacionales (nombre, fecha de inicio y fin).  
- **Equipo_Torneo** 🔗: Relación entre equipos y torneos.  
- **Cuerpo Técnico** 🎓: Entrenadores, asistentes y otros roles del staff.  
- **Cuerpo Médico** 🏥: Especialistas médicos vinculados a los equipos.  
- **Transferencia** 💸: Historial de transferencias de jugadores entre clubes.  

---

## 🛠️ Creación de la Base de Datos

```sql
DROP DATABASE IF EXISTS TorneoDB;
CREATE DATABASE TorneoDB;
USE TorneoDB;
```

---

## 📊 Tablas y Relaciones

### Tabla: **Equipo**
```sql
CREATE TABLE Equipo (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    GolesContra INT DEFAULT 0
);
```

### Tabla: **Jugador**
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

### Tabla: **Torneo**
```sql
CREATE TABLE Torneo (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    FechaInicio DATE,
    FechaFin DATE
);
```

### Tabla: **Equipo_Torneo**
```sql
CREATE TABLE Equipo_Torneo (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    EquipoId INT NOT NULL,
    TorneoId INT NOT NULL,
    FOREIGN KEY (EquipoId) REFERENCES Equipo(Id) ON DELETE CASCADE,
    FOREIGN KEY (TorneoId) REFERENCES Torneo(Id) ON DELETE CASCADE
);
```

### Tabla: **Cuerpo Técnico**
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

### Tabla: **Cuerpo Médico**
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

### Tabla: **Transferencia**
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

## 📥 Datos de Ejemplo (Inserts)

Incluye algunos equipos, jugadores, torneos y staff técnico/médico:

```sql
-- Equipos
INSERT INTO Equipo (Nombre, GolesContra)
VALUES 
('Barcelona', 10),
('Real Madrid', 8),
('Manchester City', 5),
('Boca Juniors', 12),
('Al Nassr', 15);

-- Jugadores
INSERT INTO Jugador (Nombre, Edad, Posicion, Dorsal, Asistencias, ValorMercado, EquipoId)
VALUES
('Lionel Messi', 36, 'Delantero', 10, 15, 50000000, 1),
('Karim Benzema', 35, 'Delantero', 9, 10, 25000000, 2),
('Kevin De Bruyne', 32, 'Mediocampista', 17, 20, 70000000, 3),
('Juan Román Riquelme', 28, 'Mediocampista', 10, 12, 15000000, 4),
('Pedri González', 21, 'Mediocampista', 8, 8, 60000000, 1),
('Vinícius Jr', 23, 'Extremo Izquierdo', 7, 18, 120000000, 2),
('Cristiano Ronaldo', 39, 'Delantero', 7, 12, 20000000, 5);

-- Torneos
INSERT INTO Torneo (Nombre, FechaInicio, FechaFin)
VALUES
('Champions League', '2025-02-15', '2025-06-01'),
('Copa Libertadores', '2025-03-10', '2025-07-30');

-- Relación Equipo-Torneo
INSERT INTO Equipo_Torneo (EquipoId, TorneoId)
VALUES
(1, 1), (2, 1), (3, 1), (4, 2), (5, 1);

-- Cuerpo Técnico
INSERT INTO CuerpoTecnico (Nombre, Email, Descripcion, Rol, EquipoId)
VALUES
('Pep Guardiola', 'pep@city.com', 'Estratega de posesión', 'Director Técnico', 3),
('Carlo Ancelotti', 'carlo@realmadrid.com', 'Manejador de vestuario', 'Director Técnico', 2);

-- Cuerpo Médico
INSERT INTO CuerpoMedico (Nombre, Email, Descripcion, Especialidad, EquipoId)
VALUES
('Dr. Smith', 'drsmith@barcelona.com', 'Experto en lesiones musculares', 'Fisioterapeuta', 1),
('Dr. Pérez', 'drperez@boca.com', 'Especialista en rodilla', 'Traumatólogo', 4);
```

---

## 🚀 Configuración con .NET y EF Core

Este proyecto está preparado para trabajar con **Entity Framework Core** y **MySQL**.  
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

- **MySQL 8.0+**
- **.NET 7 o superior**
- **Entity Framework Core 9**

---

