using TrabajoFinalPOS.Entidades;

namespace TrabajoFinalPOS.Repositorios;

// Implementación genérica reutilizable para cualquier entidad (OCP: se extiende por herencia
// sin modificar esta clase, como hace RepositorioCliente).
public class RepositorioEnMemoria<T> : IRepositorio<T> where T : EntidadBase
{
    protected readonly List<T> Elementos = new();
    private int _siguienteId = 1;

    public virtual T Agregar(T entidad)
    {
        entidad.Id = _siguienteId++;
        Elementos.Add(entidad);
        return entidad;
    }

    public T? ObtenerPorId(int id) => Elementos.FirstOrDefault(e => e.Id == id);

    public IReadOnlyList<T> ObtenerTodos() => Elementos.AsReadOnly();

    public virtual bool Actualizar(T entidad)
    {
        var indice = Elementos.FindIndex(e => e.Id == entidad.Id);
        if (indice == -1) return false;
        Elementos[indice] = entidad;
        return true;
    }

    public virtual bool Eliminar(int id)
    {
        var entidad = ObtenerPorId(id);
        return entidad is not null && Elementos.Remove(entidad);
    }
}
