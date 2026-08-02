using TrabajoFinalPOS.Entidades;
using TrabajoFinalPOS.Servicios;

namespace TrabajoFinalPOS.UI;

public class MenuCatalogos : IMenu
{
    public string Titulo => "Gestionar Catálogos (Tipos de Teléfono / Correo)";

    private readonly GestorCatalogoConsola<TipoTelefono> _gestorTiposTelefono;
    private readonly GestorCatalogoConsola<TipoCorreo> _gestorTiposCorreo;

    public MenuCatalogos(IServicioCatalogo<TipoTelefono> servicioTiposTelefono, IServicioCatalogo<TipoCorreo> servicioTiposCorreo)
    {
        _gestorTiposTelefono = new GestorCatalogoConsola<TipoTelefono>(
            "Tipo de Teléfono", servicioTiposTelefono,
            () => new TipoTelefono { Descripcion = Consola.LeerTexto("Descripción (ej. Casa, Móvil): ") },
            t => t.Descripcion = Consola.LeerTexto($"Descripción [{t.Descripcion}]: ", obligatorio: false) is { Length: > 0 } nueva ? nueva : t.Descripcion);

        _gestorTiposCorreo = new GestorCatalogoConsola<TipoCorreo>(
            "Tipo de Correo", servicioTiposCorreo,
            () => new TipoCorreo { Descripcion = Consola.LeerTexto("Descripción (ej. Personal, Trabajo): ") },
            t => t.Descripcion = Consola.LeerTexto($"Descripción [{t.Descripcion}]: ", obligatorio: false) is { Length: > 0 } nueva ? nueva : t.Descripcion);
    }

    public void Mostrar()
    {
        var salir = false;
        while (!salir)
        {
            Consola.Limpiar();
            Console.WriteLine("=== Catálogos ===");
            Console.WriteLine("1. Tipos de Teléfono");
            Console.WriteLine("2. Tipos de Correo");
            Console.WriteLine("0. Volver");
            var opcion = Consola.LeerEntero("Seleccione una opción: ");
            switch (opcion)
            {
                case 1: _gestorTiposTelefono.EjecutarMenu(); break;
                case 2: _gestorTiposCorreo.EjecutarMenu(); break;
                case 0: salir = true; break;
                default:
                    Console.WriteLine("Opción inválida.");
                    Consola.Pausar();
                    break;
            }
        }
    }
}
