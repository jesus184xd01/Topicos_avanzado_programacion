using System;
using System.Threading;

namespace hilos
{
    internal class Program
    {
        // Metodo que imprime un mensaje
        static void Trabajo()
        {
            // ejecuta un hilo/proceso para imprimir lo siguiente
            Console.WriteLine("Hilo en ejecución");
        } 

        // Metodo principal que se ejecuta al iniciar la app
        static void Main()
        {
            // Instancia de un objeto tipo hilo donde se le pasa como parametro el metodo Trabajo
            Thread hilo = new Thread(Trabajo);
            // Se inicia el hilo
            hilo.Start();
            // Imprime un mensaje de que el hilo ya esta ejecutandose
            Console.WriteLine("Hilo principal en ejecución");
        }
    }
}
