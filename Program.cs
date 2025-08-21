using System;
using System.Collections.Generic;
using System.Linq;
using torneo.Models;
using torneo.Repositorio;

class Program
{
    // Repositorios compartidos (asegúrate de que sean las versiones que usan tu BD)
    static EquipoRepositorio equipoRepo = new EquipoRepositorio();
    static JugadorRepositorio jugadorRepo = new JugadorRepositorio();
    static TorneoRepositorio torneoRepo = new TorneoRepositorio();

    static void Main()
    {
        while (true)
        {
            try
            {
                bool salir = false;

                while (!salir)
                {
                    Console.Clear();
                    Console.WriteLine("=== SISTEMA DE TORNEO ===");
                    Console.WriteLine("1. Gestionar Torneos");
                    Console.WriteLine("2. Menú de Jugadores");
                    Console.WriteLine("3. Menú de Equipos");
                    Console.WriteLine("4. Transferencias (Compra y Préstamos)");
                    Console.WriteLine("5. Estadísticas");
                    Console.WriteLine("6. Salir");
                    Console.Write("Seleccione una opción: ");

                    string opcion = Console.ReadLine() ?? "";

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
                            MenuTransferencias();
                            break;
                        case "5":
                            MenuEstadisticas();
                            break;
                        case "6":
                            salir = true;
                            break;
                        default:
                            Console.WriteLine("Opción no válida. Presione Enter...");
                            Console.ReadLine();
                            break;
                    }
                }

                // Salimos del programa si el usuario eligió 6
                return;
            }
            catch (Exception ex)
            {
                // Catch global para que nunca se cierre la app
                Console.WriteLine($"❌ Error inesperado: {ex.Message}");
                Console.WriteLine("La aplicación continuará. Presione Enter...");
                Console.ReadLine();
            }
        }
    }

    // ==========================
    //       TORNEOS
    // ==========================
    static void GestionarTorneos()
    {
        bool salirTorneos = false;

        while (!salirTorneos)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== GESTIONAR TORNEOS ===");
                Console.WriteLine("1. Añadir Torneo");
                Console.WriteLine("2. Buscar Torneo");
                Console.WriteLine("3. Eliminar Torneo");
                Console.WriteLine("4. Actualizar Torneo");
                Console.WriteLine("5. Volver al Menú Principal");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine() ?? "";

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
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error en Gestión de Torneos: {ex.Message}");
                Pause();
            }
        }
    }

    static void AñadirTorneo()
    {
        Console.Clear();
        Console.WriteLine("=== AÑADIR TORNEO ===");

        Console.Write("Nombre del torneo: ");
        string nombre = ReadNonEmptyString();

        Console.Write("Fecha de inicio (yyyy-mm-dd): ");
        DateTime fechaInicio = ReadDateTime();

        Console.Write("Fecha de fin (yyyy-mm-dd): ");
        DateTime fechaFin = ReadDateTime();

        try
        {
            Torneo torneo = new Torneo
            {
                Nombre = nombre,
                FechaInicio = fechaInicio,
                FechaFin = fechaFin
            };

            torneoRepo.Agregar(torneo);
            Console.WriteLine("✅ Torneo añadido con éxito.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ No se pudo agregar el torneo: {ex.Message}");
        }

        Pause();
    }

    static void BuscarTorneo()
    {
        Console.Clear();
        Console.WriteLine("=== BUSCAR TORNEO ===");

        int id = SeleccionarTorneo();
        if (id == -1) return;

        try
        {
            var torneo = torneoRepo.BuscarPorId(id);
            if (torneo != null)
            {
                Console.WriteLine($"ID: {torneo.Id} - Nombre: {torneo.Nombre} - Fechas: {torneo.FechaInicio:d} - {torneo.FechaFin:d}");
            }
            else
            {
                Console.WriteLine("Torneo no encontrado.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error al buscar torneo: {ex.Message}");
        }

        Pause();
    }

    static void EliminarTorneo()
    {
        Console.Clear();
        Console.WriteLine("=== ELIMINAR TORNEO ===");

        int id = SeleccionarTorneo();
        if (id == -1) return;

        try
        {
            var torneo = torneoRepo.BuscarPorId(id);
            if (torneo == null)
            {
                Console.WriteLine("Torneo no encontrado.");
            }
            else
            {
                torneoRepo.Eliminar(id);
                Console.WriteLine("✅ Torneo eliminado con éxito.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ No se pudo eliminar: {ex.Message}");
        }

        Pause();
    }

    static void ActualizarTorneo()
    {
        Console.Clear();
        Console.WriteLine("=== ACTUALIZAR TORNEO ===");

        int id = SeleccionarTorneo();
        if (id == -1) return;

        try
        {
            var torneo = torneoRepo.BuscarPorId(id);
            if (torneo != null)
            {
                Console.Write("Nuevo nombre del torneo: ");
                torneo.Nombre = ReadNonEmptyString();

                Console.Write("Nueva fecha de inicio (yyyy-mm-dd): ");
                torneo.FechaInicio = ReadDateTime();

                Console.Write("Nueva fecha de fin (yyyy-mm-dd): ");
                torneo.FechaFin = ReadDateTime();

                // Llamada a la firma que espera el repositorio (Actualizar(objeto))
                torneoRepo.Actualizar(torneo);

                Console.WriteLine("✅ Torneo actualizado con éxito.");
            }
            else
            {
                Console.WriteLine("Torneo no encontrado.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error al actualizar: {ex.Message}");
        }

        Pause();
    }

    // ==========================
    //       JUGADORES
    // ==========================
    static void MenuJugadores()
    {
        bool salir = false;
        while (!salir)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== MENÚ DE JUGADORES ===");
                Console.WriteLine("1. Registrar Jugador");
                Console.WriteLine("2. Buscar Jugador");
                Console.WriteLine("3. Editar Jugador");
                Console.WriteLine("4. Eliminar Jugador");
                Console.WriteLine("5. Volver al Menú Principal");
                Console.Write("Seleccione una opción: ");
                string opcion = Console.ReadLine() ?? "";

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
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error en Menú Jugadores: {ex.Message}");
                Pause();
            }
        }
    }

    static void RegistrarJugador()
    {
        Console.Clear();
        Console.WriteLine("=== REGISTRAR JUGADOR ===");

        Console.Write("Nombre: ");
        string nombre = ReadNonEmptyString();

        Console.Write("Edad: ");
        int edad = ReadInt(min: 0, max: 100);

        Console.Write("Posición: ");
        string posicion = ReadNonEmptyString();

        Console.Write("Dorsal: ");
        int dorsal = ReadInt(min: 0);

        Console.Write("Asistencias: ");
        int asistencias = ReadInt(min: 0);

        Console.Write("Valor de mercado: ");
        decimal valor = ReadDecimal(min: 0);

        // Mostrar lista de equipos y permitir 0 = Ninguno
        int equipoId = SeleccionarEquipoAllowNone(); // devuelve 0 si se elige ninguno, -1 si hay error
        if (equipoId == -1) equipoId = 0;

        try
        {
            // Si el usuario indicó un equipo distinto de 0, comprobamos su existencia
            if (equipoId > 0 && equipoRepo.BuscarPorId(equipoId) == null)
            {
                Console.WriteLine("⚠️ Equipo no encontrado. Se registrará el jugador sin equipo (EquipoId = 0).");
                equipoId = 0;
            }

            Jugador jugador = new Jugador
            {
                Nombre = nombre,
                Edad = edad,
                Posicion = posicion,
                Dorsal = dorsal,
                Asistencias = asistencias,
                ValorMercado = valor,
                EquipoId = equipoId
            };

            jugadorRepo.Agregar(jugador);

            Console.WriteLine("✅ Jugador registrado con éxito.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ No se pudo registrar el jugador: {ex.Message}");
        }

        Pause();
    }

    static void BuscarJugador()
    {
        Console.Clear();
        Console.WriteLine("=== BUSCAR JUGADOR ===");

        int id = SeleccionarJugador();
        if (id == -1) return;

        try
        {
            var jugador = jugadorRepo.BuscarPorId(id);
            if (jugador != null)
            {
                Console.WriteLine($"ID: {jugador.Id} - Nombre: {jugador.Nombre} - Edad: {jugador.Edad} - Posición: {jugador.Posicion} - EquipoId: {jugador.EquipoId} - Asist: {jugador.Asistencias} - Valor: {jugador.ValorMercado}");
            }
            else
            {
                Console.WriteLine("Jugador no encontrado.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error al buscar jugador: {ex.Message}");
        }

        Pause();
    }

    static void EditarJugador()
    {
        Console.Clear();
        Console.WriteLine("=== EDITAR JUGADOR ===");

        int id = SeleccionarJugador();
        if (id == -1) return;

        try
        {
            var jugador = jugadorRepo.BuscarPorId(id);
            if (jugador != null)
            {
                Console.Write($"Nuevo nombre ({jugador.Nombre}): ");
                string nuevoNombre = ReadOptionalString(jugador.Nombre);

                Console.Write($"Nueva edad ({jugador.Edad}): ");
                int nuevaEdad = ReadOptionalInt(jugador.Edad);

                Console.Write($"Nueva posición ({jugador.Posicion}): ");
                string nuevaPos = ReadOptionalString(jugador.Posicion);

                Console.Write($"Nuevo dorsal ({jugador.Dorsal}): ");
                int nuevoDorsal = ReadOptionalInt(jugador.Dorsal);

                Console.Write($"Nuevas asistencias ({jugador.Asistencias}): ");
                int nuevasAsist = ReadOptionalInt(jugador.Asistencias);

                Console.Write($"Nuevo valor mercado ({jugador.ValorMercado}): ");
                decimal nuevoValor = ReadOptionalDecimal(jugador.ValorMercado);

                Console.WriteLine($"Equipo actual: { (jugador.EquipoId > 0 ? (equipoRepo.BuscarPorId(jugador.EquipoId)?.Nombre ?? $"ID {jugador.EquipoId}") : "Sin equipo") }");
                Console.WriteLine("Seleccione nuevo equipo (o 0 para mantener el actual):");
                int nuevoEquipoId = SeleccionarEquipoAllowKeepCurrent(jugador.EquipoId);
                if (nuevoEquipoId == -2)
                {
                    // error / usuario decidió no cambiar, mantenemos actual
                    nuevoEquipoId = jugador.EquipoId;
                }

                if (nuevoEquipoId > 0 && equipoRepo.BuscarPorId(nuevoEquipoId) == null)
                {
                    Console.WriteLine("⚠️ Equipo destino no encontrado. Se conservará el EquipoId actual.");
                    nuevoEquipoId = jugador.EquipoId;
                }

                jugador.Nombre = nuevoNombre;
                jugador.Edad = nuevaEdad;
                jugador.Posicion = nuevaPos;
                jugador.Dorsal = nuevoDorsal;
                jugador.Asistencias = nuevasAsist;
                jugador.ValorMercado = nuevoValor;
                jugador.EquipoId = nuevoEquipoId;

                // Llamada a la firma Actualizar(jugador)
                jugadorRepo.Actualizar(jugador);
                Console.WriteLine("✅ Jugador actualizado con éxito.");
            }
            else
            {
                Console.WriteLine("Jugador no encontrado.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error al actualizar jugador: {ex.Message}");
        }

        Pause();
    }

    static void EliminarJugador()
    {
        Console.Clear();
        Console.WriteLine("=== ELIMINAR JUGADOR ===");

        int id = SeleccionarJugador();
        if (id == -1) return;

        try
        {
            var jugador = jugadorRepo.BuscarPorId(id);
            if (jugador == null)
            {
                Console.WriteLine("Jugador no encontrado.");
            }
            else
            {
                jugadorRepo.Eliminar(id);
                Console.WriteLine("✅ Jugador eliminado con éxito.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ No se pudo eliminar: {ex.Message}");
        }

        Pause();
    }

    // ==========================
    //         EQUIPOS
    // ==========================
    static void MenuEquipos()
    {
        bool salir = false;
        while (!salir)
        {
            try
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
                string opcion = Console.ReadLine() ?? "";

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
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error en Menú Equipos: {ex.Message}");
                Pause();
            }
        }
    }

    static void RegistrarEquipo()
    {
        Console.Clear();
        Console.WriteLine("=== REGISTRAR EQUIPO ===");
        Console.Write("Nombre del equipo: ");
        string nombre = ReadNonEmptyString();

        try
        {
            Equipo equipo = new Equipo { Nombre = nombre };
            equipoRepo.Agregar(equipo);

            Console.WriteLine("✅ Equipo registrado con éxito.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ No se pudo registrar el equipo: {ex.Message}");
        }

        Pause();
    }

    static void RegistrarCuerpoMedico()
    {
        Console.Clear();
        Console.WriteLine("=== REGISTRAR CUERPO MÉDICO ===");
        Console.Write("Nombre: ");
        string nombre = ReadNonEmptyString();

        Console.Write("Especialidad: ");
        string especialidad = ReadNonEmptyString();

        // Aquí puedes guardar el CuerpoMedico en BD si implementas ese repo
        Console.WriteLine("✅ Cuerpo médico registrado (notificación).");
        Pause();
    }

    static void RegistrarCuerpoTecnico()
    {
        Console.Clear();
        Console.WriteLine("=== REGISTRAR CUERPO TÉCNICO ===");
        Console.Write("Nombre: ");
        string nombre = ReadNonEmptyString();

        Console.Write("Rol (Ej: Director Técnico): ");
        string rol = ReadNonEmptyString();

        Console.WriteLine("✅ Cuerpo técnico registrado (notificación).");
        Pause();
    }

    static void InscribirEquipoEnTorneo()
    {
        Console.Clear();
        Console.WriteLine("=== INSCRIPCIÓN A TORNEO ===");

        int idEquipo = SeleccionarEquipo();
        if (idEquipo == -1) return;

        int idTorneo = SeleccionarTorneo();
        if (idTorneo == -1) return;

        // Si quieres persistir esta inscripción, implementa un repositorio de relation Equipo_Torneo
        Console.WriteLine($"✅ Equipo {idEquipo} inscrito en el torneo {idTorneo} (notificación).");
        Pause();
    }

    static void NotificarTransferencia()
    {
        Console.Clear();
        Console.WriteLine("=== NOTIFICACIÓN DE TRANSFERENCIA DE JUGADOR ===");

        int idJugador = SeleccionarJugador();
        if (idJugador == -1) return;

        int idEquipoDestino = SeleccionarEquipo();
        if (idEquipoDestino == -1) return;

        Console.WriteLine($"✅ Transferencia notificada: jugador {idJugador} -> equipo {idEquipoDestino}.");
        Pause();
    }

    static void SalirDelTorneo()
    {
        Console.Clear();
        Console.WriteLine("=== SALIDA DE EQUIPO DE TORNEO ===");

        int idEquipo = SeleccionarEquipo();
        if (idEquipo == -1) return;

        int idTorneo = SeleccionarTorneo();
        if (idTorneo == -1) return;

        Console.WriteLine($"✅ El equipo {idEquipo} ha sido retirado del torneo {idTorneo} (notificación).");
        Pause();
    }

    // ==========================
    //     TRANSFERENCIAS
    // ==========================
    static void MenuTransferencias()
    {
        bool salirMenuTransferencias = false;

        while (!salirMenuTransferencias)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== MENÚ TRANSFERENCIAS ===");
                Console.WriteLine("1. Comprar Jugador");
                Console.WriteLine("2. Prestar Jugador");
                Console.WriteLine("3. Volver al Menú Principal");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine() ?? "";

                switch (opcion)
                {
                    case "1":
                        ComprarJugador();
                        break;
                    case "2":
                        PrestarJugador();
                        break;
                    case "3":
                        salirMenuTransferencias = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Presione Enter...");
                        Console.ReadLine();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error en Menú Transferencias: {ex.Message}");
                Pause();
            }
        }
    }

    static void ComprarJugador()
    {
        Console.Clear();
        Console.WriteLine("=== COMPRA DE JUGADOR ===");

        int idJugador = SeleccionarJugador();
        if (idJugador == -1) return;

        int idEquipo = SeleccionarEquipo();
        if (idEquipo == -1) return;

        try
        {
            var jugador = jugadorRepo.BuscarPorId(idJugador);
            var equipoDestino = equipoRepo.BuscarPorId(idEquipo);

            if (jugador != null && equipoDestino != null)
            {
                // Transferencia: actualizamos el EquipoId del jugador y (si quieres) guardar historial
                jugadorRepo.TransferirJugador(jugador.Id, equipoDestino.Id);
                Console.WriteLine($"✅ El jugador {jugador.Nombre} fue transferido al {equipoDestino.Nombre}.");
            }
            else
            {
                Console.WriteLine("⚠ Jugador o equipo no encontrado.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error al comprar jugador: {ex.Message}");
        }

        Pause();
    }

    static void PrestarJugador()
    {
        Console.Clear();
        Console.WriteLine("=== PRÉSTAMO DE JUGADOR ===");

        int idJugador = SeleccionarJugador();
        if (idJugador == -1) return;

        int idEquipo = SeleccionarEquipo();
        if (idEquipo == -1) return;

        Console.Write("Duración del préstamo (meses): ");
        int meses = ReadInt(min: 1);

        try
        {
            var jugador = jugadorRepo.BuscarPorId(idJugador);
            var equipoDestino = equipoRepo.BuscarPorId(idEquipo);

            if (jugador != null && equipoDestino != null)
            {
                // Sin tabla de préstamos, persistimos como cambio de EquipoId
                jugadorRepo.TransferirJugador(jugador.Id, equipoDestino.Id);
                Console.WriteLine($"✅ El jugador {jugador.Nombre} fue prestado a {equipoDestino.Nombre} por {meses} meses.");
            }
            else
            {
                Console.WriteLine("⚠ Jugador o equipo no encontrado.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error en préstamo: {ex.Message}");
        }

        Pause();
    }

    // ==========================
    //       ESTADÍSTICAS
    // ==========================
    static void MenuEstadisticas()
    {
        bool salirMenuEstadisticas = false;

        while (!salirMenuEstadisticas)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== MENÚ ESTADÍSTICAS ===");
                Console.WriteLine("1. Jugador con más asistencias (torneos)");
                Console.WriteLine("2. Equipo con más goles en contra");
                Console.WriteLine("3. Jugadores más caros por equipo");
                Console.WriteLine("4. Jugadores mayores al promedio de edad por equipo");
                Console.WriteLine("5. Volver al Menú Principal");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine() ?? "";

                switch (opcion)
                {
                    case "1":
                        Jugador_Con_Mas_Asistencias_Torneos();
                        break;
                    case "2":
                        Equipo_Con_Mas_Goles_Contra();
                        break;
                    case "3":
                        Jugadores_Caros_x_Equipo();
                        break;
                    case "4":
                        Jugadores_Mayores_x_Equipo();
                        break;
                    case "5":
                        salirMenuEstadisticas = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Presione Enter...");
                        Console.ReadLine();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error en Menú Estadísticas: {ex.Message}");
                Pause();
            }
        }
    }

    static void Jugador_Con_Mas_Asistencias_Torneos()
    {
        Console.Clear();
        Console.WriteLine("=== JUGADOR CON MÁS ASISTENCIAS ===");

        try
        {
            var jugadores = jugadorRepo.ObtenerTodos();
            var top = jugadores.OrderByDescending(j => j.Asistencias).FirstOrDefault();

            if (top != null)
                Console.WriteLine($"Jugador con más asistencias: {top.Nombre} ({top.Asistencias} asistencias)");
            else
                Console.WriteLine("No hay jugadores registrados.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error al calcular: {ex.Message}");
        }

        Pause();
    }

    static void Equipo_Con_Mas_Goles_Contra()
    {
        Console.Clear();
        Console.WriteLine("=== EQUIPO CON MÁS GOLES EN CONTRA ===");

        try
        {
            var equipos = equipoRepo.ObtenerTodos();
            var top = equipos.OrderByDescending(e => e.GolesContra).FirstOrDefault();

            if (top != null)
                Console.WriteLine($"Equipo con más goles en contra: {top.Nombre} ({top.GolesContra} goles)");
            else
                Console.WriteLine("No hay equipos registrados.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error al calcular: {ex.Message}");
        }

        Pause();
    }

    static void Jugadores_Caros_x_Equipo()
    {
        Console.Clear();
        Console.WriteLine("=== JUGADORES MÁS CAROS POR EQUIPO ===");

        try
        {
            var equipos = equipoRepo.ObtenerTodos().ToDictionary(e => e.Id, e => e.Nombre);
            var jugadores = jugadorRepo.ObtenerTodos();

            if (equipos.Count == 0)
            {
                Console.WriteLine("No hay equipos registrados.");
                Pause();
                return;
            }

            var grupos = jugadores
                .Where(j => j.EquipoId > 0)
                .GroupBy(j => j.EquipoId)
                .OrderBy(g => g.Key);

            foreach (var g in grupos)
            {
                string nombreEquipo = equipos.ContainsKey(g.Key) ? equipos[g.Key] : $"EquipoId {g.Key}";
                var caro = g.OrderByDescending(j => j.ValorMercado).FirstOrDefault();

                if (caro != null)
                    Console.WriteLine($"Equipo: {nombreEquipo} - Jugador más caro: {caro.Nombre} (${caro.ValorMercado})");
                else
                    Console.WriteLine($"Equipo: {nombreEquipo} - Sin jugadores.");
            }

            // Equipos sin jugadores
            var equiposSinJugadores = equipos.Keys.Except(grupos.Select(x => x.Key));
            foreach (var idEquipo in equiposSinJugadores)
            {
                Console.WriteLine($"Equipo: {equipos[idEquipo]} - No tiene jugadores registrados.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error al calcular: {ex.Message}");
        }

        Pause();
    }

    static void Jugadores_Mayores_x_Equipo()
    {
        Console.Clear();
        Console.WriteLine("=== JUGADORES MAYORES AL PROMEDIO DE EDAD POR EQUIPO ===");

        try
        {
            var equipos = equipoRepo.ObtenerTodos().ToDictionary(e => e.Id, e => e.Nombre);
            var jugadores = jugadorRepo.ObtenerTodos();

            var grupos = jugadores
                .Where(j => j.EquipoId > 0)
                .GroupBy(j => j.EquipoId);

            if (!grupos.Any())
            {
                Console.WriteLine("No hay jugadores asignados a equipos.");
                Pause();
                return;
            }

            foreach (var g in grupos)
            {
                string nombreEquipo = equipos.ContainsKey(g.Key) ? equipos[g.Key] : $"EquipoId {g.Key}";
                double promedio = g.Average(j => j.Edad);
                var mayores = g.Where(j => j.Edad > promedio).OrderByDescending(j => j.Edad).ToList();

                Console.WriteLine($"\nEquipo: {nombreEquipo} (Promedio edad: {promedio:F1})");
                if (mayores.Count == 0)
                {
                    Console.WriteLine(" - Ningún jugador por encima del promedio.");
                }
                else
                {
                    foreach (var jugador in mayores)
                        Console.WriteLine($" - {jugador.Nombre} ({jugador.Edad} años)");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error al calcular: {ex.Message}");
        }

        Pause();
    }

    // ==========================
    //   HELPERS DE SELECCIÓN
    // ==========================
    static int SeleccionarJugador()
    {
        var jugadores = jugadorRepo.ObtenerTodos();
        if (jugadores == null || jugadores.Count == 0)
        {
            Console.WriteLine("⚠ No hay jugadores registrados.");
            Pause();
            return -1;
        }

        Console.WriteLine("\n--- LISTA DE JUGADORES ---");
        foreach (var j in jugadores)
            Console.WriteLine($"{j.Id}. {j.Nombre} ({j.Posicion}) - Edad: {j.Edad} - EquipoId: {j.EquipoId}");

        Console.Write("Ingrese el ID del jugador: ");
        return ReadInt(min: 1);
    }

    static int SeleccionarEquipo()
    {
        var equipos = equipoRepo.ObtenerTodos();
        if (equipos == null || equipos.Count == 0)
        {
            Console.WriteLine("⚠ No hay equipos registrados.");
            Pause();
            return -1;
        }

        Console.WriteLine("\n--- LISTA DE EQUIPOS ---");
        foreach (var e in equipos)
            Console.WriteLine($"{e.Id}. {e.Nombre} - GolesContra: {e.GolesContra}");

        Console.Write("Ingrese el ID del equipo: ");
        return ReadInt(min: 1);
    }

    // Versión para registrar jugador: permite elegir 0 = Ninguno
    static int SeleccionarEquipoAllowNone()
    {
        var equipos = equipoRepo.ObtenerTodos();
        if (equipos == null || equipos.Count == 0)
        {
            Console.WriteLine("⚠ No hay equipos registrados. Se registrará sin equipo (0).");
            Pause();
            return 0;
        }

        Console.WriteLine("\n--- LISTA DE EQUIPOS ---");
        Console.WriteLine("0. Ninguno (sin equipo)");
        foreach (var e in equipos)
            Console.WriteLine($"{e.Id}. {e.Nombre} - GolesContra: {e.GolesContra}");

        Console.Write("Ingrese el ID del equipo (o 0 para Ninguno): ");
        return ReadInt(min: 0);
    }

    // Versión para editar jugador: ofrecer mantener actual con 0 (o elegir otro)
    // Retorna: -2 = mantener actual (si el usuario deja en blanco), -1 = error/no equipos, >=0 => elegido (0 para "sin equipo")
    static int SeleccionarEquipoAllowKeepCurrent(int currentEquipoId)
    {
        var equipos = equipoRepo.ObtenerTodos();
        if (equipos == null || equipos.Count == 0)
        {
            Console.WriteLine("⚠ No hay equipos registrados. Se conservará el equipo actual.");
            Pause();
            return -2; // conservar actual
        }

        Console.WriteLine("\n--- LISTA DE EQUIPOS ---");
        Console.WriteLine("0. Ninguno (sin equipo)");
        foreach (var e in equipos)
            Console.WriteLine($"{e.Id}. {e.Nombre} - GolesContra: {e.GolesContra}");

        Console.Write($"Ingrese el ID del equipo (Enter para conservar el actual {currentEquipoId}): ");
        string? s = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(s)) return -2; // conservar actual
        if (int.TryParse(s, out int v) && v >= 0) return v;
        Console.WriteLine("Entrada inválida. Se conservará el valor actual.");
        return -2;
    }

    static int SeleccionarTorneo()
    {
        var torneos = torneoRepo.ObtenerTodos();
        if (torneos == null || torneos.Count == 0)
        {
            Console.WriteLine("⚠ No hay torneos registrados.");
            Pause();
            return -1;
        }

        Console.WriteLine("\n--- LISTA DE TORNEOS ---");
        foreach (var t in torneos)
            Console.WriteLine($"{t.Id}. {t.Nombre} - {t.FechaInicio:d} a {t.FechaFin:d}");

        Console.Write("Ingrese el ID del torneo: ");
        return ReadInt(min: 1);
    }

    // ==========================
    //   HELPERS DE ENTRADA
    // ==========================
    static void Pause()
    {
        Console.WriteLine("Presione Enter para continuar...");
        Console.ReadLine();
    }

    static string ReadNonEmptyString()
    {
        while (true)
        {
            string? s = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(s)) return s!.Trim();
            Console.Write("Entrada vacía. Intente de nuevo: ");
        }
    }

    static string ReadOptionalString(string current)
    {
        string? s = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(s)) return current;
        return s.Trim();
    }

    static int ReadInt(int min = int.MinValue, int max = int.MaxValue)
    {
        while (true)
        {
            string? s = Console.ReadLine();
            if (int.TryParse(s, out int v) && v >= min && v <= max) return v;
            Console.Write($"Número inválido. Ingrese un entero entre {min} y {max}: ");
        }
    }

    static int ReadOptionalInt(int current, int min = int.MinValue, int max = int.MaxValue)
    {
        string? s = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(s)) return current;
        if (int.TryParse(s, out int v) && v >= min && v <= max) return v;
        Console.WriteLine("Entrada inválida. Se conservará el valor actual.");
        return current;
    }

    static decimal ReadDecimal(decimal min = decimal.MinValue, decimal max = decimal.MaxValue)
    {
        while (true)
        {
            string? s = Console.ReadLine();
            if (decimal.TryParse(s, out decimal v) && v >= min && v <= max) return v;
            Console.Write($"Número inválido. Ingrese un decimal entre {min} y {max}: ");
        }
    }

    static decimal ReadOptionalDecimal(decimal current, decimal min = decimal.MinValue, decimal max = decimal.MaxValue)
    {
        string? s = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(s)) return current;
        if (decimal.TryParse(s, out decimal v) && v >= min && v <= max) return v;
        Console.WriteLine("Entrada inválida. Se conservará el valor actual.");
        return current;
    }

    static DateTime ReadDateTime()
    {
        while (true)
        {
            string? s = Console.ReadLine();
            if (DateTime.TryParse(s, out DateTime dt)) return dt;
            Console.Write("Fecha inválida. Formato esperado yyyy-mm-dd. Intente de nuevo: ");
        }
    }
}
