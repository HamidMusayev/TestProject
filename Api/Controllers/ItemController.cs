using Api;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ItemController : ControllerBase
{
    private readonly List<Item> _items = new List<Item>
    {
        new Item { ItemId = 1, Name = "Item 1", Price = 10.99m },
        new Item { ItemId = 2, Name = "Item 2", Price = 20.49m }
    };

    [HttpGet]
    public ActionResult<IEnumerable<Item>> Get()
    {
        return Ok(_items);
    }

    [HttpGet("{id}")]
    public ActionResult<Item> Get(int id)
    {
        var item = _items.Find(i => i.ItemId == id);
        if (item == null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    [HttpPost]
    public ActionResult<Item> Post([FromBody] Item newItem)
    {
        newItem.ItemId = _items.Count + 1;
        _items.Add(newItem);

        return CreatedAtAction(nameof(Get), new { id = newItem.ItemId }, newItem);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Item updatedItem)
    {
        var existingItem = _items.Find(i => i.ItemId == id);
        if (existingItem == null)
        {
            return NotFound();
        }

        existingItem.Name = updatedItem.Name;
        existingItem.Price = updatedItem.Price;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var item = _items.Find(i => i.ItemId == id);
        if (item == null)
        {
            return NotFound();
        }

        _items.Remove(item);

        return NoContent();
    }
}
