using TrabajoFinalPOS.Entidades;

namespace TrabajoFinalPOS.Repositorios;

// Herencia + polimorfismo: extiende el repositorio genérico y sobrescribe Agregar
// para aplicar una regla propia de Cliente (identificación única).
public class RepositorioCliente : RepositorioEnMemoria<Cliente>, IRepositorioCliente
{
    public Cliente? ObtenerPorIdentificacion(string identificacion) =>
        Elementos.FirstOrDefault(c => c.Identificacion == identificacion);

    public override Cliente Agregar(Cliente entidad)
    {
        if (ObtenerPorIdentificacion(entidad.Identificacion) is not null)
            throw new InvalidOperationException(
                $"Ya existe un cliente con la identificación {entidad.Identificacion}.");
        return base.Agregar(entidad);
    }
}
