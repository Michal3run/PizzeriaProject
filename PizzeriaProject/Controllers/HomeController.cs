using PizzeriaProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PizzeriaProject.Controllers
{    

    public class HomeController : Controller
    {
        PizzaDbContext _context = new PizzaDbContext();

        public ActionResult Index()
        {
            //IEnumerable<Pizza> pizzas = _context.Pizzas.ToList();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if(_context != null)
            {
                _context.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}