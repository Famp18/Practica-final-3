using TrabajoFinalPOS.Entidades;

namespace TrabajoFinalPOS.Servicios;

public interface IServicioCatalogo<T> where T : EntidadBase
{
    T Crear(T entidad);
    void Actualizar(T entidad);
    void Eliminar(int id);
    T? ObtenerPorId(int id);
    IReadOnlyList<T> Listar();
}
