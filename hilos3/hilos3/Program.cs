using System;
using System.Threading;

namespace hilos3
{
    class Programa
    {
        static void Tarea()
        {
            Thread.Sleep(3000);
            Console.WriteLine("Tarea completada");
        }
        static void Main()
        {
            Thread hilo = new Thread(Tarea);
            hilo.Start();
            Console.WriteLine("El programa continúa ejecutándose");
        }
        static void countup()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
            }
        }
        static void countdown()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
            }
        }
    }
}