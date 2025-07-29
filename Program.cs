using System;
using torneo.Models;
using torneo.Repositorio;

class Program
{
    static EquipoRepositorio equipoRepo = new EquipoRepositorio();
    static JugadorRepositorio jugadorRepo = new JugadorRepositorio();
    static TorneoRepositorio torneoRepo = new TorneoRepositorio();

    static void Main()
    {
        bool salir = false;

        while (!salir)
        {
            Console.Clear();
            Console.WriteLine("=== SISTEMA DE TORNEO ===");
            Console.WriteLine("1. Gestionar Torneos");
            Console.WriteLine("2. Gestionar Equipos y Jugadores");
            Console.WriteLine("3. Salir");
            Console.Write("Seleccione una opción: ");

            string? opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    GestionarTorneos();
                    break;
                case "2":
                    MenuEquiposJugadores();
                    break;
                case "3":
                    salir = true;
                    break;
                default:
                    Console.WriteLine("Opción no válida. Presione Enter...");
                    Console.ReadLine();
                    break;
            }
        }
    }

    static void GestionarTorneos()
    {
        bool salirTorneos = false;

        while (!salirTorneos)
        {
            Console.Clear();
            Console.WriteLine("=== GESTIONAR TORNEOS ===");
            Console.WriteLine("1. Añadir Torneo");
            Console.WriteLine("2. Buscar Torneo");
            Console.WriteLine("3. Eliminar Torneo");
            Console.WriteLine("4. Actualizar Torneo");
            Console.WriteLine("5. Volver al Menú Principal");
            Console.Write("Seleccione una opción: ");

            string? opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    AñadirTorneo();
                    break;
                case "2":
                    BuscarTorneo();
                    break;
                case "3":
                    EliminarTorneo();
                    break;
                case "4":
                    ActualizarTorneo();
                    break;
                case "5":
                    salirTorneos = true;
                    break;
                default:
                    Console.WriteLine("Opción no válida. Presione Enter...");
                    Console.ReadLine();
                    break;
            }
        }
    }

    static void AñadirTorneo()
    {
        Console.Clear();
        Console.WriteLine("=== AÑADIR TORNEO ===");

        Console.Write("Nombre del torneo: ");
        string? nombre = Console.ReadLine();

        Console.Write("Fecha de inicio (yyyy-mm-dd): ");
        DateTime fechaInicio = DateTime.Parse(Console.ReadLine());

        Console.Write("Fecha de fin (yyyy-mm-dd): ");
        DateTime fechaFin = DateTime.Parse(Console.ReadLine());

        Torneo torneo = new Torneo
        {
            Nombre = nombre,
            FechaInicio = fechaInicio,
            FechaFin = fechaFin
        };

        torneoRepo.Agregar(torneo);

        Console.WriteLine("Torneo añadido con éxito.");
        Console.ReadLine();
    }

    static void BuscarTorneo()
    {
        Console.Clear();
        Console.WriteLine("=== BUSCAR TORNEO ===");

        Console.Write("Ingrese el ID del torneo: ");
        int id = int.Parse(Console.ReadLine());

        var torneo = torneoRepo.BuscarPorId(id);
        if (torneo != null)
        {
            Console.WriteLine($"ID: {torneo.Id} - Nombre: {torneo.Nombre} - Fechas: {torneo.FechaInicio} - {torneo.FechaFin}");
        }
        else
        {
            Console.WriteLine("Torneo no encontrado.");
        }

        Console.ReadLine();
    }

    static void EliminarTorneo()
    {
        Console.Clear();
        Console.WriteLine("=== ELIMINAR TORNEO ===");

        Console.Write("Ingrese el ID del torneo a eliminar: ");
        int id = int.Parse(Console.ReadLine());

        torneoRepo.Eliminar(id);

        Console.WriteLine(" Torneo eliminado con éxito.");
        Console.ReadLine();
    }

    static void ActualizarTorneo()
    {
        Console.Clear();
        Console.WriteLine("=== ACTUALIZAR TORNEO ===");

        Console.Write("Ingrese el ID del torneo a actualizar: ");
        int id = int.Parse(Console.ReadLine());

        var torneo = torneoRepo.BuscarPorId(id);
        if (torneo != null)
        {
            Console.Write("Nuevo nombre del torneo: ");
            torneo.Nombre = Console.ReadLine();

            Console.Write("Nueva fecha de inicio (yyyy-mm-dd): ");
            torneo.FechaInicio = DateTime.Parse(Console.ReadLine());

            Console.Write("Nueva fecha de fin (yyyy-mm-dd): ");
            torneo.FechaFin = DateTime.Parse(Console.ReadLine());

            torneoRepo.Actualizar(id, torneo);

            Console.WriteLine(" Torneo actualizado con éxito.");
        }
        else
        {
            Console.WriteLine("Torneo no encontrado.");
        }

        Console.ReadLine();
    }

    static void MenuEquiposJugadores()
    {
        bool salir = false;

        while (!salir)
        {
            Console.Clear();
            Console.WriteLine("=== SISTEMA DE TORNEO ===");
            Console.WriteLine("1. Registrar Equipo");
            Console.WriteLine("2. Registrar Jugador");
            Console.WriteLine("3. Ver Equipos");
            Console.WriteLine("4. Ver Jugadores");
            Console.WriteLine("5. Volver al Menú Principal");
            Console.Write("Seleccione una opción: ");

            string? opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    RegistrarEquipo();
                    break;
                case "2":
                    RegistrarJugador();
                    break;
                case "3":
                    VerEquipos();
                    break;
                case "4":
                    VerJugadores();
                    break;
                case "5":
                    salir = true;
                    break;
                default:
                    Console.WriteLine("Opción no válida. Presione Enter...");
                    Console.ReadLine();
                    break;
            }
        }
    }
    static void RegistrarEquipo()
    {
        Console.Clear();
        Console.WriteLine("=== REGISTRAR EQUIPO ===");
        Console.Write("Nombre del equipo: ");
        string? nombre = Console.ReadLine();

        Equipo equipo = new Equipo { Nombre = nombre };
        equipoRepo.Agregar(equipo);

        Console.WriteLine(" Equipo registrado con éxito.");
        Console.ReadLine();
    }

    static void RegistrarJugador()
    {
        Console.Clear();
        Console.WriteLine("=== REGISTRAR JUGADOR ===");
        Console.Write("Nombre: ");
        string? nombre = Console.ReadLine();

        Console.Write("Edad: ");
        int edad = int.Parse(Console.ReadLine());

        Console.Write("Posición: ");
        string? posicion = Console.ReadLine();

        Jugador jugador = new Jugador
        {
            Nombre = nombre,
            Edad = edad,
            Posicion = posicion
        };

        jugadorRepo.Agregar(jugador);

        Console.WriteLine(" Jugador registrado con éxito.");
        Console.ReadLine();
    }
    static void VerEquipos()
    {
        Console.Clear();
        Console.WriteLine("=== LISTA DE EQUIPOS ===");

        var equipos = equipoRepo.ObtenerTodos();
        if (equipos.Count == 0)
        {
            Console.WriteLine("No hay equipos registrados.");
        }
        else
        {
            foreach (var equipo in equipos)
            {
                Console.WriteLine($"ID: {equipo.Id} - Nombre: {equipo.Nombre}");
            }
        }

        Console.ReadLine();
    }

    static void VerJugadores()
    {
        Console.Clear();
        Console.WriteLine("=== LISTA DE JUGADORES ===");

        var jugadores = jugadorRepo.ObtenerTodos();
        if (jugadores.Count == 0)
        {
            Console.WriteLine("No hay jugadores registrados.");
        }
        else
        {
            foreach (var jugador in jugadores)
            {
                Console.WriteLine($"ID: {jugador.Id} - {jugador.Nombre} - Edad: {jugador.Edad} - Posición: {jugador.Posicion}");
            }
        }

        Console.ReadLine();
    }
}
