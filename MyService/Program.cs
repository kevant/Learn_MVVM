using Grpc.Core;
using Items;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var server = new Grpc.Core.Server();
            server.Services.Add(ItemService.BindService(new ItemServiceImpl()));
            server.Ports.Add(new Grpc.Core.ServerPort("127.0.0.1", 5000, Grpc.Core.ServerCredentials.Insecure));
            server.Start();

            while (true)
            {
                Console.WriteLine("Simple IOS Login Service");
                Console.WriteLine("press 'q' to quit\n");
                var input = Console.ReadKey();
                if (input.KeyChar == 'q')
                {
                    break;
                }
            }

            server.ShutdownAsync().Wait();
        }
    }
}
