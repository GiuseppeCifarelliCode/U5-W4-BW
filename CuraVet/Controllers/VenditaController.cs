using CuraVet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CuraVet.Controllers
{
    [Authorize(Roles = "Far")]
    public class VenditaController : Controller
    {
        private static ModelDBContext db = new ModelDBContext();
        public List<Cliente> cliente = db.Cliente.ToList();
        public List<SelectListItem> c
        {
            get
            {
                List<SelectListItem> list = new List<SelectListItem>();
                foreach (Cliente c in cliente)
                {
                    SelectListItem item = new SelectListItem { Text = c.Cognome+c.Nome, Value = c.IdCliente.ToString() };
                    list.Add(item);
                }
                return list;
            }
        }
        // GET: Vendita
        public ActionResult Index()
        {
            return View(db.Prodotto.Where(p => p.Presente).ToList());
        }

        public ActionResult AddVendita(int id)
        {
            Prodotto p = db.Prodotto.Find(id);
            Vendita v = new Vendita();
            v.IdProdotto = p.IdProdotto;
            ViewBag.Cliente = c;
            return View(v);
        }
        [HttpPost]
        public ActionResult AddVendita(Vendita v)
        {
            if (ModelState.IsValid)
            {
                db.Vendita.Add(v);
                db.SaveChanges();
                return RedirectToAction("Index", "Farmacia");
            }
            return View();

        }

        public ActionResult List()
        {
            return View(db.Vendita.ToList());
        }

        public ActionResult GetProductByDate(DateTime date)
        {
            List<Vendita> vendite = db.Vendita.Where(v => v.Data == date).ToList();
            var formattedVendite = vendite.Select(v => new
            {
                v.IdVendita,
                v.IdCliente,
                v.IdProdotto,
                v.Quantita,
                v.RicettaMedica,
                Data = v.Data.ToString("yyyy-MM-dd"),
            }).ToList();
            return Json(formattedVendite,JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProductByCF(string cf)
        {
            List<Vendita> vendite = db.Vendita.Where(v => v.Cliente.CF == cf).ToList();
            var formattedVendite = vendite.Select(v => new
            {
                v.IdVendita,
                v.IdCliente,
                v.IdProdotto,
                v.Quantita,
                v.RicettaMedica,
                Data = v.Data.ToString("yyyy-MM-dd"),
            }).ToList();
            return Json(formattedVendite, JsonRequestBehavior.AllowGet);
        }
    }
}