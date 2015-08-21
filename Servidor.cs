

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using GoEmail;


class servidorCS
{
    static void Main(string[] args)
    {

        String n="", horariosOut="";
        IPAddress ip = IPAddress.Parse("127.0.0.1");
        TcpListener tcpListener = new TcpListener(ip, 5050);
        tcpListener.Start();
        Socket serverSock = tcpListener.AcceptSocket();

        if (serverSock.Connected)
        {
            NetworkStream con = new NetworkStream(serverSock);
            //Archivo local en servidor, el cual se activará para mandar un correo
         StreamWriter crearArchivoLocal = new StreamWriter("C:/Users/Dell/Documents/datoAdjunto.txt");

         StreamWriter enviarDatos = new StreamWriter(con);
          StreamReader recibirDatos = new StreamReader(con);


            if(recibirDatos.ReadLine()!=""){
                horariosOut = "Horarios disponibles: 1) 9:00 am  2) 12:00 pm  3) 3:00pm  4) 5:00 pm  5) 12:00 am. Seleccione alguna opción indicando con un numero";
                   enviarDatos.WriteLine(horariosOut);
            enviarDatos.Flush();
                }

            if(recibirDatos.ReadLine().Equals("2")){
               
                       n = (recibirDatos.ReadLine() );
                    
                       
            crearArchivoLocal.WriteLine(n);
            
            }
       
            if(recibirDatos.ReadLine().Equals("3")){
                EnviarEmail enviarCorreo = new EnviarEmail();
                bool exito = enviarCorreo.EnviarMail(recibirDatos.ReadLine(), "Datos de reservación", n, "AerolinasMisael", "josue.rodriguez.galindo@gmail.com", "josueupbc");
                if (exito == true)
                {
                    enviarDatos.WriteLine(n + " El correo fue enviado exitosamente");
                    Console.WriteLine("El correo ha sido enviado exitosamente");
                }
                else
                    Console.Error.WriteLine("No fue posible enviar el correo");
                enviarDatos.Flush();

            }



      

            recibirDatos.Close();
            crearArchivoLocal.Close();
            enviarDatos.Close();
            con.Close();
           serverSock.Close();
        }
        else
            Console.WriteLine("Fallo en la conexion");

        

    

        Console.ReadKey();
    }



}
