namespace TrabajoFinalPOS.Entidades;

public class Venta : EntidadBase
{
    public int ClienteId { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Now;
    public FormaPago FormaPago { get; set; }
    public List<DetalleVenta> Detalles { get; } = new();

    public decimal Total => Detalles.Sum(d => d.Monto);

    public override bool EsValida(out string error)
    {
        if (Detalles.Count == 0)
        {
            error = "La venta debe tener al menos un detalle.";
            return false;
        }
        error = string.Empty;
        return true;
    }

    public override string ToString() =>
        $"[{Id}] Venta del {Fecha:dd/MM/yyyy} - Total: RD${Total:N2} - Pago: {FormaPago}";
}
