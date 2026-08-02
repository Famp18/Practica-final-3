namespace TrabajoFinalPOS.UI;

// Abstracción de la que dependerá MenuPrincipal: recorre una lista de IMenu y llama
// Mostrar() sin conocer la clase concreta de cada uno (polimorfismo).
public interface IMenu
{
    string Titulo { get; }
    void Mostrar();
}
