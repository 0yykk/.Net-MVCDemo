using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.Net.MVC5.TrainingDemo.Controllers
{
    public class StoreManagerController : Controller
    {
        // GET: StoreManager
        public ActionResult Store()
        {
            return View();
        }
        public ActionResult Edit()
        {
            return View();
        }
        public ActionResult Add()
        {
            return View();
        }

    }
}