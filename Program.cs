using System;
using torneo.Models;
using torneo.Repositorio;

class Program
{
    static EquipoRepositorio equipoRepo = new EquipoRepositorio();
    static JugadorRepositorio jugadorRepo = new JugadorRepositorio();

    static void Main()
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
            Console.WriteLine("5. Salir");
            Console.Write("Seleccione una opción: ");

            string opcion = Console.ReadLine();

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

        Console.WriteLine("✅ Equipo registrado con éxito.");
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

        Console.WriteLine("✅ Jugador registrado con éxito.");
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
