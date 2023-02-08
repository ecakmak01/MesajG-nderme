using System;
using System.Text;
using System.Net.Sockets;
using System.IO;

namespace MessageAppServerSide
{                                                /// Server sided app
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(System.Net.IPAddress.Any, 1302);
            listener.Start();

            while(true)
            {
                Console.WriteLine("Waiting for a conneciton...");
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("Client accepted");
                NetworkStream stream = client.GetStream();
                StreamReader sr = new StreamReader(client.GetStream());
                StreamWriter sw = new StreamWriter(client.GetStream());

                try
                {
                    byte[] buffer = new byte[1024];
                    stream.Read(buffer, 0, buffer.Length);
                    int rcv = 0;
                    foreach (byte b in buffer)
                    {
                        if (b != 0)
                        {
                            rcv++;
                        }
                    }

                    string request = Encoding.UTF8.GetString(buffer, 0, rcv);
                    Console.WriteLine("Request received: " + request);
                    sw.WriteLine("Hello");
                    sw.Flush();
                }

                catch (Exception e)
                {
                    Console.WriteLine("Something went wrong");
                    sw.WriteLine(e.ToString());
                }

                }

            }
        
        }
    }

