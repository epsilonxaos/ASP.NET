using AppConBD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppConBD.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            MantenimientoArticulo man = new MantenimientoArticulo();
            return View(man.MostrarTodos());
        }
    }
}