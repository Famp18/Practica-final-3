namespace TrabajoFinalPOS.UI;

// Solo conoce la abstracción IMenu: puede mostrar cualquier cantidad de submenús sin
// saber nada de Clientes, Ventas, Ubicaciones, etc. (polimorfismo + bajo acoplamiento).
public class MenuPrincipal
{
    private readonly List<IMenu> _submenus;

    public MenuPrincipal(IEnumerable<IMenu> submenus)
    {
        _submenus = submenus.ToList();
    }

    public void Ejecutar()
    {
        var salir = false;
        while (!salir)
        {
            Consola.Limpiar();
            Console.WriteLine("======================================================");
            Console.WriteLine("   SISTEMA DE VENTAS - ABC COMPANY");
            Console.WriteLine("   Trabajo Final - Aplicación de Consola en C#");
            Console.WriteLine("======================================================");
            for (var i = 0; i < _submenus.Count; i++)
                Console.WriteLine($" {i + 1}. {_submenus[i].Titulo}");
            Console.WriteLine(" 0. Salir");
            Console.WriteLine("======================================================");

            var opcion = Consola.LeerEntero("Seleccione una opción: ");

            if (opcion == 0)
            {
                salir = true;
            }
            else if (opcion > 0 && opcion <= _submenus.Count)
            {
                _submenus[opcion - 1].Mostrar();
            }
            else
            {
                Console.WriteLine("Opción inválida.");
                Consola.Pausar();
            }
        }

        Console.WriteLine("¡Hasta luego!");
    }
}
