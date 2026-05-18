using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Check_AD_Group_Membership
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        App()
        {
            bool isAuthorized = AuthHelper.IsUserAuthorized();

            if (!isAuthorized)
            {
                MessageBox.Show("Access denied.");
                Application.Current.Shutdown();
            }
        }
    }
}
