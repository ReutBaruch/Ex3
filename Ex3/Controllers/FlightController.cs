using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ex3.Models;

namespace Ex3.Controllers
{
    public class FlightController : Controller
    {
        // GET: Flight
        [HttpGet]
        public ActionResult display(string ip, int port)
        {
            return View();
        }

        public string Index()
        {
            return "Welcome to our Project! Please enter a URL";
        }

    }
}