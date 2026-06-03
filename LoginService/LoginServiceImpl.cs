using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Login
{
    public class LoginServiceImpl : LoginService.LoginServiceBase
    {
        public override Task<LoginResponse> Authenticate(LoginRequest request, ServerCallContext context)
        {
            Console.WriteLine("Received request:");
            Console.WriteLine($"Username: {request.Username}, Password: {request.Password}\n");

            LoginResponse response = new LoginResponse();
            if (request.Username == "user" && request.Password == "1")
            {
                response.Success = true;
                response.Displayname = "SomeName";
            }
            else
            {
                response.Success = false;
            }

            string result = response.Success ? "Success" : "Fail";
            Console.WriteLine($"Send response: {result}");

            return Task.FromResult(response);
        }
    }
}