using TrabajoFinalPOS.Entidades;
using TrabajoFinalPOS.Servicios;

namespace TrabajoFinalPOS.UI;

public class MenuUbicaciones : IMenu
{
    public string Titulo => "Gestionar Ubicaciones (Provincia / Municipio / Sección / Sector)";

    private readonly GestorCatalogoConsola<Provincia> _gestorProvincias;
    private readonly GestorCatalogoConsola<Municipio> _gestorMunicipios;
    private readonly GestorCatalogoConsola<Seccion> _gestorSecciones;
    private readonly GestorCatalogoConsola<Sector> _gestorSectores;

    public MenuUbicaciones(
        IServicioCatalogo<Provincia> servicioProvincias,
        IServicioCatalogo<Municipio> servicioMunicipios,
        IServicioCatalogo<Seccion> servicioSecciones,
        IServicioCatalogo<Sector> servicioSectores)
    {
        _gestorProvincias = new GestorCatalogoConsola<Provincia>(
            "Provincia", servicioProvincias,
            () => new Provincia { Descripcion = Consola.LeerTexto("Descripción: ") },
            p => p.Descripcion = Consola.LeerTexto($"Descripción [{p.Descripcion}]: ", obligatorio: false) is { Length: > 0 } nueva ? nueva : p.Descripcion);

        _gestorMunicipios = new GestorCatalogoConsola<Municipio>(
            "Municipio", servicioMunicipios,
            () => new Municipio
            {
                Descripcion = Consola.LeerTexto("Descripción: "),
                ProvinciaId = Consola.LeerEntero("Id de la provincia: ")
            },
            m =>
            {
                m.Descripcion = Consola.LeerTexto($"Descripción [{m.Descripcion}]: ", obligatorio: false) is { Length: > 0 } nueva ? nueva : m.Descripcion;
                m.ProvinciaId = Consola.LeerEntero($"Id de la provincia [{m.ProvinciaId}]: ");
            });

        _gestorSecciones = new GestorCatalogoConsola<Seccion>(
            "Sección", servicioSecciones,
            () => new Seccion
            {
                Descripcion = Consola.LeerTexto("Descripción: "),
                MunicipioId = Consola.LeerEntero("Id del municipio: ")
            },
            s =>
            {
                s.Descripcion = Consola.LeerTexto($"Descripción [{s.Descripcion}]: ", obligatorio: false) is { Length: > 0 } nueva ? nueva : s.Descripcion;
                s.MunicipioId = Consola.LeerEntero($"Id del municipio [{s.MunicipioId}]: ");
            });

        _gestorSectores = new GestorCatalogoConsola<Sector>(
            "Sector", servicioSectores,
            () => new Sector
            {
                Descripcion = Consola.LeerTexto("Descripción: "),
                MunicipioId = Consola.LeerEntero("Id del municipio: "),
                SeccionId = Consola.LeerEntero("Id de la sección: ")
            },
            s =>
            {
                s.Descripcion = Consola.LeerTexto($"Descripción [{s.Descripcion}]: ", obligatorio: false) is { Length: > 0 } nueva ? nueva : s.Descripcion;
                s.MunicipioId = Consola.LeerEntero($"Id del municipio [{s.MunicipioId}]: ");
                s.SeccionId = Consola.LeerEntero($"Id de la sección [{s.SeccionId}]: ");
            });
    }

    public void Mostrar()
    {
        var salir = false;
        while (!salir)
        {
            Consola.Limpiar();
            Console.WriteLine("=== Ubicaciones ===");
            Console.WriteLine("1. Provincias");
            Console.WriteLine("2. Municipios");
            Console.WriteLine("3. Secciones");
            Console.WriteLine("4. Sectores");
            Console.WriteLine("0. Volver");
            var opcion = Consola.LeerEntero("Seleccione una opción: ");
            switch (opcion)
            {
                case 1: _gestorProvincias.EjecutarMenu(); break;
                case 2: _gestorMunicipios.EjecutarMenu(); break;
                case 3: _gestorSecciones.EjecutarMenu(); break;
                case 4: _gestorSectores.EjecutarMenu(); break;
                case 0: salir = true; break;
                default:
                    Console.WriteLine("Opción inválida.");
                    Consola.Pausar();
                    break;
            }
        }
    }
}
