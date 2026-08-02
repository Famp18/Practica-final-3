namespace TrabajoFinalPOS.Entidades;

public class Correo : EntidadBase
{
    public int ClienteId { get; set; }
    public int TipoCorreoId { get; set; }
    public string Direccion { get; set; } = string.Empty;

    public override bool EsValida(out string error)
    {
        if (string.IsNullOrWhiteSpace(Direccion) || !Direccion.Contains('@'))
        {
            error = "La dirección de correo no es válida.";
            return false;
        }
        error = string.Empty;
        return true;
    }

    public override string ToString() => Direccion;
}
