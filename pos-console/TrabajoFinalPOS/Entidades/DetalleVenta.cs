namespace TrabajoFinalPOS.Entidades;

// Guarda una "foto" del producto al momento de vender (nombre, marca, tipo, precio),
// para que el listado histórico no cambie si luego se edita el catálogo de productos.
public class DetalleVenta : EntidadBase
{
    public int ProductoId { get; set; }
    public string NombreProducto { get; set; } = string.Empty;
    public string Marca { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }

    public decimal Monto => Cantidad * PrecioUnitario;

    public override bool EsValida(out string error)
    {
        if (Cantidad <= 0)
        {
            error = "La cantidad debe ser mayor a cero.";
            return false;
        }
        error = string.Empty;
        return true;
    }

    public override string ToString() => $"{Cantidad} x {NombreProducto} ({Marca}) = RD${Monto:N2}";
}
