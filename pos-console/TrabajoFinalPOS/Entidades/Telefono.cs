namespace TrabajoFinalPOS.Entidades;

public class Telefono : EntidadBase
{
    public int ClienteId { get; set; }
    public int TipoTelefonoId { get; set; }
    public string Numero { get; set; } = string.Empty;

    public override bool EsValida(out string error)
    {
        if (string.IsNullOrWhiteSpace(Numero))
        {
            error = "El número de teléfono es obligatorio.";
            return false;
        }
        error = string.Empty;
        return true;
    }

    public override string ToString() => Numero;
}
