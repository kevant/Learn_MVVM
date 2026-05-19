using System.Reflection;
using System.IO;
namespace AOP
{
    public class LoggingProxy<T> : DispatchProxy
    {
        public T Target { get; set; }

        protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
        {
            File.AppendAllText(".\\log.txt", $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][LOG] Entering {targetMethod.Name}\n");
            Console.WriteLine($"[LOG] Entering {targetMethod.Name}");
            try
            {
                var result = targetMethod.Invoke(Target, args);
                File.AppendAllText(".\\log.txt", $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][LOG] Exiting {targetMethod.Name}\n");
                Console.WriteLine($"[LOG] Exiting {targetMethod.Name}");
                return result;
            }
            catch (TargetInvocationException ex)
            {
                File.AppendAllText(".\\log.txt", $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][ERROR] {ex.InnerException?.Message}\n");
                Console.WriteLine($"[ERROR] {ex.InnerException?.Message}");
                throw ex.InnerException!;
            }
        }

        public static T Create(T target)
        {
            var proxy = DispatchProxy.Create<T, LoggingProxy<T>>();
            ((LoggingProxy<T>)(object)proxy).Target = target;
            return proxy;
        }
    }
}
