using RestSampleApp.Models;

namespace RestSampleApp.Services.ItemService;

public interface IItemService
{
    Task<long> CreateAsync(ItemRequest item);
    IEnumerable<ItemResponse> GetAllAsync();

}