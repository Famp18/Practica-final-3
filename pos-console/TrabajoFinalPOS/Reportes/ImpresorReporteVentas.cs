namespace TrabajoFinalPOS.Reportes;

// SRP: esta clase solo sabe imprimir un reporte ya calculado; no conoce clientes, ventas ni repositorios.
public static class ImpresorReporteVentas
{
    public static void Imprimir(ReporteVentas reporte)
    {
        if (reporte.Filas.Count == 0)
        {
            Console.WriteLine("No hay ventas registradas para el criterio indicado.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("=========================== LISTADO DIARIO DE VENTAS - ABC COMPANY ===========================");
        Console.WriteLine($"{"Cliente",-22} {"Producto",-18} {"Marca",-12} {"Tipo",-10} {"Cant.",5} {"Precio",10} {"Monto",10} {"Total",10} {"Pago",-18}");
        Console.WriteLine(new string('-', 130));

        foreach (var fila in reporte.Filas)
        {
            Console.WriteLine(
                $"{Truncar(fila.NombreCliente, 22),-22} {Truncar(fila.Producto, 18),-18} {Truncar(fila.Marca, 12),-12} " +
                $"{Truncar(fila.Tipo, 10),-10} {fila.Cantidad,5} {fila.Precio,10:N2} {fila.Monto,10:N2} {fila.Total,10:N2} " +
                $"{Truncar(fila.Pago, 18),-18}");
        }

        Console.WriteLine(new string('-', 130));
        Console.WriteLine($"Gran total del listado: RD${reporte.GranTotal:N2}");
        Console.WriteLine();
    }

    private static string Truncar(string texto, int longitud) =>
        texto.Length <= longitud ? texto : texto[..(longitud - 1)] + "…";
}
