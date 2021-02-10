using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace bondarchuk_zhukovskyLab2_client_
{
    class Program
    {
        static int port = 8005;
        static string address = "127.0.0.1";
        static void Main(string[] args)
        {
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);
                char c;
                string answer;
                do
                {
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socket.Connect(ipPoint);
                    Console.Write("Введите операцию: ");
                    string operation = Console.ReadLine();
                    byte[] data = Encoding.Unicode.GetBytes(operation);
                    socket.Send(data);

                    data = new byte[256];
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;

                    do
                    {
                        bytes = socket.Receive(data, data.Length, 0);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (socket.Available > 0);
                    Console.WriteLine("ответ сервера: " + builder.ToString());

                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                    Console.Write("желаете продолжить выполнение операций со счётом?(y/n) ");
                    answer = Console.ReadLine();
                    while (!char.TryParse(answer, out c) || (c != 'y' && c != 'n'))
                    {
                        Console.WriteLine("недопустимый ответ");
                        Console.Write("желаете продолжить выполнение операций со счётом?(y/n) ");
                        answer = Console.ReadLine();
                    }

                    Console.WriteLine();
                }
                while (c == 'y');

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}