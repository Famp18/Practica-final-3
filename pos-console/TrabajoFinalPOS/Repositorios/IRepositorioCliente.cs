using TrabajoFinalPOS.Entidades;

namespace TrabajoFinalPOS.Repositorios;

// Interfaz segregada (ISP): agrega solo el método extra que necesita Cliente,
// sin forzarlo sobre el resto de los repositorios genéricos.
public interface IRepositorioCliente : IRepositorio<Cliente>
{
    Cliente? ObtenerPorIdentificacion(string identificacion);
}
