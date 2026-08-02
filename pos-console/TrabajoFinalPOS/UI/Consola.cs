namespace TrabajoFinalPOS.UI;

// SRP: toda la interacción de bajo nivel con la consola (leer, validar, pausar) vive aquí,
// para que los menús no repitan lógica de parseo/validación de entrada.
public static class Consola
{
    public static void Limpiar()
    {
        try { Console.Clear(); }
        catch (IOException) { /* salida redirigida (no es una terminal interactiva) */ }
    }

    public static void Pausar()
    {
        Console.WriteLine();
        Console.Write("Presione ENTER para continuar...");
        Console.ReadLine();
    }

    public static string LeerTexto(string mensaje, bool obligatorio = true)
    {
        while (true)
        {
            Console.Write(mensaje);
            var texto = LeerLinea().Trim();
            if (!obligatorio || !string.IsNullOrWhiteSpace(texto))
                return texto;
            Console.WriteLine("Este campo es obligatorio.");
        }
    }

    public static string? LeerTextoOpcional(string mensaje)
    {
        Console.Write(mensaje);
        var texto = LeerLinea().Trim();
        return string.IsNullOrWhiteSpace(texto) ? null : texto;
    }

    public static int LeerEntero(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);
            if (int.TryParse(LeerLinea(), out var valor))
                return valor;
            Console.WriteLine("Ingrese un número entero válido.");
        }
    }

    public static decimal LeerDecimal(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);
            if (decimal.TryParse(LeerLinea(), out var valor))
                return valor;
            Console.WriteLine("Ingrese un número válido.");
        }
    }

    public static bool LeerSiNo(string mensaje)
    {
        while (true)
        {
            Console.Write($"{mensaje} (S/N): ");
            var respuesta = LeerLinea().Trim().ToUpperInvariant();
            if (respuesta == "S") return true;
            if (respuesta == "N") return false;
            Console.WriteLine("Responda con S o N.");
        }
    }

    // Punto único de lectura: si Console.ReadLine() devuelve null, la entrada estándar
    // llegó a su fin (EOF), así que se corta el programa en lugar de reintentar para siempre.
    private static string LeerLinea() => Console.ReadLine() ?? throw new EntradaFinalizadaException();
}
