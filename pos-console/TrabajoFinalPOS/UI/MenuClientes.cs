using TrabajoFinalPOS.Entidades;
using TrabajoFinalPOS.Servicios;

namespace TrabajoFinalPOS.UI;

public class MenuClientes : IMenu
{
    public string Titulo => "Gestionar Clientes";

    private readonly IServicioCliente _servicioCliente;
    private readonly IServicioCatalogo<Provincia> _servicioProvincias;
    private readonly IServicioCatalogo<Municipio> _servicioMunicipios;
    private readonly IServicioCatalogo<Seccion> _servicioSecciones;
    private readonly IServicioCatalogo<Sector> _servicioSectores;
    private readonly IServicioCatalogo<TipoTelefono> _servicioTiposTelefono;
    private readonly IServicioCatalogo<TipoCorreo> _servicioTiposCorreo;

    public MenuClientes(
        IServicioCliente servicioCliente,
        IServicioCatalogo<Provincia> servicioProvincias,
        IServicioCatalogo<Municipio> servicioMunicipios,
        IServicioCatalogo<Seccion> servicioSecciones,
        IServicioCatalogo<Sector> servicioSectores,
        IServicioCatalogo<TipoTelefono> servicioTiposTelefono,
        IServicioCatalogo<TipoCorreo> servicioTiposCorreo)
    {
        _servicioCliente = servicioCliente;
        _servicioProvincias = servicioProvincias;
        _servicioMunicipios = servicioMunicipios;
        _servicioSecciones = servicioSecciones;
        _servicioSectores = servicioSectores;
        _servicioTiposTelefono = servicioTiposTelefono;
        _servicioTiposCorreo = servicioTiposCorreo;
    }

    public void Mostrar()
    {
        var salir = false;
        while (!salir)
        {
            Consola.Limpiar();
            Console.WriteLine("=== Clientes ===");
            Console.WriteLine("1. Listar clientes");
            Console.WriteLine("2. Registrar cliente nuevo");
            Console.WriteLine("3. Ver detalle de un cliente");
            Console.WriteLine("4. Agregar dirección a un cliente");
            Console.WriteLine("5. Agregar teléfono a un cliente");
            Console.WriteLine("6. Agregar correo a un cliente");
            Console.WriteLine("7. Desactivar cliente");
            Console.WriteLine("0. Volver");
            var opcion = Consola.LeerEntero("Seleccione una opción: ");
            switch (opcion)
            {
                case 1: Listar(); break;
                case 2: Registrar(); break;
                case 3: VerDetalle(); break;
                case 4: AgregarDireccion(); break;
                case 5: AgregarTelefono(); break;
                case 6: AgregarCorreo(); break;
                case 7: Desactivar(); break;
                case 0: salir = true; break;
                default:
                    Console.WriteLine("Opción inválida.");
                    Consola.Pausar();
                    break;
            }
        }
    }

    private void Listar()
    {
        Consola.Limpiar();
        Console.WriteLine("--- Clientes ---");
        var clientes = _servicioCliente.Listar();
        if (clientes.Count == 0) Console.WriteLine("No hay clientes registrados.");
        else foreach (var cliente in clientes) Console.WriteLine(cliente);
        Consola.Pausar();
    }

