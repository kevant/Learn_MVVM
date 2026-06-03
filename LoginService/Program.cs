namespace Service.Login
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var server = new Grpc.Core.Server();
            server.Services.Add(LoginService.BindService(new LoginServiceImpl()));
            server.Ports.Add(new Grpc.Core.ServerPort("127.0.0.1", 5000, Grpc.Core.ServerCredentials.Insecure));
            server.Start();

            while (true)
            {
                Console.WriteLine("Simple Login Service");
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
