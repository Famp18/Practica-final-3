using TrabajoFinalPOS.Entidades;
using TrabajoFinalPOS.Repositorios;

namespace TrabajoFinalPOS.Servicios;

// Concentra las reglas de negocio propias de Cliente y sus datos asociados
// (direcciones, teléfonos, correos). Depende únicamente de abstracciones (DIP).
public class ServicioCliente : IServicioCliente
{
    private readonly IRepositorioCliente _clientes;
    private readonly IRepositorio<Direccion> _direcciones;
    private readonly IRepositorio<Telefono> _telefonos;
    private readonly IRepositorio<Correo> _correos;
    private readonly IRepositorio<Provincia> _provincias;
    private readonly IRepositorio<Municipio> _municipios;
    private readonly IRepositorio<Seccion> _secciones;
    private readonly IRepositorio<Sector> _sectores;
    private readonly IRepositorio<TipoTelefono> _tiposTelefono;
    private readonly IRepositorio<TipoCorreo> _tiposCorreo;

    public ServicioCliente(
        IRepositorioCliente clientes,
        IRepositorio<Direccion> direcciones,
        IRepositorio<Telefono> telefonos,
        IRepositorio<Correo> correos,
        IRepositorio<Provincia> provincias,
        IRepositorio<Municipio> municipios,
        IRepositorio<Seccion> secciones,
        IRepositorio<Sector> sectores,
        IRepositorio<TipoTelefono> tiposTelefono,
        IRepositorio<TipoCorreo> tiposCorreo)
    {
        _clientes = clientes;
        _direcciones = direcciones;
        _telefonos = telefonos;
        _correos = correos;
        _provincias = provincias;
        _municipios = municipios;
        _secciones = secciones;
        _sectores = sectores;
        _tiposTelefono = tiposTelefono;
        _tiposCorreo = tiposCorreo;
    }

    public Cliente Registrar(Cliente cliente)
    {
        if (!cliente.EsValida(out var error))
            throw new InvalidOperationException(error);
        return _clientes.Agregar(cliente);
    }

    public void Desactivar(int clienteId)
    {
        var cliente = ObtenerClienteOLanzar(clienteId);
        cliente.Activo = false;
        _clientes.Actualizar(cliente);
    }

    public Cliente? ObtenerPorId(int id) => _clientes.ObtenerPorId(id);

    public IReadOnlyList<Cliente> Listar() => _clientes.ObtenerTodos();

    public Direccion AgregarDireccion(Direccion direccion)
    {
        ObtenerClienteOLanzar(direccion.ClienteId);
        if (!direccion.EsValida(out var error))
            throw new InvalidOperationException(error);
        if (_provincias.ObtenerPorId(direccion.ProvinciaId) is null)
            throw new InvalidOperationException("La provincia indicada no existe.");
        if (_municipios.ObtenerPorId(direccion.MunicipioId) is null)
            throw new InvalidOperationException("El municipio indicado no existe.");
        if (_secciones.ObtenerPorId(direccion.SeccionId) is null)
            throw new InvalidOperationException("La sección indicada no existe.");
        if (_sectores.ObtenerPorId(direccion.SectorId) is null)
            throw new InvalidOperationException("El sector indicado no existe.");
        return _direcciones.Agregar(direccion);
    }

    public Telefono AgregarTelefono(Telefono telefono)
    {
        ObtenerClienteOLanzar(telefono.ClienteId);
        if (!telefono.EsValida(out var error))
            throw new InvalidOperationException(error);
        if (_tiposTelefono.ObtenerPorId(telefono.TipoTelefonoId) is null)
            throw new InvalidOperationException("El tipo de teléfono indicado no existe.");
        return _telefonos.Agregar(telefono);
    }

    public Correo AgregarCorreo(Correo correo)
    {
        ObtenerClienteOLanzar(correo.ClienteId);
        if (!correo.EsValida(out var error))
            throw new InvalidOperationException(error);
        if (_tiposCorreo.ObtenerPorId(correo.TipoCorreoId) is null)
            throw new InvalidOperationException("El tipo de correo indicado no existe.");
        return _correos.Agregar(correo);
    }

    public IReadOnlyList<Direccion> ObtenerDirecciones(int clienteId) =>
        _direcciones.ObtenerTodos().Where(d => d.ClienteId == clienteId).ToList();

    public IReadOnlyList<Telefono> ObtenerTelefonos(int clienteId) =>
        _telefonos.ObtenerTodos().Where(t => t.ClienteId == clienteId).ToList();

    public IReadOnlyList<Correo> ObtenerCorreos(int clienteId) =>
        _correos.ObtenerTodos().Where(c => c.ClienteId == clienteId).ToList();

    public string ObtenerTelefonosFormateados(int clienteId) =>
        string.Join(", ", ObtenerTelefonos(clienteId).Select(t =>
        {
            var tipo = _tiposTelefono.ObtenerPorId(t.TipoTelefonoId);
            return tipo is null ? t.Numero : $"({tipo.Descripcion}){t.Numero}";
        }));

    public string ObtenerCorreosFormateados(int clienteId) =>
        string.Join(", ", ObtenerCorreos(clienteId).Select(c => c.Direccion));

    public string ObtenerDireccionPrincipalFormateada(int clienteId)
    {
        var direccion = ObtenerDirecciones(clienteId).FirstOrDefault();
        if (direccion is null) return string.Empty;

        var seccion = _secciones.ObtenerPorId(direccion.SeccionId);
        var municipio = _municipios.ObtenerPorId(direccion.MunicipioId);

        var partes = new[]
        {
            $"C/{direccion.Calle}",
            direccion.No is null ? null : $"#{direccion.No}",
            direccion.Residencial,
            seccion?.Descripcion,
            municipio?.Descripcion
        }.Where(p => !string.IsNullOrWhiteSpace(p));

        return string.Join(", ", partes);
    }

    private Cliente ObtenerClienteOLanzar(int clienteId) =>
        _clientes.ObtenerPorId(clienteId) ?? throw new InvalidOperationException("El cliente indicado no existe.");
}
