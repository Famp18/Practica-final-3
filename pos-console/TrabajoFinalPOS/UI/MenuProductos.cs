using TrabajoFinalPOS.Entidades;
using TrabajoFinalPOS.Servicios;

namespace TrabajoFinalPOS.UI;

public class MenuProductos : IMenu
{
    public string Titulo => "Gestionar Productos";

    private readonly GestorCatalogoConsola<Producto> _gestor;

    public MenuProductos(IServicioCatalogo<Producto> servicioProductos)
    {
        _gestor = new GestorCatalogoConsola<Producto>(
            "Producto", servicioProductos,
            () => new Producto
            {
                Nombre = Consola.LeerTexto("Nombre: "),
                Marca = Consola.LeerTexto("Marca: "),
                Tipo = Consola.LeerTexto("Tipo/Presentación: "),
                Precio = Consola.LeerDecimal("Precio: ")
            },
            p =>
            {
                p.Nombre = Consola.LeerTexto($"Nombre [{p.Nombre}]: ", obligatorio: false) is { Length: > 0 } n ? n : p.Nombre;
                p.Marca = Consola.LeerTexto($"Marca [{p.Marca}]: ", obligatorio: false) is { Length: > 0 } m ? m : p.Marca;
                p.Tipo = Consola.LeerTexto($"Tipo [{p.Tipo}]: ", obligatorio: false) is { Length: > 0 } t ? t : p.Tipo;
                p.Precio = Consola.LeerDecimal($"Precio [{p.Precio}]: ");
            });
    }

    public void Mostrar() => _gestor.EjecutarMenu();
}
