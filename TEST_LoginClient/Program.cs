namespace TEST_LoginClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var channel = new Grpc.Core.Channel("127.0.0.1:5000", Grpc.Core.ChannelCredentials.Insecure);
            var loginServiceClient = new LoginService.LoginServiceClient(channel);

            Console.Write("TEST: Login Service\n\n");
            Console.Write("User ID: ");
            var username = Console.ReadLine();
            Console.Write("Password: ");
            var password = Console.ReadLine();

            var request = new LoginRequest
            {
                Username = username,
                Password = password
            };

            var response = loginServiceClient.Authenticate(request);
            if (response.Success)
            {
                Console.WriteLine($"\nHello {response.Displayname}!");
            }
            else
            {
                Console.WriteLine("\nLogin Fail");
            }

            channel.ShutdownAsync().Wait();
            Console.ReadKey();
        }
    }
}
