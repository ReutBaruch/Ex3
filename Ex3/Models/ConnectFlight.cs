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
   /** public class ConnectFlight: BaseNotify
    {
        private TcpClient client;
        private TcpListener listener;
        private StreamReader reader;
        private IPEndPoint ep;

        public bool IsConnect
        {
            get;
            set;
        } = false;

        private double? lat;
        private double? lon;

        public bool ShouldStop
        {
            get;
            set;
        } = false;

        private ConnectFlight()
        {
            lat = null;
            lon = null;
        }

        #region Singleton

        private static ConnectFlight m_instance = null;

        public static ConnectFlight Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new ConnectFlight();
                }
                return m_instance;
            }
        }
        #endregion

        public void Init()
        {
            m_instance = null;
        }

        public void ServerConnect(string ip, int port)
        {
            if (listener != null)
            {
                EndConnect();
            }

            ep = new IPEndPoint(IPAddress.Parse(ip), port);
            //listener = new TcpListener(new IPEndPoint(IPAddress.Parse(ip), port));
            listener = new TcpListener(ep);
            listener.Start();
        }

        public void ServerConnect(string ip, int port)
        {
            if (listener != null)
            {
                EndConnect();
            }

            ep = new IPEndPoint(IPAddress.Parse(ip), port);
            listener = new TcpListener(new IPEndPoint(IPAddress.Parse(ip), port));
            listener.Start();
            client = new TcpClient();

            //when client is trying to connect
            while (!client.Connected)
            {
                try
                {
                    Console.WriteLine("Waiting for client connections...");

                    client.Connect(ep);
                }
                catch (Exception)
                {
                    Console.WriteLine("cant connected");
                    //TODO: throw exp
                }
            }

            Console.WriteLine("Client connected");
            //IsConnect = true;

        } //end of ServerConnect function


        public string[] GetInput()
        {

            string commandInput = "";
            string[] input;

            if (!IsConnect)
            {
                IsConnect = true;
                client = listener.AcceptTcpClient();
                reader = new StreamReader(client.GetStream());
            }
            
            commandInput = reader.ReadLine();
            input = commandInput.Split(',');
            string[] result = { input[0], input[1] };

            return result;
        }

        public void EndConnect()
        {
            IsConnect = false;
            listener.Stop();
            client.Close();
        }

        public double? Lat
        {
            get
            {
                return this.lat;
            }

            set
            {
                this.lat = value;
                NotifyPropertyChanged("Lat");

            }
        }

        public double? Lon
        {
            get
            {
                return this.lon;
            }

            set
            {
                this.lon = value;
                NotifyPropertyChanged("Lon");
            }
        }
    }**/

    public class ConnectFlight: BaseNotify
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

        public void Init()
        {
            m_instance = null;
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
            using (NetworkStream stream = client.GetStream())
            using (writer = new StreamWriter(stream))
            using (StreamReader reader = new StreamReader(stream))
            {
                string command = "";
                if (lonORlat.Equals("lat"))
                {
                    command = "get /position/latitude-deg";
                } else
                {
                    command = "get /position/longitude-deg";
                }
                
                string finalCommand = command + "\r\n";

                writer.Write(finalCommand);
                writer.Flush();
                string result = reader.ReadLine();

                return result;
            }
        }

        public string PhraserValue(string toPhras)
        {
            string[] words = toPhras.Split('=');
            words = words[1].Split('\'');

            //double result = Convert.ToDouble(words[1]);

            //return words[1];
            return "60";
        }

    }
}
 