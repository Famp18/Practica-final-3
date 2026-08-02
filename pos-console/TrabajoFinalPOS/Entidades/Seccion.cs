namespace TrabajoFinalPOS.Entidades;

public class Seccion : EntidadBase
{
    public string Descripcion { get; set; } = string.Empty;
    public int MunicipioId { get; set; }

    public override bool EsValida(out string error)
    {
        if (string.IsNullOrWhiteSpace(Descripcion))
        {
            error = "La descripción de la sección es obligatoria.";
            return false;
        }
        error = string.Empty;
        return true;
    }

    public override string ToString() => $"[{Id}] {Descripcion}";
}
