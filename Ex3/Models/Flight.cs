using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Xml;

namespace Ex3.Models
{
    public class Flight
    {
        public string Lat { get; set; }
        public string Lon { get; set; }

        public void ToXml(XmlWriter writer)
        {
            writer.WriteStartElement("Flight");
            writer.WriteElementString("lon", this.Lon);
            writer.WriteElementString("lat", this.Lat);
            writer.WriteEndElement();
        }

        double Test()
        {
            double x = 100;
            return x;
        }
    }


}
