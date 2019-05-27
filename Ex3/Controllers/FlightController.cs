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
        private string result1;

        // GET: Flight
 

        [HttpGet]
        public ActionResult display(string ip, int port, int time)
        {

            ConnectFlight.Instance.ip = ip;
            ConnectFlight.Instance.port = port.ToString();
            ConnectFlight.Instance.time = time;

           // ConnectFlight.Instance.ReadData("Lat");

            Session["time"] = time;
            return View();
        }

        [HttpPost]
        public string GetFlightData()
        {
            var fly = ConnectFlight.Instance.Flight;
            return ToXml(fly);
        }

        private string ToXml(Flight flight)
        {
            //if (Models.ConnectFlight.Instance.IsConnect)
            //{
            this.connect = new ConnectFlight();
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            XmlWriter writer = XmlWriter.Create(sb, settings);
            writer.WriteStartDocument();
            writer.WriteStartElement("Flight");
            //writer.WriteElementString("lat", connect.PhraserValue(connect.SendCommands("lat")));
            //writer.WriteElementString("lon", connect.PhraserValue(connect.SendCommands("lon")));
           
            writer.WriteElementString("Lat", "100");
            writer.WriteElementString("Lon", "600");

            //flight.ToXml(writer);

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();

            Console.Write(sb.ToString());
            return sb.ToString();
            //}
            return null;
        }

        [HttpPost]
        public string CallCommand()
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
        }
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

    }
}
