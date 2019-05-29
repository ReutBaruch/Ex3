using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using Ex3.Models;

namespace Ex3.Controllers
{
    public class FlightController : Controller
    {
        private ConnectFlight connect;
        //private string result1;

        // GET: Flight


        [HttpGet]
        public ActionResult display(string ip, int port, int time)
        {
            string[] result = new string[2];
            connect = ConnectFlight.Instance;
            connect.ServerConnect(ip, port);
            //result = connect.SendCommands();
            //connect.Flight.Lat = connect.PhraserValue(result[0]);
            //connect.Flight.Lon = connect.PhraserValue(result[1]);

            Session["time"] = time;
            return View();
        }

        [HttpPost]
        public string GetFlightData()
        {
            // var fly = ConnectFlight.Instance.Flight
            string[] result = new string[2];
            result = ConnectFlight.Instance.SendCommands();
            ConnectFlight.Instance.Flight.Lat = ConnectFlight.Instance.PhraserValue(result[0]);
            ConnectFlight.Instance.Flight.Lon = ConnectFlight.Instance.PhraserValue(result[1]);
            var fly = ConnectFlight.Instance.Flight;
            return ToXml(fly);

        }

        private string ToXml(Flight flight)
        {
            if (Models.ConnectFlight.Instance.IsConnect)
            {
                StringBuilder sb = new StringBuilder();
                XmlWriterSettings settings = new XmlWriterSettings();
                XmlWriter writer = XmlWriter.Create(sb, settings);
                writer.WriteStartDocument();
                writer.WriteStartElement("Flight");
                //writer.WriteElementString("Lat", connect.PhraserValue(connect.SendCommands("Lat")));
                //writer.WriteElementString("Lon", connect.PhraserValue(connect.SendCommands("Lon")));
                writer.WriteElementString("lat", flight.Lat);
                writer.WriteElementString("lon", flight.Lon);
                

                //flight.ToXml(writer);

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();

                Console.Write(sb.ToString());
                return sb.ToString();
            }
            return null;
        }

        /*[HttpPost]
        public string ToXml()
        {
            if (Models.ConnectFlight.Instance.IsConnect)
            {
                StringBuilder sb = new StringBuilder();
                XmlWriterSettings settings = new XmlWriterSettings();
                XmlWriter writer = XmlWriter.Create(sb, settings);
                writer.WriteStartDocument();
                writer.WriteStartElement("CORD");
                writer.WriteElementString("lat", ConnectFlight.Instance.PhraserValue(ConnectFlight.Instance.SendCommands("Lat")));
                writer.WriteElementString("lon", ConnectFlight.Instance.PhraserValue(ConnectFlight.Instance.SendCommands("Lon")));

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();

                Console.Write(sb.ToString());
                return sb.ToString();
            }
            return null;

            //return connect.PhraserValue(result1);
        }*/

        [HttpPost]
        public string Search(string data)
        {
            ConnectFlight.Instance.ReadData(data);
            return ToXml(ConnectFlight.Instance.Flight);
        }

        public string Index()
        {
            return "Welcome to our Project! Please enter a URL";
        }

        [HttpGet]
        public ActionResult save(string ip, int port, int time, int seconds, string file)
        {
            return View();
        }
    }
}
