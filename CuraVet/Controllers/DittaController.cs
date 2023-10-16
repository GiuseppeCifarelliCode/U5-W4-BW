using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CuraVet.Models;

namespace CuraVet.Controllers
{
    public class DittaController : Controller
    {
        private ModelDBContext db = new ModelDBContext();

        // GET: Ditta
        public ActionResult Index()
        {
            return View(db.Ditta.ToList());
        }

        // GET: Ditta/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ditta ditta = db.Ditta.Find(id);
            if (ditta == null)
            {
                return HttpNotFound();
            }
            return View(ditta);
        }

        // GET: Ditta/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ditta/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDitta,Nome,Recapito,Indirizzo")] Ditta ditta)
        {
            if (ModelState.IsValid)
            {
                db.Ditta.Add(ditta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ditta);
        }

        // GET: Ditta/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ditta ditta = db.Ditta.Find(id);
            if (ditta == null)
            {
                return HttpNotFound();
            }
            return View(ditta);
        }

        // POST: Ditta/Edit/5
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDitta,Nome,Recapito,Indirizzo")] Ditta ditta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ditta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ditta);
        }

        // GET: Ditta/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ditta ditta = db.Ditta.Find(id);
            if (ditta == null)
            {
                return HttpNotFound();
            }
            return View(ditta);
        }

        // POST: Ditta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ditta ditta = db.Ditta.Find(id);
            db.Ditta.Remove(ditta);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

       
    }
}
