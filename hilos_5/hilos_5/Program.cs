using System;
using System.Threading.Tasks;

namespace hilos_5
{
    class Programa
    {
        // Metodo tipo task que imprime un mensaje con una funcion flecha y que espera terminar su proceso antes de seguir con las sig. lineas
        static async Task Main()
        {
            Task tarea = Task.Run(() =>
            {
                Console.WriteLine("Tarea en ejecución");
            });

            await tarea; // pone en espera la ejecucion de las lineas de codigo hasta completar la tarea
            Console.WriteLine("Tarea finalizada");
        }
    }

}