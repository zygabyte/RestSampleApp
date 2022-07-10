using Microsoft.AspNetCore.Mvc;
using RestSampleApp.Models;
using RestSampleApp.Services.ItemService;

namespace RestSampleApp.Controllers
{
    [ApiController]
    [Route("/api/item")]
    public class ItemController : Controller
    {
        private readonly IItemService itemService;

        public ItemController(IItemService itemService)
        {
            this.itemService = itemService;
        }

        [HttpPost]
        public async Task<ActionResult<long>> CreateItem([FromBody] ItemRequest itemRequest)
        {
            var result = await itemService.CreateAsync(itemRequest);
            return result > 0 ? Ok(result) : BadRequest("Error creating item");
        }

        [HttpGet]
        public ActionResult<IEnumerable<ItemResponse>> GetItems()
        {
            var result = itemService.GetAllAsync();
            return Ok(result);
        }
    }
}
