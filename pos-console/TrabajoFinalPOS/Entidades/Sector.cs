namespace TrabajoFinalPOS.Entidades;

public class Sector : EntidadBase
{
    public string Descripcion { get; set; } = string.Empty;
    public int MunicipioId { get; set; }
    public int SeccionId { get; set; }

    public override bool EsValida(out string error)
    {
        if (string.IsNullOrWhiteSpace(Descripcion))
        {
            error = "La descripción del sector es obligatoria.";
            return false;
        }
        error = string.Empty;
        return true;
    }

    public override string ToString() => $"[{Id}] {Descripcion}";
}
