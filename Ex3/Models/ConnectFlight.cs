using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace Ex3.Models
{
    public class ConnectFlight 
    {
        private TcpClient client;
        private StreamWriter writer;
        
        //        private StreamReader reader;

        public bool IsConnect
        {
            get;
            set;
        } = false;

        #region Singleton

        private static ConnectFlight m_instance = null;

        public static ConnectFlight Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new ConnectFlight();
                    //  m_instance.ServerConnect("127.0.0.1", 5402);
                }
                return m_instance;
            }
        }
        #endregion

        public Flight Flight { get; private set; }
        public string ip { get; set; }
        public string port { get; set; }
        public int time { get; set; }

        public ConnectFlight() {
           Flight = new Flight();
        }
        public void Init()
        {
            m_instance = null;
        }

        public const string SCENARIO_FILE = "~/App_Data/{0}.txt";
        public void ReadData(string data)
        {
            /**
             * check about this function
             * */
            string path = HttpContext.Current.Server.MapPath(String.Format(SCENARIO_FILE, data));
            if (!File.Exists(path))
            {
                string str = data;


                using (System.IO.StreamWriter file = new System.IO.StreamWriter(path, true))
                {
                    file.WriteLine(Flight.Lat);
                    file.WriteLine(Flight.Lon);
                }
            }
            else
            {
                string[] lines = System.IO.File.ReadAllLines(path);        // reading all the lines of the file
                Flight.Lon = double.Parse(lines[0]);
                Flight.Lat = double.Parse(lines[1]);
            }
        }

            public void ServerConnect(string ip, int port)
        {

            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
            client = new TcpClient();

            //when client is trying to connect
            while (!client.Connected)
            {
                try
                {
                    // Console.WriteLine("Waiting for client connections...");

                    client.Connect(ep);
                }
                catch (Exception e)
                {
                    throw (e);
                }
            }

            Console.WriteLine("Client connected");
            IsConnect = true;
        }

        public string SendCommands(string lonORlat)
        {
            if (client != null)
            {
                using (NetworkStream stream = client.GetStream())
                using (writer = new StreamWriter(stream))
                using (StreamReader reader = new StreamReader(stream))
                {
                    string command = "";
                    if (lonORlat.Equals("Lat"))
                    {
                        command = "get /position/latitude-deg\r\n";
                    }
                    else
                    {
                        command = "get /position/longitude-deg\r\n";
                    }

                    string finalCommand = command;

                    writer.Write(finalCommand);
                    writer.Flush();
                    string result = reader.ReadLine();

                    return result;
                }
            }
            return "100";
        }

        public string PhraserValue(string toPhras)
        {
            string[] words = toPhras.Split('=');
            //if (words[1] != null)
           // {
            //    words = words[1].Split('\'');
           // }
            //double result = Convert.ToDouble(words[1]);

            //return words[1];
            return "100";
        }

    }
}
