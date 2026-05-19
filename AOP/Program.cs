using System.Reflection;

namespace AOP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //IOrderService service = new OrderService("Laptop");
            IOrderService service = LoggingProxy<IOrderService>.Create(new OrderService("Laptop"));
            service.CreateOrder();
            int count = service.GetOrderCount();
            Console.WriteLine($"Count = {count}");
        }
    }
}
