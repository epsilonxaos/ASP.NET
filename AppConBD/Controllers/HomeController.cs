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

        //GET: Alta
        public ActionResult Alta()
        {
            return View();
        }

        //Metodo: Recibir datos del formulario
        [HttpPost]
        public ActionResult Alta(FormCollection collection)
        {
            MantenimientoArticulo man = new MantenimientoArticulo();
            Articulo art = new Articulo {
                Codigo = int.Parse(collection["codigo"]),
                Descripcion = collection["descripcion"],
                Precio = float.Parse(collection["precio"].ToString())
            };
            man.Alta(art);
            return RedirectToAction("Index");
        }

        //GET: Baja
        public ActionResult Baja(int cod)
        {
            MantenimientoArticulo man = new MantenimientoArticulo();
            man.Borrar(cod);

            return RedirectToAction("Index");
        }

        public ActionResult Modificacion(int cod)
        {
            MantenimientoArticulo man = new MantenimientoArticulo();
            Articulo art = man.Recuperar(cod);
            return View(art);
        }

        [HttpPost]
        public ActionResult Modificacion(FormCollection collection)
        {
            MantenimientoArticulo man = new MantenimientoArticulo();
            Articulo art = new Articulo {
                Codigo = int.Parse(collection["codigo"].ToString()),
                Descripcion = collection["descripcion"].ToString(),
                Precio = float.Parse(collection["precio"].ToString())
            };
            man.Modificar(art);

            return RedirectToAction("Index");
        }
    }
}