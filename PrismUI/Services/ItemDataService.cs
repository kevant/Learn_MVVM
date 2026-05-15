using Items;
using PrismUI.Model;

namespace PrismUI.Services
{
    public class ItemDataService : IItemDataService
    {
        private readonly ItemService.ItemServiceClient _client;

        public ItemDataService()
        {
            var channel = new Grpc.Core.Channel("127.0.0.1:5000", Grpc.Core.ChannelCredentials.Insecure);
            _client = new ItemService.ItemServiceClient(channel);
        }

        public async Task<(bool success, string message, List<ItemModel> items)> GetItemsAsync()
        {
            var request = new ItemRequest
            {
                RequestId = "a"
            };

            var response = await _client.GetItemsAsync(request);
            var items = response.Items
                .Select(x => new ItemModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();

            return (response.Success, response.Message, items);
        }
    }
}
