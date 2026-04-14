using System;
using System.Threading;

namespace hilos2
{
    class Programa
    {
        // Metodo principal que se ejecuta automaticamente al debugear el programa
        static void Main()
        {
            // Imprime un mensaje
            Console.WriteLine("Inicio");
            Thread.Sleep(3000); // Simula tarea larga
            // Despues de 3 segundos ejecuta el mensaje final
            Console.WriteLine("Fin");
        }
    }
}
