﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CuraVet.Controllers
{
    [Authorize(Roles ="Far")]
    public class FarmaciaController : Controller
    {
        // GET: Farmacia
        public ActionResult Index()
        {
            return View();
        }
    }
}