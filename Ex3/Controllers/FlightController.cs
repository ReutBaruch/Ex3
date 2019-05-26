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
        public ActionResult display(string ip, int port)
        {
            this.connect = ConnectFlight.Instance;
            //connect.ServerConnect(ip, port);
            //string result = connect.SendCommands("lat");
            result1 = "/position/longitude-deg = '-60'";
           // double re = PhraserValue(result);
           
            return View();
        }

        [HttpPost]
        public string CallCommand()
        {
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            XmlWriter writer = XmlWriter.Create(sb, settings);

            writer.WriteStartDocument();
            writer.WriteStartElement("CORD");
            writer.WriteElementString("lat",connect.PhraserValue(connect.SendCommands("lat")));
            writer.WriteElementString("lon", connect.PhraserValue(connect.SendCommands("lon")));

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();

            return sb.ToString();
            //return connect.PhraserValue(result1);
        }

        public string Index()
        {
            return "Welcome to our Project! Please enter a URL";
        }

    }
}