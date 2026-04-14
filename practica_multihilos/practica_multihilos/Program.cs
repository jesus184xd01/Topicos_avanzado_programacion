using System;
using System.Threading;

namespace practica_multihilos
{
    internal class Program
    {
        static double saldo = 1000;
        static object bloqueo = new object();

        static void Operaciones(object param)
        {
            string[] acciones = (string[])param;
            string nombreCliente = Thread.CurrentThread.Name;
            int contadorDormido = 0;

            Console.WriteLine($"  [{nombreCliente}] ● Iniciado");

            foreach (string accion in acciones)
            {
                string[] partes = accion.Split(':');
                string operacion = partes[0];
                double cantidad = partes.Length > 1 ? double.Parse(partes[1]) : 0;

                if (contadorDormido < 2)
                {
                    Console.WriteLine($"  [{nombreCliente}] ~ Dormido");
                    contadorDormido++;
                }
                Thread.Sleep(500);

                bool lockLibre = Monitor.TryEnter(bloqueo, 0);
                if (!lockLibre)
                    Console.WriteLine($"  [{nombreCliente}] ■ Esperando turno");
                else
                    Monitor.Exit(bloqueo);

                lock (bloqueo)
                {
                    switch (operacion)
                    {
                        case "consultar":
                            Console.WriteLine($"  [{nombreCliente}] ▶ Ejecutando — Consultando saldo...");
                            Thread.Sleep(1000);
                            Console.WriteLine($"  [{nombreCliente}]   Saldo actual: ${saldo}");
                            break;

                        case "depositar":
                            Console.WriteLine($"  [{nombreCliente}] ▶ Ejecutando — Depositando ${cantidad}...");
                            Thread.Sleep(1000);
                            saldo += cantidad;
                            Console.WriteLine($"  [{nombreCliente}]   Deposito realizado. Nuevo saldo: ${saldo}");
                            break;

                        case "retirar":
                            Console.WriteLine($"  [{nombreCliente}] ▶ Ejecutando — Retirando ${cantidad}...");
                            Thread.Sleep(1000);
                            if (cantidad <= saldo)
                            {
                                saldo -= cantidad;
                                Console.WriteLine($"  [{nombreCliente}]   Retiro exitoso. Nuevo saldo: ${saldo}");
                            }
                            else
                            {
                                Console.WriteLine($"  [{nombreCliente}]   Fondos insuficientes. Saldo: ${saldo}");
                            }
                            break;
                    }
                }
            }

            Console.WriteLine($"  [{nombreCliente}] ✓ Terminado");
        }

        static void Main(string[] args)
        {
            string[] operacionesCliente1 = {
                "consultar",
                "depositar:300",
                "consultar",
                "retirar:500",
                "consultar"
            };

            string[] operacionesCliente2 = {
                "depositar:200",
                "consultar",
                "retirar:100",
                "depositar:50",
                "consultar"
            };

            Thread cliente1 = new Thread(Operaciones) { Name = "Cliente1" };
            Thread cliente2 = new Thread(Operaciones) { Name = "Cliente2" };

            Console.WriteLine("         CAJERO AUTOMATICO            ");
            Console.WriteLine();
            Console.WriteLine($"  Saldo inicial : ${saldo}");
            Console.WriteLine($"  Cliente1      : {cliente1.ThreadState}");
            Console.WriteLine($"  Cliente2      : {cliente2.ThreadState}");
            Console.WriteLine();

            cliente1.Start(operacionesCliente1);
            cliente2.Start(operacionesCliente2);

            cliente1.Join();
            cliente2.Join();

            Console.WriteLine();
            Console.WriteLine($"  Cliente1      : {cliente1.ThreadState}");
            Console.WriteLine($"  Cliente2      : {cliente2.ThreadState}");
            Console.WriteLine($"  Saldo final   : ${saldo}");
            Console.WriteLine();
            Console.WriteLine("        PROGRAMA TERMINADO            ");
        }
    }
}
