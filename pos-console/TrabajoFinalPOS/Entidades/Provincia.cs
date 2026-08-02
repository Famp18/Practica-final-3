namespace TrabajoFinalPOS.Entidades;

public class Provincia : EntidadBase
{
    public string Descripcion { get; set; } = string.Empty;

    public override bool EsValida(out string error)
    {
        if (string.IsNullOrWhiteSpace(Descripcion))
        {
            error = "La descripción de la provincia es obligatoria.";
            return false;
        }
        error = string.Empty;
        return true;
    }

    public override string ToString() => $"[{Id}] {Descripcion}";
}
