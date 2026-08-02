using TrabajoFinalPOS.Entidades;

namespace TrabajoFinalPOS.Servicios;

public interface IServicioCliente
{
    Cliente Registrar(Cliente cliente);
    void Desactivar(int clienteId);
    Cliente? ObtenerPorId(int id);
    IReadOnlyList<Cliente> Listar();

    Direccion AgregarDireccion(Direccion direccion);
    Telefono AgregarTelefono(Telefono telefono);
    Correo AgregarCorreo(Correo correo);

    IReadOnlyList<Direccion> ObtenerDirecciones(int clienteId);
    IReadOnlyList<Telefono> ObtenerTelefonos(int clienteId);
    IReadOnlyList<Correo> ObtenerCorreos(int clienteId);

    string ObtenerTelefonosFormateados(int clienteId);
    string ObtenerCorreosFormateados(int clienteId);
    string ObtenerDireccionPrincipalFormateada(int clienteId);
}
