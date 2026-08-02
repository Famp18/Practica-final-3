using TrabajoFinalPOS.Entidades;

namespace TrabajoFinalPOS.Repositorios;

// Abstracción de persistencia (DIP): los servicios dependen de esta interfaz, nunca de la
// implementación concreta. Permite cambiar el almacenamiento (memoria, archivo, BD) sin tocar
// la capa de negocio.
public interface IRepositorio<T> where T : EntidadBase
{
    T Agregar(T entidad);
    T? ObtenerPorId(int id);
    IReadOnlyList<T> ObtenerTodos();
    bool Actualizar(T entidad);
    bool Eliminar(int id);
}
