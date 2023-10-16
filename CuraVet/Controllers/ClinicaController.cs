using CuraVet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CuraVet.Controllers
{
    public class ClinicaController : Controller
    {
        private ModelDBContext db = new ModelDBContext();
        // GET: Clinica
        [HttpGet]
        public ActionResult AddTipologia()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddTipologia(Tipologia t)
        {
            if (ModelState.IsValid)
            {
                db.Tipologia.Add(t);
                db.SaveChanges();
                ViewBag.Message = "Tipologie Aggiornate";
                return RedirectToAction("TipologiaList");
            }
            else
            {
                ViewBag.Message = "Errore durante l'inserimento";
                return View();
            }

        }
        public ActionResult DeleteTipologia(int id)
        {
            Tipologia t = db.Tipologia.Find(id);
            db.Tipologia.Remove(t);
            db.SaveChanges();
            return RedirectToAction("TipologiaList");
        }
        public ActionResult TipologiaList()
        {
            return View(db.Tipologia.ToList());
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
                return RedirectToAction("Index", "Home");
            }
                return View();
            
        }
        public ActionResult ClientiList()
        {
            return View(db.Cliente.ToList());
        }

    }
}