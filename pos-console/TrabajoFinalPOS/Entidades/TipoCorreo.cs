namespace TrabajoFinalPOS.Entidades;

public class TipoCorreo : EntidadBase
{
    public string Descripcion { get; set; } = string.Empty;

    public override bool EsValida(out string error)
    {
        if (string.IsNullOrWhiteSpace(Descripcion))
        {
            error = "La descripción del tipo de correo es obligatoria.";
            return false;
        }
        error = string.Empty;
        return true;
    }

    public override string ToString() => $"[{Id}] {Descripcion}";
}
