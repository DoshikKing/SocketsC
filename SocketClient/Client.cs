using System.Net.Sockets;

namespace Client
{
    internal class Client
    {
        public static void Main()
        {
            TcpClient client = new TcpClient("127.0.0.1", 12000);
            try
            {
                Stream s = client.GetStream();
                StreamReader sr = new StreamReader(s);
                StreamWriter sw = new StreamWriter(s);
                sw.AutoFlush = true;

                Console.WriteLine(sr.ReadLine());
                while (true)
                {
                    Console.Write("Task number: ");
                    string task = Console.ReadLine();
                    sw.WriteLine(task);
                    if(task == "21" || task == "24")
                    {
                        string num = Console.ReadLine();
                        sw.WriteLine(num);
                    }
                    if(task == "15")
                    {
                        while (true)
                        {
                            string num = Console.ReadLine();
                            sw.WriteLine(num);
                            string n = sr.ReadLine();
                            Console.WriteLine(n);
                            if (n == "end")
                            {
                                break;
                            }
                        }
                    }
                    if (task == "") break;
                    if (task != "15")
                    {
                        Console.WriteLine(sr.ReadLine());
                    }
                }
                s.Close();
            }
            finally
            {
                client.Close();
            }

        }
    }
}
