using Demo.Data.Respositories;
using Demo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Provider.Provider
{
    public interface IOrderProvider
    {
        string CreateOrder(List<CartListView> cartTable, decimal totalprice);
    }
    public class OrderProvider:IOrderProvider
    {
        private readonly IOrderRespository _orderRespository;
        public OrderProvider(IOrderRespository orderRespository)
        {
            _orderRespository = orderRespository;
        }
        public string CreateOrder(List<CartListView> cartTable, decimal totalprice)
        {
             string i=_orderRespository.CreatOrder(cartTable, totalprice);
            return i;
        }
    }
}
