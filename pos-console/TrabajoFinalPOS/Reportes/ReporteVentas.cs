namespace TrabajoFinalPOS.Reportes;

public record ReporteVentas(IReadOnlyList<FilaListadoVenta> Filas, decimal GranTotal);
