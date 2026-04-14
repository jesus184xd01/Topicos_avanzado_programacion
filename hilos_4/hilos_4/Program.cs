using System;
using System.Threading;

namespace hilos_4
{
    class Programa
    {
        // Iteracciones que va realizar el hilo de manera iterativa con un ciclo for
        static void Trabajo()
        {
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine($"Iteración {i}");
                Thread.Sleep(1000);
            }
        }
        //  Metodo principal que ejecuta un hilo y define que cuando termine su ejecucion imprima un mensaje indicandolo
        static void Main()
        {
        Thread hilo = new Thread(Trabajo);
        hilo.Start();
        
        hilo.Join(); // Espera a que termine
        Console.WriteLine("Hilo finalizado");
        }
        }
}