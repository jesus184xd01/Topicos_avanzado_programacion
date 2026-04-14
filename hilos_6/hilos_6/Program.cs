using System;

namespace hilos_6
{
    class Programa
    {
    static int contador = 0;
    static object bloqueo = new object ();
 
        // metodo que incrementa el contador con un ciclo y que hace un lock en un objeto para sincronizar los hilos
    static void Incrementar()
        {
            for (int i = 0; i < 100000; i++)
            {
                lock (bloqueo)
                {
                    contador++;
                }
            }
        }
        // Metodo principal que llama la funcion de incrementar e imprime el contador
    static void Main()
        {
            Incrementar();
            Console.WriteLine(contador);
        }
    }
}