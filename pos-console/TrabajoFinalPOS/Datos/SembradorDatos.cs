using TrabajoFinalPOS.Entidades;
using TrabajoFinalPOS.Servicios;

namespace TrabajoFinalPOS.Datos;

// Reproduce, a través de los propios servicios (no accediendo a los repositorios directamente),
// los datos de ejemplo del archivo "Tabla BD.xlsx" para que la aplicación arranque ya funcional.
public static class SembradorDatos
{
    public static void Sembrar(
        IServicioCatalogo<Provincia> servicioProvincias,
        IServicioCatalogo<Municipio> servicioMunicipios,
        IServicioCatalogo<Seccion> servicioSecciones,
        IServicioCatalogo<Sector> servicioSectores,
        IServicioCatalogo<TipoTelefono> servicioTiposTelefono,
        IServicioCatalogo<TipoCorreo> servicioTiposCorreo,
        IServicioCatalogo<Producto> servicioProductos,
        IServicioCliente servicioClientes,
        IServicioVenta servicioVentas)
    {
        var santoDomingo = servicioProvincias.Crear(new Provincia { Descripcion = "Santo Domingo" });
        var distritoNacional = servicioProvincias.Crear(new Provincia { Descripcion = "Distrito Nacional" });

        var sdNorte = servicioMunicipios.Crear(new Municipio { Descripcion = "Santo Domingo Norte", ProvinciaId = santoDomingo.Id });
        var dn = servicioMunicipios.Crear(new Municipio { Descripcion = "Distrito Nacional", ProvinciaId = distritoNacional.Id });

        var seccionUrbanaSdNorte = servicioSecciones.Crear(new Seccion { Descripcion = "Urbana", MunicipioId = sdNorte.Id });
        var seccionUrbanaDn = servicioSecciones.Crear(new Seccion { Descripcion = "Urbana", MunicipioId = dn.Id });

        var villaMella = servicioSectores.Crear(new Sector { Descripcion = "Villa Mella", MunicipioId = sdNorte.Id, SeccionId = seccionUrbanaSdNorte.Id });
        var losAlamos = servicioSectores.Crear(new Sector { Descripcion = "Los Alamos", MunicipioId = sdNorte.Id, SeccionId = seccionUrbanaSdNorte.Id });
        var ensLuperon = servicioSectores.Crear(new Sector { Descripcion = "Ensanche Luperón", MunicipioId = dn.Id, SeccionId = seccionUrbanaDn.Id });

        var tipoCasa = servicioTiposTelefono.Crear(new TipoTelefono { Descripcion = "Casa" });
        var tipoMovil = servicioTiposTelefono.Crear(new TipoTelefono { Descripcion = "Movil" });
        var tipoFlota = servicioTiposTelefono.Crear(new TipoTelefono { Descripcion = "Flota" });

        var tipoPersonal = servicioTiposCorreo.Crear(new TipoCorreo { Descripcion = "Personal" });
        var tipoTrabajo = servicioTiposCorreo.Crear(new TipoCorreo { Descripcion = "Trabajo" });

        var jabon = servicioProductos.Crear(new Producto { Nombre = "Jabon de Bañarse", Marca = "Dove", Tipo = "Pasta", Precio = 105 });
        var aceite = servicioProductos.Crear(new Producto { Nombre = "Aceite de Soya", Marca = "Crisol", Tipo = "Galon", Precio = 405 });
        var lechuga = servicioProductos.Crear(new Producto { Nombre = "Lechuga", Marca = "Generica", Tipo = "Paquete", Precio = 45 });
        var tuna = servicioProductos.Crear(new Producto { Nombre = "Tuna", Marca = "Paco Fish", Tipo = "Lata 8oz", Precio = 55 });
        var desodorante = servicioProductos.Crear(new Producto { Nombre = "Desodorante", Marca = "Rexona", Tipo = "Esprey", Precio = 175 });
        var afeitadora = servicioProductos.Crear(new Producto { Nombre = "Afeitadora", Marca = "Big", Tipo = "Paquete 12", Precio = 210 });
        var papelBano = servicioProductos.Crear(new Producto { Nombre = "Papel de Baño", Marca = "Domino", Tipo = "Paquete de 4", Precio = 200 });
        var espaguetti = servicioProductos.Crear(new Producto { Nombre = "Espaguetti", Marca = "Princesa", Tipo = "Unidad", Precio = 30 });
        var panAgua = servicioProductos.Crear(new Producto { Nombre = "Pan de Agua", Marca = "Generico", Tipo = "Paquete", Precio = 55 });
        var sopita = servicioProductos.Crear(new Producto { Nombre = "Sopita", Marca = "Doña Gallina", Tipo = "Unidad", Precio = 10 });

        var angel = servicioClientes.Registrar(new Cliente { Nombres = "Angel", Apellidos = "Martinez", Identificacion = "00112223334" });
        servicioClientes.AgregarDireccion(new Direccion
        {
            ClienteId = angel.Id,
            Calle = "Obispo",
            No = "25",
            Residencial = "Villa Mella",
            ProvinciaId = santoDomingo.Id,
            MunicipioId = sdNorte.Id,
            SeccionId = seccionUrbanaSdNorte.Id,
            SectorId = villaMella.Id
        });
        servicioClientes.AgregarTelefono(new Telefono { ClienteId = angel.Id, TipoTelefonoId = tipoCasa.Id, Numero = "8094568978" });
        servicioClientes.AgregarTelefono(new Telefono { ClienteId = angel.Id, TipoTelefonoId = tipoFlota.Id, Numero = "8492500000" });
        servicioClientes.AgregarCorreo(new Correo { ClienteId = angel.Id, TipoCorreoId = tipoPersonal.Id, Direccion = "Angelp@hotmail.com" });

        var marcial = servicioClientes.Registrar(new Cliente { Nombres = "Marcial", Apellidos = "Ascensio", Identificacion = "00223334445" });
        servicioClientes.AgregarDireccion(new Direccion
        {
            ClienteId = marcial.Id,
            Calle = "Juana Mendez",
            No = "25",
            Apto = "203",
            Residencial = "Edificio Don Onorio I, Ens. Luperón",
            ProvinciaId = distritoNacional.Id,
            MunicipioId = dn.Id,
            SeccionId = seccionUrbanaDn.Id,
            SectorId = ensLuperon.Id
        });
        servicioClientes.AgregarTelefono(new Telefono { ClienteId = marcial.Id, TipoTelefonoId = tipoCasa.Id, Numero = "8095342055" });
        servicioClientes.AgregarTelefono(new Telefono { ClienteId = marcial.Id, TipoTelefonoId = tipoMovil.Id, Numero = "8295640202" });
        servicioClientes.AgregarCorreo(new Correo { ClienteId = marcial.Id, TipoCorreoId = tipoPersonal.Id, Direccion = "marcialAsc25@claro.net" });
        servicioClientes.AgregarCorreo(new Correo { ClienteId = marcial.Id, TipoCorreoId = tipoTrabajo.Id, Direccion = "mascensio@mitrabajo.com" });

        var maria = servicioClientes.Registrar(new Cliente { Nombres = "Maria De los Angeles", Apellidos = "Reynoso", Identificacion = "00334445556" });
        servicioClientes.AgregarDireccion(new Direccion
        {
            ClienteId = maria.Id,
            Calle = "El Peñón",
            No = "45",
            Residencial = "Los Alamos",
            ProvinciaId = santoDomingo.Id,
            MunicipioId = sdNorte.Id,
            SeccionId = seccionUrbanaSdNorte.Id,
            SectorId = losAlamos.Id,
            Referencia = "Manz 12, Peaton 2"
        });
        servicioClientes.AgregarTelefono(new Telefono { ClienteId = maria.Id, TipoTelefonoId = tipoCasa.Id, Numero = "8095634545" });

        servicioVentas.Registrar(angel.Id, FormaPago.Efectivo, new (int, int)[]
        {
            (jabon.Id, 2), (aceite.Id, 1), (lechuga.Id, 1), (tuna.Id, 5)
        });

        servicioVentas.Registrar(marcial.Id, FormaPago.TarjetaCredito, new (int, int)[]
        {
            (desodorante.Id, 2), (afeitadora.Id, 1)
        });

        servicioVentas.Registrar(maria.Id, FormaPago.BonoNavideno, new (int, int)[]
        {
            (papelBano.Id, 1), (espaguetti.Id, 5), (panAgua.Id, 1), (sopita.Id, 12)
        });
    }
}
