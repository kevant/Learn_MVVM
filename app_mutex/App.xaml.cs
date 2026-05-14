using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;

namespace app_mutex
{
    public partial class App : Application
    {
        private static Mutex _mutex;

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        protected override void OnStartup(StartupEventArgs e)
        {
            const string mutexName = "SingleInstanceWpfApp";

            // for machine-wide single instance
            //const string mutexName = @"Global\CompanyName_MyWpfApp";

            bool createdNew;
            _mutex = new Mutex(true, mutexName, out createdNew);

            if (!createdNew)
            {
                BringExistingInstanceToFront();
                Shutdown();
                return;
            }

            base.OnStartup(e);
        }

        private void BringExistingInstanceToFront()
        {
            Process current = Process.GetCurrentProcess();

            foreach (Process process in Process.GetProcessesByName(current.ProcessName))
            {
                if (process.Id != current.Id)
                {
                    IntPtr handle = process.MainWindowHandle;

                    if (handle != IntPtr.Zero)
                    {
                        SetForegroundWindow(handle);
                    }

                    break;
                }
            }
        }
    }

}
