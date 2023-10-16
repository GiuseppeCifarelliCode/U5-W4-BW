using CuraVet.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CuraVet.Controllers
{
    public class ClinicaController : Controller
    {
        private ModelDBContext db = new ModelDBContext();
        // GET: Clinica

        public ActionResult Index()
        {
            return View();
        }

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
        public ActionResult DeleteCliente(int id)
        {
            Cliente c = db.Cliente.Find(id);
            db.Cliente.Remove(c);
            db.SaveChanges();
            return RedirectToAction("ClientiList");
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
                return RedirectToAction("ClientiList");
            }
            else
            {
                return View(c);
            }
        }
        [HttpGet]
        public ActionResult AddAnimale()
        {
            List<SelectListItem> listClienti = new List<SelectListItem>();
            List<SelectListItem> listTipo = new List<SelectListItem>();
            List<Cliente> c = db.Cliente.ToList();
            List<Tipologia> t = db.Tipologia.ToList();
            SelectListItem itemDefault = new SelectListItem { Text = $"Nessun Padrone", Value = "NULL" };
            listClienti.Add(itemDefault);
            foreach (Cliente cl in c)
            {
                SelectListItem item = new SelectListItem { Text = $"{cl.Nome} {cl.Cognome}", Value = $"{cl.IdCliente}" };
                listClienti.Add(item);
            }
            ViewBag.ListClienti = listClienti;
            foreach (Tipologia ti in t)
            {
                SelectListItem item = new SelectListItem { Text = $"{ti.Tipo}", Value = $"{ti.IdTipologia}" };
                listTipo.Add(item);
            }
            ViewBag.ListTipo = listTipo;
            return View();
        }
        [HttpPost]
        public ActionResult AddAnimale(Animale a, HttpPostedFileBase Foto)
        {
            List<SelectListItem> listClienti = new List<SelectListItem>();
            List<SelectListItem> listTipo = new List<SelectListItem>();
            List<Cliente> c = db.Cliente.ToList();
            List<Tipologia> t = db.Tipologia.ToList();
            SelectListItem itemDefault = new SelectListItem { Text = $"Nessun Padrone"};
            listClienti.Add(itemDefault);
            foreach (Cliente cl in c)
            {
                SelectListItem item = new SelectListItem { Text = $"{cl.Nome} {cl.Cognome}", Value = $"{cl.IdCliente}" };
                listClienti.Add(item);
            }
            ViewBag.ListClienti = listClienti;
            foreach (Tipologia ti in t)
            {
                SelectListItem item = new SelectListItem { Text = $"{ti.Tipo}", Value = $"{ti.IdTipologia}" };
                listTipo.Add(item);
            }
            ViewBag.ListTipo = listTipo;
            if (ModelState.IsValid)
            {
                if (Foto != null && Foto.ContentLength > 0)
                {
                    string nomeFile = Foto.FileName;
                    string path = Path.Combine(Server.MapPath("~/Content/assets"), nomeFile);
                    Foto.SaveAs(path);
                    a.Foto = nomeFile;
                }
                db.Animale.Add(a);
                db.SaveChanges();
                return RedirectToAction("ClientiList");
            }
            else
            {
                return View();
            }
        }

    }
}