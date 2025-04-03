using MarketPlaceDTO;
using MarketplaceServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ItemController : ControllerBase
{
    private readonly IItemDtoService _itemsService;

    public ItemController(IItemDtoService itemsService)
    {
        _itemsService = itemsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllItems()
    {
        var items = await _itemsService.GetAllAsync();
        if (items == null || !items.Any())
        {
            return NotFound();
        }
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetItemById(int id)
    {
        var Item = await _itemsService.GetByIdAsync(id);
        if (Item == null)
        {
            return NotFound();
        }
        return Ok(Item);
    }

    [HttpPost]
    public async Task<IActionResult> CreateItem(ItemDto itemDto)
    {
        var createdItem = await _itemsService.CreateAsync(itemDto);
        return CreatedAtAction(nameof(GetItemById), new { id = createdItem.Id }, createdItem);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateItem(int id, ItemDto ItemDto)
    {
        var updatedItem = await _itemsService.UpdateAsync(id, ItemDto);
        if (!updatedItem)
        {
            return NotFound();
        }
        return Ok(updatedItem);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteItem(int id)
    {
        var result = await _itemsService.DeleteAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}