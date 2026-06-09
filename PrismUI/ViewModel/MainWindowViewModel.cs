using Prism.Commands;
using Prism.Mvvm;
using PrismUI.Model;
using PrismUI.Services;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PrismUI
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IItemDataService _itemDataService;

        public ObservableCollection<ItemModel> Items { get; } = new ObservableCollection<ItemModel>();

        private string _statusMessage = "";
        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        public DelegateCommand LoadItemsCommand { get; private set; }

        public MainWindowViewModel(IItemDataService itemDataService)
        {
            _itemDataService = itemDataService;
            LoadItemsCommand = new DelegateCommand(async () => await LoadItemsAsync());
            //LoadItemsCommand = new DelegateCommand(Execute, CanExecute);
        }

        private async Task LoadItemsAsync()
        {
            StatusMessage = "Loading...";

            var result = await _itemDataService.GetItemsAsync();
            Items.Clear();

            foreach (var item in result.items)
            {
                Items.Add(item);
            }

            StatusMessage = result.message;
        }

        // for testing
        //private void Execute() { StatusMessage = $"Updated: {DateTime.Now}"; }
        //private bool CanExecute() { return true; }
    }

#if DEBUG
    public class DesignMainWindowViewModel
    {
        public string StatusMessage { get; set; } = "Some Status";
        public ObservableCollection<ItemModel> Items { get; set; } = new() { 
            new ItemModel { Id = 1, Name = "Item1" }, 
            new ItemModel { Id = 1, Name = "Item1" }
        };
    }
#endif
}
