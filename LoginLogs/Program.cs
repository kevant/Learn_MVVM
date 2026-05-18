using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;


namespace LoginLogs
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var logins = SecurityLogReader.GetLogins();

            foreach (var login in logins)
            {
                Console.WriteLine($"{login.TimeCreated} | d: {login.Domain} | u: {login.UserName}");
            }

            Console.ReadLine();
        }
    }
}
