using ASP.Net.MVC5.TrainingDemo.Models;
using Demo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public ActionResult ShoppingCart()
        {
            var list = new List<CartListView>();
            if(Session["CartList"]!=null)
            {
                list = Session["CartList"] as List<CartListView>;
            }
            return View();
        }
        public ActionResult OrderRequest(List<CartListView> cartTable)
        {
            List<CartListView> i = new List<CartListView>();
            i = cartTable;
            return View("Order");
        }

    }
}