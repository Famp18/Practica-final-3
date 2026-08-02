namespace TrabajoFinalPOS.Entidades;

public class Cliente : EntidadBase
{
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string Identificacion { get; set; } = string.Empty;
    public DateTime FechaRegistro { get; set; } = DateTime.Now;
    public bool Activo { get; set; } = true;

    public string NombreCompleto => $"{Nombres} {Apellidos}";

    public override bool EsValida(out string error)
    {
        if (string.IsNullOrWhiteSpace(Nombres))
        {
            error = "El nombre es obligatorio.";
            return false;
        }
        if (string.IsNullOrWhiteSpace(Apellidos))
        {
            error = "El apellido es obligatorio.";
            return false;
        }
        if (string.IsNullOrWhiteSpace(Identificacion))
        {
            error = "La identificación es obligatoria.";
            return false;
        }
        error = string.Empty;
        return true;
    }

    public override string ToString() =>
        $"[{Id}] {NombreCompleto} - {Identificacion} - {(Activo ? "Activo" : "Inactivo")}";
}
