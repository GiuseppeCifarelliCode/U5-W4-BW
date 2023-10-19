using CuraVet.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CuraVet.Controllers
{
    [Authorize(Roles = "Far, Vet")]
    public class ClienteController : Controller
    {
        private ModelDBContext db = new ModelDBContext();
        // GET: Cliente
        public ActionResult Index()
        {
            return View(db.Cliente.ToList());
        }

        [HttpGet]
        public ActionResult AddCliente()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCliente(Cliente c)
        {
            if (ModelState.IsValid)
            {
                db.Cliente.Add(c);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();

        }

        [HttpGet]
        public ActionResult ModifyCliente(int id)
        {
            Cliente c = db.Cliente.Find(id);
            return View(c);
        }
        [HttpPost]
        public ActionResult ModifyCliente(Cliente c)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(c);
            }
        }

        public ActionResult DeleteCliente(int id)
        {
            Cliente c = db.Cliente.Find(id);
            db.Cliente.Remove(c);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}