using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.Net.MVC5.TrainingDemo.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Order()
        {
            return View();
        }
        public ActionResult ShoppingDetail()
        {
            return View();
        }
    }
}