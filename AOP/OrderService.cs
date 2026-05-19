using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOP
{
    public class OrderService : IOrderService
    {
        string _product;
        public OrderService(string product)
        {
            _product = product;
        }
        public void CreateOrder()
        {
            Console.WriteLine($"Creating order for {_product}");
        }

        public int GetOrderCount()
        {
            Console.WriteLine("Getting order count");
            return 42;
        }
    }
}
