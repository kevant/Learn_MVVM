using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace WpfApp1
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

        public DelegateCommand LoadItemsCommand { get; }

        public MainWindowViewModel(IItemDataService itemDataService)
        {
            _itemDataService = itemDataService;
            LoadItemsCommand = new DelegateCommand(async () => await LoadItemsAsync());
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
    }
}
