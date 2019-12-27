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
        public  string CreatOrder(List<CartListView> cartTable, decimal totalprice)
        {
            var guid = Guid.NewGuid().ToString();
            var storeProduceName = "[dbo].[CreatUserOrderDetail]";
             var result=_dbContext.Database.SqlQuery<OrderViewModel>(
                $"{storeProduceName} @guid,@orderDate,@totalprice",
                new SqlParameter("@guid", guid),
                new SqlParameter("@orderDate", DateTime.Now),
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
    }
}