    private void Registrar()
    {
        Consola.Limpiar();
        Console.WriteLine("--- Registrar cliente ---");
        try
        {
            var cliente = new Cliente
            {
                Nombres = Consola.LeerTexto("Nombres: "),
                Apellidos = Consola.LeerTexto("Apellidos: "),
                Identificacion = Consola.LeerTexto("Identificación: "),
                Activo = true
            };
            _servicioCliente.Registrar(cliente);
            Console.WriteLine($"Cliente registrado con Id {cliente.Id}.");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        Consola.Pausar();
    }

    private void VerDetalle()
    {
        Consola.Limpiar();
        Console.WriteLine("--- Detalle de cliente ---");
        var id = Consola.LeerEntero("Id del cliente: ");
        var cliente = _servicioCliente.ObtenerPorId(id);
        if (cliente is null)
        {
            Console.WriteLine("No existe un cliente con ese Id.");
            Consola.Pausar();
            return;
        }
        Console.WriteLine(cliente);
        Console.WriteLine();
        Console.WriteLine("Direcciones:");
        foreach (var d in _servicioCliente.ObtenerDirecciones(id))
            Console.WriteLine($"  - {d.Calle} #{d.No}, {d.Residencial}");
        Console.WriteLine("Teléfonos: " + _servicioCliente.ObtenerTelefonosFormateados(id));
        Console.WriteLine("Correos: " + _servicioCliente.ObtenerCorreosFormateados(id));
        Consola.Pausar();
    }

    private void AgregarDireccion()
    {
        Consola.Limpiar();
        Console.WriteLine("--- Agregar dirección ---");
        try
        {
            var clienteId = Consola.LeerEntero("Id del cliente: ");
            MostrarCatalogoBreve("Provincias", _servicioProvincias.Listar());
            var provinciaId = Consola.LeerEntero("Id de la provincia: ");
            MostrarCatalogoBreve("Municipios", _servicioMunicipios.Listar());
            var municipioId = Consola.LeerEntero("Id del municipio: ");
            MostrarCatalogoBreve("Secciones", _servicioSecciones.Listar());
            var seccionId = Consola.LeerEntero("Id de la sección: ");
            MostrarCatalogoBreve("Sectores", _servicioSectores.Listar());
            var sectorId = Consola.LeerEntero("Id del sector: ");

            var direccion = new Direccion
            {
                ClienteId = clienteId,
                Calle = Consola.LeerTexto("Calle: "),
                No = Consola.LeerTextoOpcional("Número: "),
                Apto = Consola.LeerTextoOpcional("Apartamento: "),
                Residencial = Consola.LeerTextoOpcional("Residencial/Barrio: "),
                ProvinciaId = provinciaId,
                MunicipioId = municipioId,
                SeccionId = seccionId,
                SectorId = sectorId,
                Referencia = Consola.LeerTextoOpcional("Referencia: ")
            };
            _servicioCliente.AgregarDireccion(direccion);
            Console.WriteLine("Dirección agregada.");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        Consola.Pausar();
    }

    private void AgregarTelefono()
    {
        Consola.Limpiar();
        Console.WriteLine("--- Agregar teléfono ---");
        try
        {
            var clienteId = Consola.LeerEntero("Id del cliente: ");
            MostrarCatalogoBreve("Tipos de teléfono", _servicioTiposTelefono.Listar());
            var tipoId = Consola.LeerEntero("Id del tipo de teléfono: ");
            var numero = Consola.LeerTexto("Número: ");
            _servicioCliente.AgregarTelefono(new Telefono { ClienteId = clienteId, TipoTelefonoId = tipoId, Numero = numero });
            Console.WriteLine("Teléfono agregado.");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        Consola.Pausar();
    }

    private void AgregarCorreo()
    {
        Consola.Limpiar();
        Console.WriteLine("--- Agregar correo ---");
        try
        {
            var clienteId = Consola.LeerEntero("Id del cliente: ");
            MostrarCatalogoBreve("Tipos de correo", _servicioTiposCorreo.Listar());
            var tipoId = Consola.LeerEntero("Id del tipo de correo: ");
            var direccion = Consola.LeerTexto("Correo: ");
            _servicioCliente.AgregarCorreo(new Correo { ClienteId = clienteId, TipoCorreoId = tipoId, Direccion = direccion });
            Console.WriteLine("Correo agregado.");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        Consola.Pausar();
    }

    private void Desactivar()
    {
        Consola.Limpiar();
        Console.WriteLine("--- Desactivar cliente ---");
        try
        {
            var clienteId = Consola.LeerEntero("Id del cliente: ");
            _servicioCliente.Desactivar(clienteId);
            Console.WriteLine("Cliente desactivado.");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        Consola.Pausar();
    }

    private static void MostrarCatalogoBreve<T>(string titulo, IReadOnlyList<T> elementos) where T : EntidadBase
    {
        Console.WriteLine($"{titulo} disponibles:");
        foreach (var elemento in elementos)
            Console.WriteLine($"  {elemento}");
    }
}
