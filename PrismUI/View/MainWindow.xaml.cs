using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrismUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // This block only compiles and runs during development
#if DEBUG
        // Check if we are currently looking at the Visual Studio / Rider designer
        if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
        {
            this.DataContext = new DesignMainWindowViewModel();
        }
#endif
        }
    }
}