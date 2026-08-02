using TrabajoFinalPOS.Entidades;
using TrabajoFinalPOS.Reportes;
using TrabajoFinalPOS.Servicios;

namespace TrabajoFinalPOS.UI;

public class MenuVentas : IMenu
{
    public string Titulo => "Registrar Ventas y Ver Listado Diario";

    private readonly IServicioVenta _servicioVenta;
    private readonly IServicioCliente _servicioCliente;
    private readonly IServicioCatalogo<Producto> _servicioProducto;

    public MenuVentas(IServicioVenta servicioVenta, IServicioCliente servicioCliente, IServicioCatalogo<Producto> servicioProducto)
    {
        _servicioVenta = servicioVenta;
        _servicioCliente = servicioCliente;
        _servicioProducto = servicioProducto;
    }

    public void Mostrar()
    {
        var salir = false;
        while (!salir)
        {
            Consola.Limpiar();
            Console.WriteLine("=== Ventas ===");
            Console.WriteLine("1. Registrar nueva venta");
            Console.WriteLine("2. Ver listado diario de ventas (todas)");
            Console.WriteLine("3. Ver listado diario de ventas (por fecha)");
            Console.WriteLine("0. Volver");
            var opcion = Consola.LeerEntero("Seleccione una opción: ");
            switch (opcion)
            {
                case 1: RegistrarVenta(); break;
                case 2: MostrarListado(null); break;
                case 3: MostrarListadoPorFecha(); break;
                case 0: salir = true; break;
                default:
                    Console.WriteLine("Opción inválida.");
                    Consola.Pausar();
                    break;
            }
        }
    }

    private void RegistrarVenta()
    {
        Consola.Limpiar();
        Console.WriteLine("--- Registrar venta ---");
        try
        {
            Console.WriteLine("Clientes disponibles:");
            foreach (var cliente in _servicioCliente.Listar())
                Console.WriteLine($"  {cliente}");
            var clienteId = Consola.LeerEntero("Id del cliente: ");

            var items = new List<(int productoId, int cantidad)>();
            var agregarOtro = true;
            while (agregarOtro)
            {
                Console.WriteLine("Productos disponibles:");
                foreach (var producto in _servicioProducto.Listar())
                    Console.WriteLine($"  {producto}");
                var productoId = Consola.LeerEntero("Id del producto: ");
                var cantidad = Consola.LeerEntero("Cantidad: ");
                items.Add((productoId, cantidad));
                agregarOtro = Consola.LeerSiNo("¿Agregar otro producto a la venta?");
            }

            Console.WriteLine("Formas de pago disponibles:");
            foreach (var forma in Enum.GetValues<FormaPago>())
                Console.WriteLine($"  {(int)forma}. {forma}");
            var formaPago = (FormaPago)Consola.LeerEntero("Seleccione forma de pago: ");

            var venta = _servicioVenta.Registrar(clienteId, formaPago, items);
            Console.WriteLine($"Venta registrada con Id {venta.Id}. Total: RD${venta.Total:N2}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        Consola.Pausar();
    }

    private void MostrarListado(DateOnly? fecha)
    {
        Consola.Limpiar();
        var reporte = _servicioVenta.GenerarListadoDiario(fecha);
        ImpresorReporteVentas.Imprimir(reporte);
        Consola.Pausar();
    }

    private void MostrarListadoPorFecha()
    {
        Consola.Limpiar();
        Console.WriteLine("--- Listado por fecha ---");
        var texto = Consola.LeerTexto("Fecha (dd/MM/yyyy): ");
        if (DateOnly.TryParseExact(texto, "dd/MM/yyyy", out var fecha))
            MostrarListado(fecha);
        else
        {
            Console.WriteLine("Fecha inválida.");
            Consola.Pausar();
        }
    }
}
