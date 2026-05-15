using Grpc.Core;
using Items;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyService
{
    public class ItemServiceImpl : ItemService.ItemServiceBase
    {
        public override Task<ItemResponse> GetItems(ItemRequest request, ServerCallContext context)
        {
            var response = new ItemResponse
            {
                Success = true,
                Message = "Items loaded successfully"
            };

            response.Items.Add(new ItemDto
            {
                Id = 1,
                Name = "Apple"
            });

            response.Items.Add(new ItemDto
            {
                Id = 2,
                Name = "Orange"
            });

            response.Items.Add(new ItemDto
            {
                Id = 3,
                Name = "Banana"
            });

            return Task.FromResult(response);
        }
    }
}