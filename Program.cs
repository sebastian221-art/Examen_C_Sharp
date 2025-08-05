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
    Console.WriteLine("2. Menú de Jugadores");
    Console.WriteLine("3. Menú de Equipos");
    Console.WriteLine("4. Salir");
    Console.Write("Seleccione una opción: ");

    string? opcion = Console.ReadLine();

    switch (opcion)
    {
        case "1":
            GestionarTorneos();
            break;
        case "2":
            MenuJugadores();
            break;
        case "3":
            MenuEquipos();
            break;
        case "4":
            salir = true;
            break;
        default:
            Console.WriteLine("Opción no válida. Presione Enter...");
            Console.ReadLine();
            break;
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

   static void MenuJugadores()
{
    bool salir = false;
    while (!salir)
    {
        Console.Clear();
        Console.WriteLine("=== MENÚ DE JUGADORES ===");
        Console.WriteLine("1. Registrar Jugador");
        Console.WriteLine("2. Buscar Jugador");
        Console.WriteLine("3. Editar Jugador");
        Console.WriteLine("4. Eliminar Jugador");
        Console.WriteLine("5. Volver al Menú Principal");
        Console.Write("Seleccione una opción: ");
        string? opcion = Console.ReadLine();

        switch (opcion)
        {
            case "1":
                RegistrarJugador();
                break;
            case "2":
                BuscarJugador();
                break;
            case "3":
                EditarJugador();
                break;
            case "4":
                EliminarJugador();
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

    Console.WriteLine("Jugador registrado con éxito.");
    Console.ReadLine();
}

static void BuscarJugador()
{
    Console.Clear();
    Console.WriteLine("=== BUSCAR JUGADOR ===");
    Console.Write("Ingrese el ID del jugador: ");
    int id = int.Parse(Console.ReadLine());

    var jugador = jugadorRepo.BuscarPorId(id);
    if (jugador != null)
    {
        Console.WriteLine($"ID: {jugador.Id} - Nombre: {jugador.Nombre} - Edad: {jugador.Edad} - Posición: {jugador.Posicion}");
    }
    else
    {
        Console.WriteLine("Jugador no encontrado.");
    }

    Console.ReadLine();
}

static void EditarJugador()
{
    Console.Clear();
    Console.WriteLine("=== EDITAR JUGADOR ===");
    Console.Write("Ingrese el ID del jugador a editar: ");
    int id = int.Parse(Console.ReadLine());

    var jugador = jugadorRepo.BuscarPorId(id);
    if (jugador != null)
    {
        Console.Write("Nuevo nombre: ");
        jugador.Nombre = Console.ReadLine();

        Console.Write("Nueva edad: ");
        jugador.Edad = int.Parse(Console.ReadLine());

        Console.Write("Nueva posición: ");
        jugador.Posicion = Console.ReadLine();

        jugadorRepo.Actualizar(id, jugador);
        Console.WriteLine("Jugador actualizado con éxito.");
    }
    else
    {
        Console.WriteLine("Jugador no encontrado.");
    }

    Console.ReadLine();
}

static void EliminarJugador()
{
    Console.Clear();
    Console.WriteLine("=== ELIMINAR JUGADOR ===");
    Console.Write("Ingrese el ID del jugador a eliminar: ");
    int id = int.Parse(Console.ReadLine());

    jugadorRepo.Eliminar(id);
    Console.WriteLine("Jugador eliminado con éxito.");
    Console.ReadLine();
}

static void MenuEquipos()
{
    bool salir = false;
    while (!salir)
    {
        Console.Clear();
        Console.WriteLine("=== MENÚ DE EQUIPOS ===");
        Console.WriteLine("1. Registrar Equipo");
        Console.WriteLine("2. Registrar Cuerpo Médico");
        Console.WriteLine("3. Registrar Cuerpo Técnico");
        Console.WriteLine("4. Inscripción a Torneo");
        Console.WriteLine("5. Notificar Transferencia de Jugador");
        Console.WriteLine("6. Salir del Torneo");
        Console.WriteLine("7. Volver al Menú Principal");
        Console.Write("Seleccione una opción: ");
        string? opcion = Console.ReadLine();

        switch (opcion)
        {
            case "1":
                RegistrarEquipo();
                break;
            case "2":
                RegistrarCuerpoMedico();
                break;
            case "3":
                RegistrarCuerpoTecnico();
                break;
            case "4":
                InscribirEquipoEnTorneo();
                break;
            case "5":
                NotificarTransferencia();
                break;
            case "6":
                SalirDelTorneo();
                break;
            case "7":
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

            Console.WriteLine("Equipo registrado con éxito.");
            Console.ReadLine();
        }

static void RegistrarCuerpoMedico()
{
    Console.Clear();
    Console.WriteLine("=== REGISTRAR CUERPO MÉDICO ===");
    Console.Write("Nombre: ");
    string? nombre = Console.ReadLine();

    Console.Write("Especialidad: ");
    string? especialidad = Console.ReadLine();

    CuerpoMedico medico = new CuerpoMedico
    {
        Nombre = nombre,
        Especialidad = especialidad
    };


    Console.WriteLine("Cuerpo médico registrado con éxito.");
    Console.ReadLine();
}

static void RegistrarCuerpoTecnico()
{
    Console.Clear();
    Console.WriteLine("=== REGISTRAR CUERPO TÉCNICO ===");
    Console.Write("Nombre: ");
    string? nombre = Console.ReadLine();

    Console.Write("Rol (Ej: Director Técnico): ");
    string? rol = Console.ReadLine();

    CuerpoTecnico tecnico = new CuerpoTecnico
    {
        Nombre = nombre,
        Rol = rol
    };

    Console.WriteLine("Cuerpo técnico registrado con éxito.");
    Console.ReadLine();
}

static void InscribirEquipoEnTorneo()
{
    Console.Clear();
    Console.WriteLine("=== INSCRIPCIÓN A TORNEO ===");

    Console.Write("ID del equipo: ");
    int idEquipo = int.Parse(Console.ReadLine());

    Console.Write("ID del torneo: ");
    int idTorneo = int.Parse(Console.ReadLine());


    Console.WriteLine($"Equipo {idEquipo} inscrito en el torneo {idTorneo} con éxito.");
    Console.ReadLine();
}

static void NotificarTransferencia()
{
    Console.Clear();
    Console.WriteLine("=== NOTIFICACIÓN DE TRANSFERENCIA DE JUGADOR ===");

    Console.Write("ID del jugador transferido: ");
    int idJugador = int.Parse(Console.ReadLine());

    Console.Write("Nuevo equipo destino: ");
    string? nuevoEquipo = Console.ReadLine();


    Console.WriteLine($"Transferencia del jugador {idJugador} al equipo '{nuevoEquipo}' notificada con éxito.");
    Console.ReadLine();
}

static void SalirDelTorneo()
        {
            Console.Clear();
            Console.WriteLine("=== SALIDA DE EQUIPO DE TORNEO ===");

            Console.Write("ID del equipo: ");
            int idEquipo = int.Parse(Console.ReadLine());

            Console.Write("ID del torneo: ");
            int idTorneo = int.Parse(Console.ReadLine());


            Console.WriteLine($"El equipo {idEquipo} ha sido retirado del torneo {idTorneo}.");
            Console.ReadLine();
        }
    }
}