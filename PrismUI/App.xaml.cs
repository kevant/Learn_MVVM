using Prism.Ioc;
using Prism.Unity;
using PrismUI.Services;
using System.Windows;

namespace PrismUI
{
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //containerRegistry.Register<IItemDataService, ItemDataService>();
            containerRegistry.Register<IItemDataService, Mock_ItemDataService>();
        }
    }
}
