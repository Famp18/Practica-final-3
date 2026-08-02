using TrabajoFinalPOS.Entidades;
using TrabajoFinalPOS.Reportes;

namespace TrabajoFinalPOS.Servicios;

public interface IServicioVenta
{
    Venta Registrar(int clienteId, FormaPago formaPago, IEnumerable<(int productoId, int cantidad)> items);
    IReadOnlyList<Venta> Listar();
    ReporteVentas GenerarListadoDiario(DateOnly? fecha = null);
}
