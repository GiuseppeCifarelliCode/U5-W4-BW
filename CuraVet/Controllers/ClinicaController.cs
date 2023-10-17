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
    [Authorize(Roles = "Vet")]
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
            SelectListItem itemDefault = new SelectListItem { Text = $"Nessun Padrone" };
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
                a.DataRegistrazione = DateTime.Now;
                db.Animale.Add(a);
                db.SaveChanges();
                return RedirectToAction("ClientiList");
            }
            else
            {
                return View();
            }
        }
        public ActionResult AnimaliList()
        {
            return View(db.Animale.ToList());
        }
        [HttpGet]
        public ActionResult AddVisita(int id)
        {
            TempData["Id"] = id;
            return View();
        }
        [HttpPost]
        public ActionResult AddVisita(Visita v)
        {
            if (ModelState.IsValid)
            {
                v.DataVisita = DateTime.Now;
                v.IdAnimale = (int)TempData["Id"];
                v.Attiva = true;
                db.Visita.Add(v);
                db.SaveChanges();
                return RedirectToAction("AnimaliList");
            }
            else
            {
                {
                    return View(v);
                }
            }
        }
        public ActionResult AnimaleDetails(int id)
        {
            Animale a = db.Animale.Find(id);
            return View(a);
        }
        [HttpGet]
        public ActionResult ModifyVisita(int id)
        {
            Visita v = db.Visita.Find(id);
            return View(v);
        }
        [HttpPost]
        public ActionResult ModifyVisita(Visita v)
        {
            if (ModelState.IsValid)
            {
                db.Entry(v).State = EntityState.Modified;
                db.SaveChanges();
                int id = v.IdAnimale;
                return RedirectToAction("AnimaleDetails", new { 
                    id});
            }
            else
            {
                return View(v);
            }
        }
        public JsonResult GetAnimaleByChipNr(string ChipNr)
        {
            List<Animale> a = db.Animale.Where(x => x.Microchip == ChipNr).ToList();
            var formattedAnimals = a.Select(o => new
            {
                o.IdAnimale,
                o.Nome,
                o.IdTipologia,
                o.Razza,
                o.Colore,
                DataNascita = o.DataNascita.ToString(),
                DataReg = o.DataRegistrazione.ToString(),
                o.Microchip,
                o.IdCliente,
                o.Foto

            }).ToList();
            return Json(formattedAnimals, JsonRequestBehavior.AllowGet);
        }

    }
}