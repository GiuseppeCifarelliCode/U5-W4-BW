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
    public class ProdottoController : Controller
    {
        private ModelDBContext db = new ModelDBContext();

        // GET: Prodotto
        public ActionResult Index()
        {
            var prodotto = db.Prodotto.Include(p => p.Ditta);
            return View(prodotto.ToList());
        }

        // GET: Prodotto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodotto prodotto = db.Prodotto.Find(id);
            if (prodotto == null)
            {
                return HttpNotFound();
            }
            return View(prodotto);
        }

        // GET: Prodotto/Create
        public ActionResult Create()
        {
            ViewBag.IdDitta = new SelectList(db.Ditta, "IdDitta", "Nome");
            return View();
        }

        // POST: Prodotto/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdProdotto,Nome,IdDitta,Tipologia,Descrizione,Armadio,Cassetto,Presente,Prezzo")] Prodotto prodotto)
        {
            if (ModelState.IsValid)
            {
                db.Prodotto.Add(prodotto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdDitta = new SelectList(db.Ditta, "IdDitta", "Nome", prodotto.IdDitta);
            return View(prodotto);
        }

        // GET: Prodotto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodotto prodotto = db.Prodotto.Find(id);
            if (prodotto == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdDitta = new SelectList(db.Ditta, "IdDitta", "Nome", prodotto.IdDitta);
            return View(prodotto);
        }

        // POST: Prodotto/Edit/5
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdProdotto,Nome,IdDitta,Tipologia,Descrizione,Armadio,Cassetto,Presente,Prezzo")] Prodotto prodotto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prodotto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdDitta = new SelectList(db.Ditta, "IdDitta", "Nome", prodotto.IdDitta);
            return View(prodotto);
        }

        // GET: Prodotto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodotto prodotto = db.Prodotto.Find(id);
            if (prodotto == null)
            {
                return HttpNotFound();
            }
            return View(prodotto);
        }

        // POST: Prodotto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prodotto prodotto = db.Prodotto.Find(id);
            db.Prodotto.Remove(prodotto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        
    }
}
