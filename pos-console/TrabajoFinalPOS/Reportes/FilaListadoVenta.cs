namespace TrabajoFinalPOS.Reportes;

// DTO de una fila del "Listado Diario de Ventas" (mismo formato que el Excel original).
public record FilaListadoVenta(
    string NombreCliente,
    string Direccion,
    string Telefono,
    string Correo,
    int Cantidad,
    string Producto,
    string Marca,
    string Tipo,
    decimal Precio,
    decimal Monto,
    decimal Total,
    string Pago);
