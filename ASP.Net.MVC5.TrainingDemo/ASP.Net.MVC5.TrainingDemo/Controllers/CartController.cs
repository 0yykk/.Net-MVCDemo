using ASP.Net.MVC5.TrainingDemo.Models;
using Demo.Domain;
using Demo.Provider.Provider;
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
        private readonly IOrderProvider _orderProvider;
        public CartController(IOrderProvider orderProvider)
        {
            _orderProvider = orderProvider;
        }
        //GET: Cart
        public ActionResult Order()
        {
            return View();
        }
        public ActionResult ShoppingDetail()
        {
            string orderGuid = Request.QueryString["OrderGuid"];

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
        
        public JsonResult OrderList(List<CartListView> cartTable)
        {
            List<CartListView> i = new List<CartListView>();
            i = cartTable;
            decimal totalPrice = 0;
            
            foreach(var item in cartTable)
            {  
                totalPrice += item.Count * item.Price;
                
            }
            string a=_orderProvider.CreateOrder(i, totalPrice);
            return Json(a, JsonRequestBehavior.AllowGet);

        }

    }
}