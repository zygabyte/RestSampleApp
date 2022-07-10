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
            if (bool.TryParse(HttpContext.Session.GetString("UserLoggedIn"), out var loggedIn) && loggedIn)
            {
                var result = await itemService.CreateAsync(itemRequest);
                return result > 0 ? Ok(result) : BadRequest("Error creating item");
            }

            return Unauthorized("User is not authenticated");
        }

        [HttpGet]
        public ActionResult<IEnumerable<ItemResponse>> GetItems()
        {
            if (bool.TryParse(HttpContext.Session.GetString("UserLoggedIn"), out var loggedIn) && loggedIn)
            {
                var result = itemService.GetAllAsync();
                return Ok(result);
            }

            return Unauthorized("User is not authenticated");
        }
    }
}
