using TrabajoFinalPOS.Entidades;
using TrabajoFinalPOS.Repositorios;

namespace TrabajoFinalPOS.Servicios;

// Servicio genérico reutilizado por Provincia, Municipio, Seccion, Sector, TipoTelefono,
// TipoCorreo y Producto. La validación adicional (ej. verificar una llave foránea) se inyecta
// como delegado en el constructor: así se puede EXTENDER el comportamiento sin MODIFICAR esta
// clase (Open/Closed). Depende de IRepositorio<T>, no de una implementación concreta (DIP).
public class ServicioCatalogo<T> : IServicioCatalogo<T> where T : EntidadBase
{
    private readonly IRepositorio<T> _repositorio;
    private readonly Func<T, string?>? _validacionAdicional;

    public ServicioCatalogo(IRepositorio<T> repositorio, Func<T, string?>? validacionAdicional = null)
    {
        _repositorio = repositorio;
        _validacionAdicional = validacionAdicional;
    }

    public T Crear(T entidad)
    {
        Validar(entidad);
        return _repositorio.Agregar(entidad);
    }

    public void Actualizar(T entidad)
    {
        Validar(entidad);
        if (!_repositorio.Actualizar(entidad))
            throw new InvalidOperationException($"No existe el registro con Id {entidad.Id}.");
    }

    public void Eliminar(int id)
    {
        if (!_repositorio.Eliminar(id))
            throw new InvalidOperationException($"No existe el registro con Id {id}.");
    }

    public T? ObtenerPorId(int id) => _repositorio.ObtenerPorId(id);

    public IReadOnlyList<T> Listar() => _repositorio.ObtenerTodos();

    private void Validar(T entidad)
    {
        if (!entidad.EsValida(out var error))
            throw new InvalidOperationException(error);

        var errorAdicional = _validacionAdicional?.Invoke(entidad);
        if (!string.IsNullOrEmpty(errorAdicional))
            throw new InvalidOperationException(errorAdicional);
    }
}
