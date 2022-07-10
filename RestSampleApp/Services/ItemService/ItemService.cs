using Microsoft.EntityFrameworkCore;
using RestSampleApp.Data;
using RestSampleApp.Entities;
using RestSampleApp.Models;

namespace RestSampleApp.Services.ItemService;

public class ItemService : IItemService
{
    private readonly AppDbContext appDbContext;

    public ItemService(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task<long> CreateAsync(ItemRequest item)
    {
        if (await appDbContext.Items.AnyAsync(x => x.Name.ToLower() == item.Name.ToLower()))
        {
            Console.WriteLine($"{item.Name} already exists");
            return 0;
        }

        var newItem = new Item
        {
            Name = item.Name,
            Quantity = item.Quantity
        };

        await appDbContext.Items.AddAsync(newItem);
        await appDbContext.SaveChangesAsync();

        return newItem.Id;
    }

    public IEnumerable<ItemResponse> GetAllAsync() => appDbContext.Items.Select(x => new ItemResponse(x.Id, x.Name, x.Quantity));
}