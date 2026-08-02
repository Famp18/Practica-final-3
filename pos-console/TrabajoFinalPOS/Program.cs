using TrabajoFinalPOS.Datos;
using TrabajoFinalPOS.Entidades;
using TrabajoFinalPOS.Repositorios;
using TrabajoFinalPOS.Servicios;
using TrabajoFinalPOS.UI;

namespace TrabajoFinalPOS;

// Composition root: único lugar donde se construyen las implementaciones concretas y se
// inyectan como abstracciones (interfaces) en los servicios y menús. Todo el resto del
// proyecto solo conoce interfaces (IRepositorio<T>, IServicioCatalogo<T>, IServicioCliente,
// IServicioVenta, IMenu), cumpliendo el principio de Inversión de Dependencias.
class Program
{
    static void Main(string[] args)
    {
        var repoProvincia = new RepositorioEnMemoria<Provincia>();
        var repoMunicipio = new RepositorioEnMemoria<Municipio>();
        var repoSeccion = new RepositorioEnMemoria<Seccion>();
        var repoSector = new RepositorioEnMemoria<Sector>();
        var repoTipoTelefono = new RepositorioEnMemoria<TipoTelefono>();
        var repoTipoCorreo = new RepositorioEnMemoria<TipoCorreo>();
        var repoDireccion = new RepositorioEnMemoria<Direccion>();
        var repoTelefono = new RepositorioEnMemoria<Telefono>();
        var repoCorreo = new RepositorioEnMemoria<Correo>();
        var repoCliente = new RepositorioCliente();
        var repoProducto = new RepositorioEnMemoria<Producto>();
        var repoVenta = new RepositorioEnMemoria<Venta>();

        var servicioProvincia = new ServicioCatalogo<Provincia>(repoProvincia);

        var servicioMunicipio = new ServicioCatalogo<Municipio>(repoMunicipio,
            m => repoProvincia.ObtenerPorId(m.ProvinciaId) is null ? "La provincia indicada no existe." : null);

        var servicioSeccion = new ServicioCatalogo<Seccion>(repoSeccion,
            s => repoMunicipio.ObtenerPorId(s.MunicipioId) is null ? "El municipio indicado no existe." : null);

        var servicioSector = new ServicioCatalogo<Sector>(repoSector, s =>
        {
            if (repoMunicipio.ObtenerPorId(s.MunicipioId) is null) return "El municipio indicado no existe.";
            if (repoSeccion.ObtenerPorId(s.SeccionId) is null) return "La sección indicada no existe.";
            return null;
        });

        var servicioTipoTelefono = new ServicioCatalogo<TipoTelefono>(repoTipoTelefono);
        var servicioTipoCorreo = new ServicioCatalogo<TipoCorreo>(repoTipoCorreo);
        var servicioProducto = new ServicioCatalogo<Producto>(repoProducto);

        var servicioCliente = new ServicioCliente(
            repoCliente, repoDireccion, repoTelefono, repoCorreo,
            repoProvincia, repoMunicipio, repoSeccion, repoSector,
            repoTipoTelefono, repoTipoCorreo);

        var servicioVenta = new ServicioVenta(repoVenta, repoProducto, servicioCliente);

        SembradorDatos.Sembrar(
            servicioProvincia, servicioMunicipio, servicioSeccion, servicioSector,
            servicioTipoTelefono, servicioTipoCorreo, servicioProducto,
            servicioCliente, servicioVenta);

        var submenus = new List<IMenu>
        {
            new MenuClientes(servicioCliente, servicioProvincia, servicioMunicipio, servicioSeccion,
                servicioSector, servicioTipoTelefono, servicioTipoCorreo),
            new MenuUbicaciones(servicioProvincia, servicioMunicipio, servicioSeccion, servicioSector),
            new MenuCatalogos(servicioTipoTelefono, servicioTipoCorreo),
            new MenuProductos(servicioProducto),
            new MenuVentas(servicioVenta, servicioCliente, servicioProducto)
        };

        try
        {
            new MenuPrincipal(submenus).Ejecutar();
        }
        catch (EntradaFinalizadaException)
        {
            Console.WriteLine();
            Console.WriteLine("Entrada finalizada. Cerrando la aplicación.");
        }
    }
}
