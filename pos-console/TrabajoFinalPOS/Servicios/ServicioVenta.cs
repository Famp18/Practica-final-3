using TrabajoFinalPOS.Entidades;
using TrabajoFinalPOS.Repositorios;
using TrabajoFinalPOS.Reportes;

namespace TrabajoFinalPOS.Servicios;

// Orquesta la creación de ventas y arma el "Listado Diario de Ventas".
// No conoce cómo se imprime el reporte (eso es responsabilidad de ImpresorReporteVentas) ni
// cómo se guardan los teléfonos/correos del cliente (eso lo delega a IServicioCliente).
public class ServicioVenta : IServicioVenta
{
    private readonly IRepositorio<Venta> _ventas;
    private readonly IRepositorio<Producto> _productos;
    private readonly IServicioCliente _servicioCliente;

    public ServicioVenta(IRepositorio<Venta> ventas, IRepositorio<Producto> productos, IServicioCliente servicioCliente)
    {
        _ventas = ventas;
        _productos = productos;
        _servicioCliente = servicioCliente;
    }

    public Venta Registrar(int clienteId, FormaPago formaPago, IEnumerable<(int productoId, int cantidad)> items)
    {
        var cliente = _servicioCliente.ObtenerPorId(clienteId)
            ?? throw new InvalidOperationException("El cliente indicado no existe.");

        var venta = new Venta { ClienteId = cliente.Id, FormaPago = formaPago };

        foreach (var (productoId, cantidad) in items)
        {
            var producto = _productos.ObtenerPorId(productoId)
                ?? throw new InvalidOperationException($"El producto {productoId} no existe.");

            var detalle = new DetalleVenta
            {
                ProductoId = producto.Id,
                NombreProducto = producto.Nombre,
                Marca = producto.Marca,
                Tipo = producto.Tipo,
                Cantidad = cantidad,
                PrecioUnitario = producto.Precio
            };

            if (!detalle.EsValida(out var errorDetalle))
                throw new InvalidOperationException(errorDetalle);

            venta.Detalles.Add(detalle);
        }

        if (!venta.EsValida(out var error))
            throw new InvalidOperationException(error);

        return _ventas.Agregar(venta);
    }

    public IReadOnlyList<Venta> Listar() => _ventas.ObtenerTodos();

    public ReporteVentas GenerarListadoDiario(DateOnly? fecha = null)
    {
        var ventas = _ventas.ObtenerTodos().AsEnumerable();
        if (fecha is not null)
            ventas = ventas.Where(v => DateOnly.FromDateTime(v.Fecha) == fecha);
        var ventasFiltradas = ventas.ToList();

        var filas = new List<FilaListadoVenta>();
        foreach (var venta in ventasFiltradas)
        {
            var cliente = _servicioCliente.ObtenerPorId(venta.ClienteId);
            var nombreCliente = cliente?.NombreCompleto ?? "Desconocido";
            var direccion = _servicioCliente.ObtenerDireccionPrincipalFormateada(venta.ClienteId);
            var telefono = _servicioCliente.ObtenerTelefonosFormateados(venta.ClienteId);
            var correo = _servicioCliente.ObtenerCorreosFormateados(venta.ClienteId);

            foreach (var detalle in venta.Detalles)
            {
                filas.Add(new FilaListadoVenta(
                    nombreCliente, direccion, telefono, correo,
                    detalle.Cantidad, detalle.NombreProducto, detalle.Marca, detalle.Tipo,
                    detalle.PrecioUnitario, detalle.Monto, venta.Total, venta.FormaPago.ToString()));
            }
        }

        var granTotal = ventasFiltradas.Sum(v => v.Total);
        return new ReporteVentas(filas, granTotal);
    }
}
