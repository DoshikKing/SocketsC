using System.Net;
using System.Net.Sockets;


namespace Server
{
    class Program
    {
        // Listener
        static TcpListener? listener;
        //Number of connections
        const int LIMIT = 5;
        const int PORT = 12000;

        public static void Main()
        {
            // Defining new TcpListener
            listener = new TcpListener(new IPEndPoint(IPAddress.Parse("127.0.0.1"), PORT));
            //Starting listener
            listener.Start();
            Console.WriteLine("Server started! Listening on port: {0}", PORT);

            // Starting new threads for each new connections
            for (int i = 0; i < LIMIT; i++)
            {
                Thread t = new Thread(new ThreadStart(Service));
                t.Start();
            }
        }
        public static void Service()
        {
            while (true)
            {
                Socket soc = listener.AcceptSocket();
                Console.WriteLine("Connected: {0}", soc.RemoteEndPoint);
                try
                {
                    Stream s = new NetworkStream(soc);
                    StreamReader sr = new StreamReader(s);
                    StreamWriter sw = new StreamWriter(s);
                    // enable automatic flushing
                    sw.AutoFlush = true;
                    sw.WriteLine("Welcome!");
                    while (true)
                    {
                        // Reading incoming stream from client
                        string msg = sr.ReadLine();

                        // Ending if stream is empty
                        if (msg == "" || msg == null) break;

                        string job = "";

                        job += tasks(Convert.ToInt16(msg), sr, sw);

                        // Sending warnin' if task does not exist
                        if (job == "0") job = "No such task!";

                        // Sending outcoming stream to client
                        sw.WriteLine(job);
                    }
                    // Closing stream
                    s.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine("Disconnected: {0}", soc.RemoteEndPoint);

                // Closing connection
                soc.Close();
            }
        }

        private static string tasks(int taks_number, StreamReader sr, StreamWriter sw)
        {
            string result = "";
            switch (taks_number)
            {
                case 12: 
                {
                    int n = 1;
                    double z = 0;
                    while (true)
                    {
                        z = (Math.Log10(9 * n) / n*n);
                        if (z < 1)
                        {
                            break;
                        }
                        n++;
                    }
                    return result += Math.Round(z,2);
                }
                case 15:
                {
                    while (true)
                    {
                        string msg = sr.ReadLine();
                        double num = Convert.ToDouble(msg);
                        if(num % 3 == 0)
                        {
                            break;
                        }
                        sw.WriteLine(Math.Round(Math.Pow(num, 2),2));
                    }
                    return result += "end";
                } 
                case 18:
                {
                    for (double i = 3; i > -8; i-=0.9)
                    {
                        double z = Math.Round((Math.Log10(Math.Abs(i)) + Math.Tan(2 * i)), 2);
                        if (z > 2)
                        {
                            result += z + " ";
                        }
                    }
                    return result;
                }
                case 21:
                {
                    string msg = sr.ReadLine();
                    if (Convert.ToDouble(msg) > 0)
                    {
                        return result += Convert.ToString(msg.First());
                    }
                    return result += "Num is negative!";
                }
                case 24:
                {
                    string msg = sr.ReadLine();
                    double n = Convert.ToDouble(msg);
                    double f = 0;
                    for (double i = 0; i < 10000; i+=0.1)
                    {
                        f = Math.Pow(n, i);
                        if (f > n)
                        {
                            result += f;
                            break;
                        }
                    }
                    return result;
                }
            }
            return "";
        }
    }
    
}
