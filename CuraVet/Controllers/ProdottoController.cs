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
        private static ModelDBContext db = new ModelDBContext();
        public List<Ditta> ditte = db.Ditta.ToList();
        public List<SelectListItem> d
        {
            get
            {
                List<SelectListItem> list = new List<SelectListItem>();
                foreach ( Ditta ditta in ditte)
                {
                    SelectListItem item = new SelectListItem { Text = ditta.Nome, Value=ditta.IdDitta.ToString()};
                    list.Add(item);
                }
                return list;
            }
        }

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
            ViewBag.Ditte = d;
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
            Prodotto prodotto = db.Prodotto.Find(id);
            db.Prodotto.Remove(prodotto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Search(string productName)
        {
            List<Prodotto> productList = db.Prodotto.Where(p => p.Nome.Contains(productName)).ToList();
            var formattedProduct = productList.Select(p => new
            {
                p.IdProdotto,
                p.Nome,
                p.IdDitta,
                p.Tipologia,
                p.Descrizione,
                p.Armadio,
                p.Cassetto,
                p.Presente,
                p.Prezzo,
            }).ToList();
            TempData["ProductList"] = productList;
            return Json(formattedProduct,JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchList()
        {
            List<Prodotto> productList = TempData["ProductList"] as List<Prodotto>;

            if (productList != null)
            {
                return View(productList);
            }
            else
            {
                return View(new List<Prodotto>());
            }
        }


    }
}
