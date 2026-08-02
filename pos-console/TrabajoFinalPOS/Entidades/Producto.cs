namespace TrabajoFinalPOS.Entidades;

public class Producto : EntidadBase
{
    public string Nombre { get; set; } = string.Empty;
    public string Marca { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public decimal Precio { get; set; }

    public override bool EsValida(out string error)
    {
        if (string.IsNullOrWhiteSpace(Nombre))
        {
            error = "El nombre del producto es obligatorio.";
            return false;
        }
        if (Precio <= 0)
        {
            error = "El precio debe ser mayor a cero.";
            return false;
        }
        error = string.Empty;
        return true;
    }

    public override string ToString() => $"[{Id}] {Nombre} ({Marca} - {Tipo}) - RD${Precio:N2}";
}
