/*
Service Abstraction (Prism Style)

To align with Prism recommendations:
ViewModel does not directly know transport details
Hence it relies on an interface instead
*/
namespace WpfApp1
{
    public interface IItemDataService
    {
        Task<(bool success, string message, List<ItemModel> items)> GetItemsAsync();
    }
}
