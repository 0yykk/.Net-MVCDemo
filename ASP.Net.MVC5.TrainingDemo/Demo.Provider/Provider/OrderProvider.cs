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
        Task<List<CartListView>>getCartList(string ordguid);
        Task<OrderViewModel> getOrder(string ordguid);
        int DeleteItem(int id,string orderGuid);
    }
    public class OrderProvider:IOrderProvider
    {
        private readonly IOrderRespository _orderRespository;
        public OrderProvider(IOrderRespository orderRespository)
        {
            _orderRespository = orderRespository;
        }
        public int DeleteItem(int id,string orderGuid)
        {
            int returnValue=3;
            returnValue = _orderRespository.DeleteItem(id, orderGuid);
            return returnValue;
        }
        public string CreateOrder(List<CartListView> cartTable, decimal totalprice)
        {
             string i=_orderRespository.CreatOrder(cartTable, totalprice);
            return i;
        }
        public async Task<List<CartListView>> getCartList(string ordguid)
        {
            var _cartList = new List<CartListView>();
            _cartList = await _orderRespository.getCartList(ordguid);
            return (_cartList == null) ? new List<CartListView>() : _cartList;
        }
        public async Task<OrderViewModel> getOrder(string ordguid)
        {
            var _orderList = new OrderViewModel();
            _orderList = await _orderRespository.getOrder(ordguid);
            return (_orderList == null) ? new OrderViewModel() : _orderList;
        }
    }
}
