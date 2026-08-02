using TrabajoFinalPOS.Entidades;
using TrabajoFinalPOS.Servicios;

namespace TrabajoFinalPOS.UI;

// Encapsula el ciclo Listar/Crear/Editar/Eliminar por consola para CUALQUIER catálogo.
// Reutilizado por Provincia, Municipio, Sección, Sector, TipoTelefono, TipoCorreo y Producto:
// evita duplicar 7 veces el mismo menú (DRY) y demuestra genéricos + inyección de comportamiento
// (los delegados "construir" y "editar" son el único código específico de cada entidad).
public class GestorCatalogoConsola<T> where T : EntidadBase
{
    private readonly string _nombreEntidad;
    private readonly IServicioCatalogo<T> _servicio;
    private readonly Func<T> _construir;
    private readonly Action<T> _editar;

    public GestorCatalogoConsola(string nombreEntidad, IServicioCatalogo<T> servicio, Func<T> construir, Action<T> editar)
    {
        _nombreEntidad = nombreEntidad;
        _servicio = servicio;
        _construir = construir;
        _editar = editar;
    }

    public void EjecutarMenu()
    {
        var salir = false;
        while (!salir)
        {
            Consola.Limpiar();
            Console.WriteLine($"=== {_nombreEntidad} ===");
            Console.WriteLine("1. Listar");
            Console.WriteLine("2. Crear");
            Console.WriteLine("3. Editar");
            Console.WriteLine("4. Eliminar");
            Console.WriteLine("0. Volver");
            var opcion = Consola.LeerEntero("Seleccione una opción: ");
            switch (opcion)
            {
                case 1: Listar(); break;
                case 2: Crear(); break;
                case 3: Editar(); break;
                case 4: Eliminar(); break;
                case 0: salir = true; break;
                default:
                    Console.WriteLine("Opción inválida.");
                    Consola.Pausar();
                    break;
            }
        }
    }

    private void Listar()
    {
        Consola.Limpiar();
        Console.WriteLine($"--- {_nombreEntidad}: listado ---");
        var elementos = _servicio.Listar();
        if (elementos.Count == 0)
            Console.WriteLine("No hay registros.");
        else
            foreach (var elemento in elementos)
                Console.WriteLine(elemento);
        Consola.Pausar();
    }

    private void Crear()
    {
        Consola.Limpiar();
        Console.WriteLine($"--- {_nombreEntidad}: nuevo registro ---");
        try
        {
            var entidad = _construir();
            _servicio.Crear(entidad);
            Console.WriteLine($"{_nombreEntidad} creado con Id {entidad.Id}.");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        Consola.Pausar();
    }

    private void Editar()
    {
        Consola.Limpiar();
        Console.WriteLine($"--- {_nombreEntidad}: editar registro ---");
        var id = Consola.LeerEntero("Id a editar: ");
        var entidad = _servicio.ObtenerPorId(id);
        if (entidad is null)
        {
            Console.WriteLine("No existe un registro con ese Id.");
            Consola.Pausar();
            return;
        }
        try
        {
            _editar(entidad);
            _servicio.Actualizar(entidad);
            Console.WriteLine("Registro actualizado.");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        Consola.Pausar();
    }

    private void Eliminar()
    {
        Consola.Limpiar();
        Console.WriteLine($"--- {_nombreEntidad}: eliminar registro ---");
        var id = Consola.LeerEntero("Id a eliminar: ");
        try
        {
            _servicio.Eliminar(id);
            Console.WriteLine("Registro eliminado.");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        Consola.Pausar();
    }
}
