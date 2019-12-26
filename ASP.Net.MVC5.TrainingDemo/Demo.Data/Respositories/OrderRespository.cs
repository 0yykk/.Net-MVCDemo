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
    }
}
