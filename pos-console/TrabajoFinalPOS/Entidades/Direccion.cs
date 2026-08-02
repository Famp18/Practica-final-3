namespace TrabajoFinalPOS.Entidades;

public class Direccion : EntidadBase
{
    public int ClienteId { get; set; }
    public string Calle { get; set; } = string.Empty;
    public string? No { get; set; }
    public string? Apto { get; set; }
    public string? Residencial { get; set; }
    public int ProvinciaId { get; set; }
    public int MunicipioId { get; set; }
    public int SeccionId { get; set; }
    public int SectorId { get; set; }
    public string? Referencia { get; set; }

    public override bool EsValida(out string error)
    {
        if (string.IsNullOrWhiteSpace(Calle))
        {
            error = "La calle es obligatoria.";
            return false;
        }
        error = string.Empty;
        return true;
    }

    public override string ToString() => $"[{Id}] C/{Calle} #{No}, {Residencial}";
}
