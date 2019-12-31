using Demo.Data.Contexts;
using Demo.Data.Model;
using Demo.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.Respositories
{
    public interface IOrderRespository
    {
        string CreatOrder(List<CartListView> cartTable,decimal totalprice);
        Task<List<CartListView>> getCartList(string ordguid);
        Task<OrderViewModel> getOrder(string ordguid);
        int DeleteItem(int id,string orderGuid);
        Task UpdateOrder(OrderViewModel order);
        Task<List<OrderViewModel>> GetAllOrder();
        List<CartListView> GetThisOrderDetail(string orderguid);
        int DeleteOrder(string orderguid);
        Task<List<OrderViewModel>> GetOrderByDate(DateTime? _date);
        Task<List<OrderViewModel>> GetOrderByUserName(string name);
        Task<List<OrderViewModel>> GetOrderByDateandName(DateTime? _date, string name);
    }
    public class OrderRespository:IOrderRespository
    {
        private readonly IMusicStoreContext _db;
        private readonly DbContext _dbContext;
        public OrderRespository(IMusicStoreContext db)
        {
            _db = db;
            _dbContext = _db.GetDbContext();
        }
        
        public int DeleteItem(int id,string orderGuid)
        {
            var db = new MusicStoreContext();
            int returnValue = 0;
            var model = _db.OrderDetail.FirstOrDefault(s => s.AlbumId == id&&s.OrderGuid==orderGuid);
            decimal i = model.nowTotalPrice;
            if (model != null)
            {
                _db.OrderDetail.Remove(model);
                _dbContext.SaveChanges();
                var ordermodel = _db.Order.FirstOrDefault(d => d.OrderGuid == orderGuid);
                if (ordermodel != null)
                {
                    db.Set<Order>().Attach(ordermodel);
                    db.Entry(ordermodel).State = EntityState.Modified;
                    ordermodel.TotalPrice = (ordermodel.TotalPrice - i);
                    db.SaveChanges();
                    return returnValue;
                }
            }
            return (returnValue = 1);
                
        }
        public int DeleteOrder(string orderguid)
        {
            var storeProduceName = "[dbo].[DeleteOrder]";
            var result = _dbContext.Database.SqlQuery<int>(
                $"{storeProduceName} @guid",
                new SqlParameter("@guid", orderguid)
                ).FirstOrDefault();
            int i = Convert.ToInt32(result);
            return i;
        }
        public async Task<List<OrderViewModel>> GetAllOrder()
        {
            var orderList = new List<OrderViewModel>();
            var orderlist = new List<Order>();
            orderlist = await _db.Order.ToListAsync();
            foreach(var item in orderlist)
            {
                OrderViewModel order = new OrderViewModel();
                order.OrderGuid = item.OrderGuid;
                order.OrderDate = item.OrderDate;
                order.UserName = item.UserName;
                order.Phone = item.Phone;
                order.Country = item.Country;
                order.State = item.State;
                order.City = item.City;
                order.Address = item.Address;
                order.TotalPrice = item.TotalPrice;
                orderList.Add(order);
            }
            return orderList;      
        }
        public async Task<List<OrderViewModel>> GetOrderByDate(DateTime? _date)
        {
            var list = new List<OrderViewModel>();
            var storeProduceName = "[dbo].[GetOrderByDate]";
            var result = await _dbContext.Database.SqlQuery<OrderViewModel>(
                $"{storeProduceName} @date",
                new SqlParameter("@date", _date)
                ).ToListAsync();
            return result;
        }
        public List<CartListView> GetThisOrderDetail(string orderguid)
        {
            var list = new List<CartListView>();
            var storeProduceName = "[dbo].[GetThisOrderDetail]";
            var result = _dbContext.Database.SqlQuery<CartListView>(
                $"{storeProduceName} @guid",
                new SqlParameter("@guid",orderguid)
                ).ToList();
            return result;
        }
        public async Task<List<OrderViewModel>> GetOrderByDateandName(DateTime? _date, string name)
        {
            var list = new List<OrderViewModel>();
            var storeProduceName = "[dbo].[GetOrderByDateandName]";
            var result = await _dbContext.Database.SqlQuery<OrderViewModel>(
                $"{storeProduceName} @date,@name",
                new SqlParameter("@date", _date),
                new SqlParameter("@name", name)
                ).ToListAsync();
            return result;
        }
        public  string CreatOrder(List<CartListView> cartTable, decimal totalprice)
        {
            var guid = Guid.NewGuid().ToString();
            var storeProduceName = "[dbo].[CreatUserOrderDetail]";
             var result=_dbContext.Database.SqlQuery<OrderViewModel>(
                $"{storeProduceName} @guid,@orderDate,@totalprice",
                new SqlParameter("@guid", guid),
                new SqlParameter("@orderDate", DateTime.Now.ToShortDateString()),
                new SqlParameter("@totalprice", totalprice)
            ).FirstOrDefault();
            MusicStoreContext db = new MusicStoreContext();
            foreach (var item in cartTable)
            {
                var newItem = new OrderDetail()
                {
                    OrderDetailGuid = Guid.NewGuid().ToString(),
                    AlbumId = item.AlbumId,
                    Total = item.Count,
                    OrderGuid = guid,
                    nowTotalPrice = (item.Count * (Convert.ToInt32(item.Price)))
                };
                db.OrderDetail.Add(newItem);
                db.SaveChanges();
            }
            return guid;
        }
        public async Task<List<CartListView>> getCartList(string ordguid)
        {
            var storeProduceName = "[dbo].[GetCartListByOrdGuid]";
            var _cartList = new List<CartListView>();
            _cartList = await _dbContext.Database.SqlQuery<CartListView>(
                $"{storeProduceName} @guid",
                new SqlParameter("@guid", ordguid)
            ).ToListAsync();
            return (_cartList != null) ? _cartList :new List<CartListView>();
        }
        public async Task<List<OrderViewModel>> GetOrderByUserName(string name)
        {
            var storeNameProduce = "[dbo].[GetOrderByUserName]";
            var list =new List<OrderViewModel>();
            list = await _dbContext.Database.SqlQuery<OrderViewModel>(
                $"{storeNameProduce} @name",
                new SqlParameter("@name", name)
                ).ToListAsync();
            return list;
        }
        public async Task<OrderViewModel> getOrder(string ordguid)
        {
            var storeProduceName = "[dbo].[GetOrderByGuid]";
            var _orderList = new OrderViewModel();
            _orderList = await _dbContext.Database.SqlQuery<OrderViewModel>(
                $"{storeProduceName} @guid",
                new SqlParameter("@guid", ordguid)
                ).FirstOrDefaultAsync();
            return (_orderList != null) ? _orderList : new OrderViewModel();
        }
        public async Task UpdateOrder(OrderViewModel order)
        {
            var db = new MusicStoreContext();
            var _City =  _db.Cities.FirstOrDefault(c => c.CityCode == order.City).CityName;
            var _Country =  _db.Countries.FirstOrDefault(c => c.CountryCode == order.Country).CountryName;
            var _State =  _db.States.FirstOrDefault(s => s.StateCode == order.State).StateName;
            var model = await _db.Order.FirstOrDefaultAsync(o => o.OrderGuid == order.OrderGuid);
            if (model != null)
            {
                db.Set<Order>().Attach(model);
                db.Entry(model).State = EntityState.Modified;
                model.UserName = order.UserName;
                model.Phone = order.Phone;
                model.Email = order.Email;
                model.Country = _Country;
                model.State = _State;
                model.City = _City;
                model.Address = order.Address;
                model.PostalCode = order.PostalCode;
                db.SaveChanges();
            }
        }
    }
}
