using System;
using System.Text;
using System.Net.Sockets;
using System.IO;

namespace TcpServer
{                                                      /// client sided app
    class Program                                          
    {
        static void Main(string[] args)
        {
        connection:
            try
            {
                TcpClient client = new TcpClient("127.0.0.1", 1302);
                string MessageToSend = "Hello";

                int byteCode = Encoding.ASCII.GetByteCount(MessageToSend + 1);
                byte[] sendData = new byte[byteCode];
                sendData = Encoding.ASCII.GetBytes(MessageToSend);

                NetworkStream stream = client.GetStream();
                stream.Write(sendData, 0, sendData.Length);
                Console.WriteLine("Sending data to server... ");

                StreamReader sr = new StreamReader(stream);
                string response = sr.ReadLine();
                Console.WriteLine(response);

                stream.Close();
                client.Close();
                Console.ReadKey();

            }

            catch (Exception e)
            {
                Console.WriteLine("Failed to conncet");
                goto connection;
            }
        }
    }
}