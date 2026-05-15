using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Items;
using PrismUI;

namespace PrismUI
{
    internal class Mock_ItemDataService : IItemDataService
    {
        public async Task<(bool success, string message, List<ItemModel> items)> GetItemsAsync()
        {
            List<ItemModel> items = new List<ItemModel>();
            await Task.Run(() => {
                Task.Delay(2000);
                items.Add(new ItemModel() { Id = 1, Name = "Name1"});
                items.Add(new ItemModel() { Id = 2, Name = "Name2"});
                items.Add(new ItemModel() { Id = 3, Name = "Name3"});
            });
            return (true, "Some Message", items);
        }
    }
}
