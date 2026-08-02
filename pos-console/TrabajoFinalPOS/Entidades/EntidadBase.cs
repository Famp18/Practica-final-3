namespace TrabajoFinalPOS.Entidades;

// Abstracción común: toda entidad tiene identidad y sabe validarse a sí misma (encapsulamiento de sus propias reglas).
public abstract class EntidadBase
{
    public int Id { get; set; }

    public abstract bool EsValida(out string error);
}
