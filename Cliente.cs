

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Cliente;
using System.IO;

class clienteCS
{
    static  void crearTicket()
    {
        string destinoIn = "", nombreCompletoIn = "", procedenciaIn = "", sexoIn = "";
        int edad = 0, opcionHorarioIn = 0, opcionEjecucion = 1;
        string sInput;


        TcpClient clientSock;
        try
        {
            clientSock = new TcpClient("127.0.0.1", 5050);
        }
        catch
        {
            Console.WriteLine("Fallo en la conxecion con el servidor");
            return;
        }
        NetworkStream con = clientSock.GetStream();

        StreamWriter enviarDatos = new StreamWriter(con);
        StreamReader recibirDatos = new StreamReader(con);




        while (nombreCompletoIn.Equals(""))
        {
            Console.WriteLine("Introduce tu nombre completo");
            nombreCompletoIn = Console.ReadLine();
            nombreCompletoIn = nombreCompletoIn.ToUpper();
        }



        while (sexoIn.Equals(""))
        {
            Console.WriteLine("Sexo (M/F)");
            sexoIn = Console.ReadLine();
            sexoIn = sexoIn.ToUpper();

        }
        try
        {
            Console.WriteLine("Edad");
            edad = int.Parse(Console.ReadLine());

            if (edad < 18)
            {
                throw new MenorEdadException();
            }

            while (edad.Equals(""))
            {
                Console.WriteLine("edad");
                edad = int.Parse(Console.ReadLine());

            }
        }

        catch (System.FormatException)
        {
            Console.WriteLine("El dato ingresado es erróneo");
            Console.WriteLine("Introduce de nuevo la edad");
            edad = int.Parse(Console.ReadLine());


        }
        catch (MenorEdadException)
        {
            Console.WriteLine("Reservaciones para mayores de edad");
            Console.WriteLine("Presione cualquier tecla");
            Console.ReadKey();
            Environment.Exit(0);

        }



        while (destinoIn.Equals(""))
        {
            Console.WriteLine("Lugar de destino");
            destinoIn = Console.ReadLine();
            destinoIn = destinoIn.ToUpper();
            enviarDatos.WriteLine(destinoIn);
            enviarDatos.Flush();
            Console.WriteLine("Espere, por favor. Obteniendo horarios");


            sInput = recibirDatos.ReadLine();
            Console.WriteLine(sInput);
            opcionHorarioIn = int.Parse(Console.ReadLine());
            while (opcionHorarioIn < 1 ||opcionHorarioIn>5)
            {
                Console.WriteLine("Introduce una opcion");
                opcionHorarioIn = int.Parse(Console.ReadLine());

            }




            while (procedenciaIn.Equals(""))
            {
                Console.WriteLine("Lugar de procedencia");
                procedenciaIn = Console.ReadLine();
                procedenciaIn = procedenciaIn.ToUpper();

            }



            DateTime fechaNow = new DateTime();
            fechaNow = DateTime.Now;

            opcionEjecucion = 2;
            enviarDatos.WriteLine(opcionEjecucion);
            enviarDatos.WriteLine("Fecha de reservacion: " + fechaNow.ToString("d") + " Hora de vuelo: " + opcionHorarioIn + " Nombre: " + nombreCompletoIn + " Sexo: " + sexoIn + " Edad: " + edad + " Procedencia: " + procedenciaIn + " Destino: " + destinoIn);
            enviarDatos.Flush();

            opcionEjecucion = 3;
            enviarDatos.WriteLine(opcionEjecucion);
            Console.WriteLine("Escriba su correo");
            enviarDatos.WriteLine(Console.ReadLine());





            enviarDatos.Flush();


            sInput = recibirDatos.ReadLine();
            Console.WriteLine(sInput);
            con.Close();


            Console.ReadKey();

        }
    }
    static void Main(string[] args)
    {

        crearTicket();
    }
}
    