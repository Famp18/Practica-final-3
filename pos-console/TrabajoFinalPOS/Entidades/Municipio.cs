namespace TrabajoFinalPOS.Entidades;

public class Municipio : EntidadBase
{
    public string Descripcion { get; set; } = string.Empty;
    public int ProvinciaId { get; set; }

    public override bool EsValida(out string error)
    {
        if (string.IsNullOrWhiteSpace(Descripcion))
        {
            error = "La descripción del municipio es obligatoria.";
            return false;
        }
        error = string.Empty;
        return true;
    }

    public override string ToString() => $"[{Id}] {Descripcion}";
}
