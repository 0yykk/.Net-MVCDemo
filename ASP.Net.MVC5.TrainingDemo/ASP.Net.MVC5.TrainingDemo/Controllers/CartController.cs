using ASP.Net.MVC5.TrainingDemo.Models;
using Demo.Core.Utilities;
using Demo.Domain;
using Demo.Provider.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ASP.Net.MVC5.TrainingDemo.Controllers
{
    public class CartController : Controller
    {
        private readonly IOrderProvider _orderProvider;
        private readonly IddlProvider _ddlProvider;
        public CartController(IOrderProvider orderProvider,
            IddlProvider ddlProvider
            )
        {
            _orderProvider = orderProvider;
            _ddlProvider = ddlProvider;
        }
        //GET: Cart
        public async Task<ActionResult> Order()
        {
            var Orderlist = new List<OrderViewModel>();
            Orderlist = await _orderProvider.GetAllOrder();
            int pageindex = 1;
            var recordCount = Orderlist.Count();
            if (Request.QueryString["page"] != null)
                pageindex = Convert.ToInt32(Request.QueryString["page"]);
            const int PAGE_SZ = 10;
            ViewBag.OrderList = Orderlist.OrderByDescending(art=>art.OrderGuid)
                .Skip((pageindex - 1) * PAGE_SZ)
                .Take(PAGE_SZ).ToList();
            ViewBag.Pager = new PagerHelper()
            {
                PageIndex = pageindex,
                PageSize = PAGE_SZ,
                RecordCount = recordCount,
            };
            return View();
        }
        public ActionResult SubmitSuccess()
        {
            return View();
        }
        public async Task<ActionResult> ShoppingDetail()
        {
            string orderGuid = Request.QueryString["OrderGuid"];
            var modelOrder = new OrderViewModel();
            modelOrder =await  _orderProvider.getOrder(orderGuid);
            var modelCart = await _orderProvider.getCartList(orderGuid);
            ViewBag.OrederDetail = modelCart;
            var countryList = new List<CountryViewModel>();
            countryList = await _ddlProvider.GetCountryList();
            ViewBag.CountryList = countryList;
            return View(modelOrder);
        }
        public JsonResult GetStateList(string countryCode)
        {
            List<StateViewModel> stateList = new List<StateViewModel>();
            stateList = _ddlProvider.GetStateList(countryCode);
            return Json(stateList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCityList(string stateCode)
        {
            List<CityViewModel> cityList = new List<CityViewModel>();
            cityList = _ddlProvider.GetCityList(stateCode);
            return Json(cityList, JsonRequestBehavior.AllowGet);
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
        public JsonResult GetOrderDetail(string orderguid)
        {
            var i = Regex.Replace(orderguid, @"\n", "").Trim(); 
            List<CartListView> list = new List<CartListView>();
            list = _orderProvider.GetThisOrderDetail(i);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int id,string orderId)
        {
            string str = null;
            if (orderId.IndexOf('?') != -1)
            {
                 str= orderId.Split('?')[1];
                 str = str.Split('=')[1];
            }
            
            var result = new DeleteMassage();  
            int returnValue = _orderProvider.DeleteItem(id, str);
            if (returnValue == 0)
            {
                result.IsSuccess = true;
                result.ErrorMessage = "Delete success!";
            }

            else
            {
                result.IsSuccess = false;
                result.ErrorMessage = "The current item has been deleted!";
            }
            
            return Json(result);
        }
        public ActionResult DeleteOrder(string Id)
        {
            var i = Id;
            int returnValue = _orderProvider.DeleteOrder(i);
            var result = new DeleteMassage();
            if (returnValue == 0)
            {
                result.IsSuccess = true;
                result.ErrorMessage = "Delete success!";
            }

            else
            {
                result.IsSuccess = false;
                result.ErrorMessage = "The current order has been deleted!";
            }
            //else
            //{
            //result.IsSuccess = false;
            //result.ErrorMessage = "Network busy, please try again later";
            //}
            return Json(result);
        }
        [HttpPost]
        public async Task<ActionResult> ShoppingDetail(OrderViewModel order)
        {
            if (order != null)
            {
                await _orderProvider.UpdateOrder(order);

                return Redirect("SubmitSuccess");
            }
            return View();
        }
        [HttpPost]
        public async Task<ActionResult>Order(OrderViewModel order)
        {
            
            var orderlist = new List<OrderViewModel>();
            int pageindex = 1;
            var recordCount = 0;
            const int PAGE_SZ = 10;
            if (order.OrderDate == null && order.UserName != null)
            {
                 orderlist = await _orderProvider.GetOrderByUserName(order.UserName);
                 recordCount = orderlist.Count();
                 if (Request.QueryString["page"] != null)
                    pageindex = Convert.ToInt32(Request.QueryString["page"]);
                             
            }
            else if (order.OrderDate!=null&&order.UserName==null)
            {
                orderlist = await _orderProvider.GetOrderByDate(Convert.ToDateTime(order.OrderDate));
                recordCount = orderlist.Count();
                if (Request.QueryString["page"] != null)
                    pageindex = Convert.ToInt32(Request.QueryString["page"]);
            }
            else if (order.OrderDate != null && order.UserName != null)
            {
                orderlist = await _orderProvider.GetOrderByDateandName(Convert.ToDateTime(order.OrderDate), order.UserName);
                recordCount = orderlist.Count();
                if (Request.QueryString["page"] != null)
                    pageindex = Convert.ToInt32(Request.QueryString["page"]);
            }
            else
            {
                return Redirect("Order");
            }
            ViewBag.OrderList = orderlist.OrderByDescending(art => art.OrderGuid)
                    .Skip((pageindex - 1) * PAGE_SZ)
                    .Take(PAGE_SZ).ToList();
            ViewBag.Pager = new PagerHelper()
            {
                PageIndex = pageindex,
                PageSize = PAGE_SZ,
                RecordCount = recordCount,
            };
            return View();
        }

    }
}